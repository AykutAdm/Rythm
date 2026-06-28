import {
  AfterViewInit,
  Component,
  ElementRef,
  inject,
  OnDestroy,
  OnInit,
  signal,
  ViewChild,
} from '@angular/core';
import { NavigationEnd, Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { CommonModule } from '@angular/common';
import { AudioService } from '../../services/audio-service';
import { PlaylistService } from '../../services/playlist-service';
import { ResultPlaylistDto } from '../../models/playlist-model';
import { FormsModule } from '@angular/forms';
import { filter } from 'rxjs';
import { LikedSongsService } from '../../services/liked-songs-service';
import { UserService } from '../../services/user-service';
import { UserProfileDto } from '../../models/user-model';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule, FormsModule],
  templateUrl: './main-layout.html',
  styleUrl: './main-layout.css',
})
export class MainLayout implements AfterViewInit, OnDestroy, OnInit {
  @ViewChild('shaderContainer') containerRef!: ElementRef<HTMLCanvasElement>;

  constructor(
    private authService: AuthService,
    private router: Router,
    private playlistService: PlaylistService,
  ) {}

  audioService = inject(AudioService);

  playlists = signal<ResultPlaylistDto[]>([]);

  isCreatePlaylistOpen = signal<boolean>(false);

  likedSongsService = inject(LikedSongsService);

  private userService = inject(UserService);

  currentUser = signal<UserProfileDto | null>(null);

  playlistModel = {
    name: '',
    description: '',
    coverImageUrl: '',
    isPublic: true,
    appUserId: 0,
  };

  openCreatePlaylist(event: MouseEvent) {
    event.stopPropagation();
    this.isCreatePlaylistOpen.set(true);
  }

  closeCreatePlaylist() {
    this.isCreatePlaylistOpen.set(false);
  }

  createPlaylist() {
    this.playlistModel.appUserId = this.authService.getUserId();
    this.playlistService.create(this.playlistModel).subscribe({
      next: () => {
        this.isCreatePlaylistOpen.set(false);
        this.playlistModel = {
          name: '',
          description: '',
          coverImageUrl: '',
          isPublic: true,
          appUserId: 0,
        };
        this.loadPlaylists();
      },
      error: () => {
        this.isCreatePlaylistOpen.set(false);
        this.loadPlaylists();
      },
    });
  }

  seek(event: MouseEvent) {
    const bar = event.currentTarget as HTMLElement;
    const rect = bar.getBoundingClientRect();
    const ratio = (event.clientX - rect.left) / rect.width;
    const time = ratio * this.audioService.duration();
    this.audioService.seek(time);
  }

  isDropdownOpen = false;
  private animationId = 0;

  ngAfterViewInit() {
    this.initShader();
  }

  ngOnDestroy() {
    cancelAnimationFrame(this.animationId);
  }

