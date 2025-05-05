import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'home',
        loadChildren: () => import('./home/features/home.routes')
    },
    {
        path: '**',
        redirectTo: 'home'
    }
];
