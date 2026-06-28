import { DecimalPipe } from '@angular/common';
import { Component, computed, effect, inject, signal } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import { RouterLink } from '@angular/router';
import { DashboardService } from '../../../services/dashboard-service';

interface StatCard {
  label: string;
  value: number;
  icon: string;
  gradient: string;
  accentLine: string;
  iconBg: string;
  glow: string;
  ring: string;
  link: string;
  share: number;
}

interface QuickAction {
  label: string;
  description: string;
  icon: string;
  link: string;
  gradient: string;
  iconBg: string;
  iconColor: string;
}

interface BreakdownItem {
  label: string;
  value: number;
  color: string;
  hex: string;
  pct: number;
}

interface DonutSegment extends BreakdownItem {
  dash: number;
  offset: number;
  circumference: number;
}

interface PlatformModule {
  label: string;
  value: number;
  icon: string;
  color: string;
  barColor: string;
  link: string;
  max: number;
}

@Component({
  selector: 'app-dashboard',
  imports: [RouterLink, DecimalPipe],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  private dashboardService = inject(DashboardService);

  readonly donutCircumference = 100;
  animatedTotal = signal(0);
  animatedHealth = signal(0);

  stats = toSignal(this.dashboardService.getStats(), {
    initialValue: null,
  });

  today = new Date().toLocaleDateString('tr-TR', {
    weekday: 'long',
    day: 'numeric',
    month: 'long',
    year: 'numeric',
  });

  greeting = computed(() => {
    const hour = new Date().getHours();
    if (hour < 12) return 'Günaydın';
    if (hour < 18) return 'İyi günler';
    return 'İyi akşamlar';
  });

  greetingIcon = computed(() => {
    const hour = new Date().getHours();
    if (hour < 12) return 'wb_twilight';
    if (hour < 18) return 'light_mode';
    return 'dark_mode';
  });

  totalContent = computed(() => {
    const s = this.stats();
    if (!s) return 0;
    return s.totalSongs + s.totalArtists + s.totalAlbums + s.totalGenres + s.totalPlaylists;
  });

  healthScore = computed(() => {
    const s = this.stats();
    if (!s) return 0;
    const categories = [s.totalSongs, s.totalArtists, s.totalAlbums, s.totalGenres, s.totalPlaylists];
    return Math.round((categories.filter((c) => c > 0).length / 5) * 100);
  });

  healthLabel = computed(() => {
    const score = this.healthScore();
    if (score >= 80) return 'Mükemmel';
    if (score >= 60) return 'İyi';
    if (score >= 40) return 'Orta';
    return 'Başlangıç';
  });

  statCards = computed((): StatCard[] => {
    const s = this.stats();
    if (!s) return [];

    const total = this.totalContent() || 1;
    const cards: Omit<StatCard, 'share'>[] = [
      {
        label: 'Şarkılar',
        value: s.totalSongs,
        icon: 'music_note',
        gradient: 'from-indigo-600/30 via-indigo-500/5 to-transparent',
        accentLine: 'from-indigo-400 via-indigo-500 to-transparent',
        iconBg: 'bg-indigo-500/20 text-indigo-300 border-indigo-400/30',
        glow: 'hover:shadow-indigo-500/30',
        ring: 'ring-indigo-500/20',
        link: '/admin/songs',
      },
      {
        label: 'Sanatçılar',
        value: s.totalArtists,
        icon: 'artist',
        gradient: 'from-violet-600/30 via-violet-500/5 to-transparent',
        accentLine: 'from-violet-400 via-violet-500 to-transparent',
        iconBg: 'bg-violet-500/20 text-violet-300 border-violet-400/30',
        glow: 'hover:shadow-violet-500/30',
        ring: 'ring-violet-500/20',
        link: '/admin/artists',
      },
      {
        label: 'Albümler',
        value: s.totalAlbums,
        icon: 'album',
        gradient: 'from-cyan-600/30 via-cyan-500/5 to-transparent',
        accentLine: 'from-cyan-400 via-cyan-500 to-transparent',
        iconBg: 'bg-cyan-500/20 text-cyan-300 border-cyan-400/30',
        glow: 'hover:shadow-cyan-500/30',
        ring: 'ring-cyan-500/20',
        link: '/admin/albums',
      },
      {
        label: 'Türler',
        value: s.totalGenres,
        icon: 'category',
        gradient: 'from-emerald-600/30 via-emerald-500/5 to-transparent',
        accentLine: 'from-emerald-400 via-emerald-500 to-transparent',
        iconBg: 'bg-emerald-500/20 text-emerald-300 border-emerald-400/30',
        glow: 'hover:shadow-emerald-500/30',
        ring: 'ring-emerald-500/20',
        link: '/admin/genres',
      },
      {
        label: 'Playlistler',
        value: s.totalPlaylists,
        icon: 'playlist_play',
        gradient: 'from-amber-600/30 via-amber-500/5 to-transparent',
        accentLine: 'from-amber-400 via-amber-500 to-transparent',
        iconBg: 'bg-amber-500/20 text-amber-300 border-amber-400/30',
        glow: 'hover:shadow-amber-500/30',
        ring: 'ring-amber-500/20',
        link: '/admin/songs',
      },
      {
        label: 'Kullanıcılar',
        value: s.totalUsers,
        icon: 'group',
        gradient: 'from-sky-600/30 via-sky-500/5 to-transparent',
        accentLine: 'from-sky-400 via-sky-500 to-transparent',
        iconBg: 'bg-sky-500/20 text-sky-300 border-sky-400/30',
        glow: 'hover:shadow-sky-500/30',
        ring: 'ring-sky-500/20',
        link: '/admin/users',
      },
    ];

    return cards.map((card) => ({
      ...card,
      share: Math.round((card.value / total) * 100),
    }));
  });

  featuredCards = computed(() => this.statCards().slice(0, 2));
  secondaryCards = computed(() => this.statCards().slice(2));

  contentBreakdown = computed((): BreakdownItem[] => {
    const s = this.stats();
    if (!s) return [];

    const total = this.totalContent();
    if (total === 0) return [];

    return [
      { label: 'Şarkılar', value: s.totalSongs, color: 'bg-indigo-500', hex: '#6366f1', pct: (s.totalSongs / total) * 100 },
      { label: 'Sanatçılar', value: s.totalArtists, color: 'bg-violet-500', hex: '#8b5cf6', pct: (s.totalArtists / total) * 100 },
      { label: 'Albümler', value: s.totalAlbums, color: 'bg-cyan-500', hex: '#06b6d4', pct: (s.totalAlbums / total) * 100 },
      { label: 'Türler', value: s.totalGenres, color: 'bg-emerald-500', hex: '#10b981', pct: (s.totalGenres / total) * 100 },
      { label: 'Playlistler', value: s.totalPlaylists, color: 'bg-amber-500', hex: '#f59e0b', pct: (s.totalPlaylists / total) * 100 },
    ];
  });

  donutSegments = computed((): DonutSegment[] => {
    const items = this.contentBreakdown();
    let offset = 0;

    return items.map((item) => {
      const dash = (item.pct / 100) * this.donutCircumference;
      const segment: DonutSegment = {
        ...item,
        dash,
        offset,
        circumference: this.donutCircumference,
      };
      offset += dash;
      return segment;
    });
  });

  platformModules = computed((): PlatformModule[] => {
    const s = this.stats();
    if (!s) return [];

    const max = Math.max(s.totalSongs, s.totalArtists, s.totalAlbums, s.totalGenres, s.totalPlaylists, 1);

    return [
      { label: 'Şarkı Kütüphanesi', value: s.totalSongs, icon: 'music_note', color: 'text-indigo-400', barColor: 'bg-indigo-500', link: '/admin/songs', max },
      { label: 'Sanatçı Havuzu', value: s.totalArtists, icon: 'artist', color: 'text-violet-400', barColor: 'bg-violet-500', link: '/admin/artists', max },
      { label: 'Albüm Arşivi', value: s.totalAlbums, icon: 'album', color: 'text-cyan-400', barColor: 'bg-cyan-500', link: '/admin/albums', max },
      { label: 'Tür Kataloğu', value: s.totalGenres, icon: 'category', color: 'text-emerald-400', barColor: 'bg-emerald-500', link: '/admin/genres', max },
    ];
  });

  readonly quickActions: QuickAction[] = [
    {
      label: 'Yeni Şarkı',
      description: 'Kütüphaneye ekle',
      icon: 'music_note',
      link: '/admin/songs/create',
      gradient: 'from-indigo-500/20 to-indigo-600/5',
      iconBg: 'bg-indigo-500/20 border-indigo-400/30',
      iconColor: 'text-indigo-300',
    },
    {
      label: 'Yeni Sanatçı',
      description: 'Profil oluştur',
      icon: 'person_add',
      link: '/admin/artists/create',
      gradient: 'from-violet-500/20 to-violet-600/5',
      iconBg: 'bg-violet-500/20 border-violet-400/30',
      iconColor: 'text-violet-300',
    },
    {
      label: 'Yeni Albüm',
      description: 'Kayıt aç',
      icon: 'library_add',
      link: '/admin/albums/create',
      gradient: 'from-cyan-500/20 to-cyan-600/5',
      iconBg: 'bg-cyan-500/20 border-cyan-400/30',
      iconColor: 'text-cyan-300',
    },
    {
      label: 'Yeni Tür',
      description: 'Tür tanımla',
      icon: 'new_label',
      link: '/admin/genres/create',
      gradient: 'from-emerald-500/20 to-emerald-600/5',
      iconBg: 'bg-emerald-500/20 border-emerald-400/30',
      iconColor: 'text-emerald-300',
    },
  ];

  constructor() {
    effect(() => {
      const total = this.totalContent();
      const health = this.healthScore();
      if (total > 0 || health > 0) {
        this.animateValue(this.animatedTotal, total);
        this.animateValue(this.animatedHealth, health);
      }
    });
  }

  private animateValue(target: ReturnType<typeof signal<number>>, end: number) {
    const duration = 900;
    const start = target();
    const startTime = performance.now();

    const step = (now: number) => {
      const progress = Math.min((now - startTime) / duration, 1);
      const eased = 1 - Math.pow(1 - progress, 3);
      target.set(Math.round(start + (end - start) * eased));
      if (progress < 1) requestAnimationFrame(step);
    };

    requestAnimationFrame(step);
  }

  moduleFill(value: number, max: number): number {
    return max === 0 ? 0 : Math.round((value / max) * 100);
  }
}
