import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { MainLayout } from './layouts/main-layout/main-layout';
import { Home } from './pages/main/home/home';
import { Search } from './pages/main/search/search';
import { Artist } from './pages/main/artist/artist';
import { Playlist } from './pages/main/playlist/playlist';
import { AdminLayout } from './layouts/admin-layout/admin-layout';
import { SongList } from './pages/admin/songs/song-list/song-list';
import { SongCreate } from './pages/admin/songs/song-create/song-create';
import { SongUpdate } from './pages/admin/songs/song-update/song-update';
import { ArtistList } from './pages/admin/artists/artist-list/artist-list';
import { ArtistCreate } from './pages/admin/artists/artist-create/artist-create';
import { ArtistUpdate } from './pages/admin/artists/artist-update/artist-update';
import { AlbumList } from './pages/admin/albums/album-list/album-list';
import { AlbumCreate } from './pages/admin/albums/album-create/album-create';
import { AlbumUpdate } from './pages/admin/albums/album-update/album-update';
import { GenreList } from './pages/admin/genres/genre-list/genre-list';
import { GenreCreate } from './pages/admin/genres/genre-create/genre-create';
import { GenreUpdate } from './pages/admin/genres/genre-update/genre-update';
import { UserList } from './pages/admin/users/user-list/user-list';
import { adminGuard } from './guards/admin-guard';
import { authGuard } from './guards/auth-guard';
import { Dashboard } from './pages/admin/dashboard/dashboard';
import { Profile } from './pages/main/profile/profile';
import { Album } from './pages/main/album/album';
import { Forbidden } from './pages/forbidden/forbidden';

export const routes: Routes = [
  { path: 'login', component: Login },
  { path: 'register', component: Register },
  { path: '403', component: Forbidden },

  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard],
    children: [
      { path: 'home', component: Home },
      { path: 'search', component: Search },
      { path: 'artist/:id', component: Artist },
      { path: 'playlist/:id', component: Playlist },
      { path: 'profile', component: Profile },
      { path: 'album/:id', component: Album },
      { path: '', redirectTo: 'home', pathMatch: 'full' },
    ],
  },

  {
    path: 'admin',
    component: AdminLayout,
    canActivate: [adminGuard],
    children: [
      { path: 'dashboard', component: Dashboard },

      { path: 'songs', component: SongList },
      { path: 'songs/create', component: SongCreate },
      { path: 'songs/update/:id', component: SongUpdate },

      { path: 'artists', component: ArtistList },
      { path: 'artists/create', component: ArtistCreate },
      { path: 'artists/update/:id', component: ArtistUpdate },

      { path: 'albums', component: AlbumList },
      { path: 'albums/create', component: AlbumCreate },
      { path: 'albums/update/:id', component: AlbumUpdate },

      { path: 'genres', component: GenreList },
      { path: 'genres/create', component: GenreCreate },
      { path: 'genres/update/:id', component: GenreUpdate },

      { path: 'users', component: UserList },

      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    ],
  },
];
