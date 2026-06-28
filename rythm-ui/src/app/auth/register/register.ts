import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { RegisterRequest } from '../../models/auth.model';
import { AuthService } from '../../services/auth-service';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  imports: [FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register implements AfterViewInit {

   @ViewChild('canvas') canvasRef!: ElementRef<HTMLCanvasElement>;

  constructor(private authService: AuthService, private router: Router) {}

    model: RegisterRequest = {
    firstName: '',
    lastName: '',
    userName: '',
    email: '',
    password: '',
    birthDate: ''
  }

  mouseX = 0;
  mouseY = 0;
  isHovering = false;

  ngAfterViewInit() {
    this.initWebGL();
  }

  initWebGL() {
    const canvas = this.canvasRef.nativeElement;
    const gl = canvas.getContext('webgl');
    if (!gl) return;

    const vertexSource = `
      attribute vec4 a_position;
      void main() {
        gl_Position = a_position;
      }
    `;

    const fragmentSource = `
      precision mediump float;
      uniform vec2 iResolution;
      uniform float iTime;
      uniform vec2 iMouse;
      uniform vec3 u_color;

      void mainImage(out vec4 fragColor, in vec2 fragCoord){
        vec2 uv = fragCoord / iResolution;
        vec2 centeredUV = (2.0 * fragCoord - iResolution.xy) / min(iResolution.x, iResolution.y);
        float time = iTime * 0.5;
        vec2 mouse = iMouse / iResolution;
        vec2 rippleCenter = 2.0 * mouse - 1.0;
        vec2 distortion = centeredUV;
        for (float i = 1.0; i < 8.0; i++) {
          distortion.x += 0.5 / i * cos(i * 2.0 * distortion.y + time + rippleCenter.x * 3.1415);
          distortion.y += 0.5 / i * cos(i * 2.0 * distortion.x + time + rippleCenter.y * 3.1415);
        }
        float wave = abs(sin(distortion.x + distortion.y + time));
        float glow = smoothstep(0.9, 0.2, wave);
        fragColor = vec4(u_color * glow, 1.0);
      }

      void main() {
        mainImage(gl_FragColor, gl_FragCoord.xy);
      }
    `;

    const compileShader = (type: number, source: string) => {
      const shader = gl.createShader(type)!;
      gl.shaderSource(shader, source);
      gl.compileShader(shader);
      return shader;
    };

    const vertexShader = compileShader(gl.VERTEX_SHADER, vertexSource);
    const fragmentShader = compileShader(gl.FRAGMENT_SHADER, fragmentSource);

    const program = gl.createProgram()!;
    gl.attachShader(program, vertexShader);
    gl.attachShader(program, fragmentShader);
    gl.linkProgram(program);
    gl.useProgram(program);

    const buffer = gl.createBuffer();
    gl.bindBuffer(gl.ARRAY_BUFFER, buffer);
    gl.bufferData(gl.ARRAY_BUFFER, new Float32Array([-1,-1, 1,-1, -1,1, -1,1, 1,-1, 1,1]), gl.STATIC_DRAW);

    const posLoc = gl.getAttribLocation(program, 'a_position');
    gl.enableVertexAttribArray(posLoc);
    gl.vertexAttribPointer(posLoc, 2, gl.FLOAT, false, 0, 0);

    const iResolution = gl.getUniformLocation(program, 'iResolution');
    const iTime = gl.getUniformLocation(program, 'iTime');
    const iMouse = gl.getUniformLocation(program, 'iMouse');
    const uColor = gl.getUniformLocation(program, 'u_color');

    gl.uniform3f(uColor, 0.25, 0.35, 1.0);

    const startTime = Date.now();

    const render = () => {
      const w = window.innerWidth;
      const h = window.innerHeight;
      canvas.width = w;
      canvas.height = h;
      gl.viewport(0, 0, w, h);

      const currentTime = (Date.now() - startTime) / 1000;
      gl.uniform2f(iResolution, w, h);
      gl.uniform1f(iTime, currentTime);
      gl.uniform2f(iMouse, this.isHovering ? this.mouseX : w / 2, this.isHovering ? h - this.mouseY : h / 2);
      gl.drawArrays(gl.TRIANGLES, 0, 6);
      requestAnimationFrame(render);
    };

    canvas.addEventListener('mousemove', (e) => {
      const rect = canvas.getBoundingClientRect();
      this.mouseX = e.clientX - rect.left;
      this.mouseY = e.clientY - rect.top;
    });

    canvas.addEventListener('mouseenter', () => this.isHovering = true);
    canvas.addEventListener('mouseleave', () => this.isHovering = false);

    render();
  }

   onRegister() {
    this.authService.register(this.model).subscribe(() => {
      this.router.navigate(['/login']);
    });
   }
}
