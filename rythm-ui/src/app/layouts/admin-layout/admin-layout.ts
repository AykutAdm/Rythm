import { AfterViewInit, Component, ElementRef, OnDestroy, ViewChild } from '@angular/core';
import { Router, RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-layout',
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './admin-layout.html',
  styleUrl: './admin-layout.css',
})
export class AdminLayout implements AfterViewInit, OnDestroy {


  @ViewChild('shaderCanvas') canvasRef!: ElementRef<HTMLCanvasElement>;

  constructor(private authService: AuthService, private router: Router) {}

  isCollapsed = false;
  private animationId = 0;

    ngAfterViewInit() {
    this.initShader();
  }

  ngOnDestroy() {
    cancelAnimationFrame(this.animationId);
  }

  toggleSidebar() {
    this.isCollapsed = !this.isCollapsed;
  }

  onLogout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  initShader() {
    const canvas = this.canvasRef.nativeElement;
    const gl = canvas.getContext('webgl') || canvas.getContext('experimental-webgl') as WebGLRenderingContext;
    if (!gl) return;

    const vs = `
      attribute vec2 a_position;
      varying vec2 v_texCoord;
      void main() {
        v_texCoord = a_position * 0.5 + 0.5;
        gl_Position = vec4(a_position, 0.0, 1.0);
      }
    `;

    const fs = `
      precision highp float;
      uniform float u_time;
      uniform vec2 u_resolution;
      uniform vec2 u_mouse;
      varying vec2 v_texCoord;

      void main() {
        vec2 uv = v_texCoord;
        vec2 aspect = vec2(u_resolution.x / u_resolution.y, 1.0);
        vec2 p = (uv - 0.5) * aspect;
        float t = u_time * 0.18;

        vec2 mouse = (u_mouse / u_resolution - 0.5) * aspect * 0.35;

        vec2 c1 = vec2(0.35 + sin(t * 0.7) * 0.28, 0.35 + cos(t * 0.55) * 0.22) + mouse;
        vec2 c2 = vec2(-0.38 + cos(t * 0.65) * 0.24, -0.15 + sin(t * 0.75) * 0.28) - mouse * 0.6;
        vec2 c3 = vec2(-0.05 + sin(t * 0.45) * 0.32, 0.55 + cos(t * 0.85) * 0.18);
        vec2 c4 = vec2(0.5 + cos(t * 0.5) * 0.2, -0.45 + sin(t * 0.6) * 0.2) + mouse * 0.3;

        float orb1 = smoothstep(0.58, 0.0, length(p - c1));
        float orb2 = smoothstep(0.52, 0.0, length(p - c2));
        float orb3 = smoothstep(0.48, 0.0, length(p - c3));
        float orb4 = smoothstep(0.42, 0.0, length(p - c4));

        vec3 base = vec3(0.03, 0.04, 0.10);
        vec3 indigo = vec3(0.18, 0.22, 0.92);
        vec3 teal = vec3(0.06, 0.72, 0.78);
        vec3 violet = vec3(0.38, 0.18, 0.88);
        vec3 cyan = vec3(0.10, 0.52, 0.95);
        vec3 amber = vec3(0.85, 0.55, 0.08);

        vec3 col = base;
        col = mix(col, indigo, orb1 * 0.62);
        col = mix(col, teal, orb2 * 0.52);
        col = mix(col, violet, orb3 * 0.42);
        col = mix(col, cyan, orb4 * 0.38);
        col = mix(col, amber, (orb1 * orb4) * 0.18);

        float pulse = sin(t * 1.2) * 0.5 + 0.5;
        col += indigo * orb1 * pulse * 0.06;
        col += teal * orb2 * (1.0 - pulse) * 0.05;

        col *= 1.0 - dot(p, p) * 0.12;

        gl_FragColor = vec4(col, 1.0);
      }
    `;

    const compileShader = (type: number, source: string) => {
      const shader = gl.createShader(type)!;
      gl.shaderSource(shader, source);
      gl.compileShader(shader);
      return shader;
    };

    const program = gl.createProgram()!;
    gl.attachShader(program, compileShader(gl.VERTEX_SHADER, vs));
    gl.attachShader(program, compileShader(gl.FRAGMENT_SHADER, fs));
    gl.linkProgram(program);
    gl.useProgram(program);

    const buf = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, buf);
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array([-1,-1, 1,-1, -1,1, 1,1]), gl.STATIC_DRAW);

    const pos = gl.getAttribLocation(program, 'a_position');
    gl.enableVertexAttribArray(pos);
    gl.vertexAttribPointer(pos, 2, gl.FLOAT, false, 0, 0);

    const uTime = gl.getUniformLocation(program, 'u_time');
    const uRes = gl.getUniformLocation(program, 'u_resolution');
    const uMouse = gl.getUniformLocation(program, 'u_mouse');

    let mouse = { x: 0, y: 0 };

    const syncSize = () => {
      canvas.width = window.innerWidth;
      canvas.height = window.innerHeight;
      gl.viewport(0, 0, canvas.width, canvas.height);
    };

    syncSize();
    window.addEventListener('resize', syncSize);

    window.addEventListener('mousemove', (e) => {
      mouse.x = e.clientX / window.innerWidth * canvas.width;
      mouse.y = (1 - e.clientY / window.innerHeight) * canvas.height;
    });

    const render = (t: number) => {
      gl.uniform1f(uTime, t * 0.001);
      gl.uniform2f(uRes, canvas.width, canvas.height);
      gl.uniform2f(uMouse, mouse.x, mouse.y);
      gl.drawArrays(gl.TRIANGLE_STRIP, 0, 4);
      this.animationId = requestAnimationFrame(render);
    };

    render(0);
  }
}
