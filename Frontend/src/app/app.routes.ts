import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: 'home',
        loadChildren: () => import('./home/features/home.routes')
    },
    {
        path: 'departments',
        loadChildren: () => import('./departments/features/departments.routes')
    },
    {
        path: 'recruiters',
        loadChildren: () => import('./recruiters/features/recruiters.routes')
    },
    {
        path: 'positions',
        loadChildren: () => import('./positions/features/positions.routes')
    },
    {
        path: '**',
        redirectTo: 'home'
    }
];
