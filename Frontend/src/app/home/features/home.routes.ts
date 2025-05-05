import { Routes } from "@angular/router";

export default [
    {
        path: '',
        loadComponent: () => import('./home-detail/home-detail.component'),
    }
] as Routes;