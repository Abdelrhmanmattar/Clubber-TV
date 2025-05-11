import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path:'',
        pathMatch:'full',
        loadComponent: ()=>import('./auth/login/login.component').then((m)=>m.LoginComponent),
    },
    {
        path:'login',
        loadComponent: ()=>import('./auth/login/login.component').then((m)=>m.LoginComponent),
    }
    ,{
        path:'matche',
        loadComponent:()=>import('./component/match-list/match-list.component').then((m)=>m.MatchListComponent),
    },
    {
        path:'register',
        loadComponent: ()=>import('./auth/register/register.component').then((m)=>m.RegisterComponent),
    },
    {
        path:'playlist',
        loadComponent: ()=>import('./component/playlist/playlist.component').then((m)=>m.PlaylistComponent),
    }
];