  initShader() {
    const canvas = this.containerRef.nativeElement;
    const gl =
      canvas.getContext('webgl') ||
      (canvas.getContext('experimental-webgl') as WebGLRenderingContext);
    if (!gl) return;

    const vsSource = `
    precision mediump float;
    attribute vec2 a_position;
    varying vec2 vUv;
    void main() {
      vUv = .5 * (a_position + 1.);
      gl_Position = vec4(a_position, 0.0, 1.0);
    }
  `;

    const fsSource = `
    precision mediump float;
    varying vec2 vUv;
    uniform float u_time;
    uniform float u_ratio;
    uniform vec2 u_pointer_position;
    uniform float u_scroll_progress;

    vec2 rotate(vec2 uv, float th) {
      return mat2(cos(th), sin(th), -sin(th), cos(th)) * uv;
    }

    float neuro_shape(vec2 uv, float t, float p) {
      vec2 sine_acc = vec2(0.);
      vec2 res = vec2(0.);
      float scale = 8.;
      for (int j = 0; j < 15; j++) {
        uv = rotate(uv, 1.);
        sine_acc = rotate(sine_acc, 1.);
        vec2 layer = uv * scale + float(j) + sine_acc - t;
        sine_acc += sin(layer) + 2.4 * p;
        res += (.5 + .5 * cos(layer)) / scale;
        scale *= (1.2);
      }
      return res.x + res.y;
    }

    void main() {
      vec2 uv = .5 * vUv;
      uv.x *= u_ratio;
      vec2 pointer = vUv - u_pointer_position;
      pointer.x *= u_ratio;
      float p = clamp(length(pointer), 0., 1.);
      p = .5 * pow(1. - p, 2.);
      float t = .001 * u_time;
      vec3 color = vec3(0.);
      float noise = neuro_shape(uv, t, p);
      noise = 1.2 * pow(noise, 3.);
      noise += pow(noise, 10.);
      noise = max(.0, noise - .5);
      noise *= (1. - length(vUv - .5));
      color = vec3(0.05, 0.05, 0.6);
      color = mix(color, vec3(0.3, 0.4, 1.0), 0.4);
      color += vec3(0.2, 0.2, 0.8) * sin(2.0 * u_scroll_progress + 1.5);
      color = color * noise;
      gl_FragColor = vec4(color, noise);
    }
  `;

    const compileShader = (type: number, source: string) => {
      const shader = gl.createShader(type)!;
      gl.shaderSource(shader, source);
      gl.compileShader(shader);
      return shader;
    };

    const vertexShader = compileShader(gl.VERTEX_SHADER, vsSource);
    const fragmentShader = compileShader(gl.FRAGMENT_SHADER, fsSource);

    const program = gl.createProgram()!;
    gl.attachShader(program, vertexShader);
    gl.attachShader(program, fragmentShader);
    gl.linkProgram(program);
    gl.useProgram(program);

    const vertices = new Float32Array([-1, -1, 1, -1, -1, 1, 1, 1]);
    const vertexBuffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, vertexBuffer);
    gl.bufferData(gl.ARRAY_BUFFER, vertices, gl.STATIC_DRAW);

    const positionLocation = gl.getAttribLocation(program, 'a_position');
    gl.enableVertexAttribArray(positionLocation);
    gl.vertexAttribPointer(positionLocation, 2, gl.FLOAT, false, 0, 0);

    const uTime = gl.getUniformLocation(program, 'u_time');
    const uRatio = gl.getUniformLocation(program, 'u_ratio');
    const uPointerPosition = gl.getUniformLocation(program, 'u_pointer_position');
    const uScrollProgress = gl.getUniformLocation(program, 'u_scroll_progress');

    // Mouse pointer
    let pX = 0,
      pY = 0,
      tX = 0,
      tY = 0;

    const resizeCanvas = () => {
      const dpr = Math.min(window.devicePixelRatio, 2);
      canvas.width = window.innerWidth * dpr;
      canvas.height = window.innerHeight * dpr;
      gl.viewport(0, 0, canvas.width, canvas.height);
      gl.uniform1f(uRatio, canvas.width / canvas.height);
    };

    resizeCanvas();
    window.addEventListener('resize', resizeCanvas);

    const render = () => {
      pX += (tX - pX) * 0.2;
      pY += (tY - pY) * 0.2;

      gl.uniform1f(uTime, performance.now());
      gl.uniform2f(uPointerPosition, pX / window.innerWidth, 1 - pY / window.innerHeight);
      gl.uniform1f(uScrollProgress, 0);
      gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);
      this.animationId = requestAnimationFrame(render);
    };

    render();

    window.addEventListener('pointermove', (e) => {
      tX = e.clientX;
      tY = e.clientY;
    });
  }

  toggleDropdown(event: MouseEvent) {
    event.stopPropagation();
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  closeDropdown() {
    this.isDropdownOpen = false;
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  ngOnInit() {
    this.loadPlaylists();
    this.likedSongsService.load();
    this.loadCurrentUser();

    this.router.events.pipe(filter((event) => event instanceof NavigationEnd)).subscribe(() => {
      this.isDropdownOpen = false;
      this.loadPlaylists();
    });
  }

  loadPlaylists() {
    const userId = this.authService.getUserId();
    this.playlistService.getByUserId(userId).subscribe((data) => {
      this.playlists.set(data);
    });
  }

  loadCurrentUser() {
    const userId = this.authService.getUserId();
    this.userService.getProfile(userId).subscribe((data) => {
      this.currentUser.set(data);
    });
  }
}
