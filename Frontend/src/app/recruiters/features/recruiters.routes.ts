import { Routes } from "@angular/router";

export default [
    {
        path: '',
        loadComponent: () => import('./recruiters-list/recruiters-list.component'),
    }
] as Routes;