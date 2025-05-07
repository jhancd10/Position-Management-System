import { Routes } from "@angular/router";

export default [
    {
        path: '',
        loadComponent: () => import('./departments-list/departments-list.component'),
    }
] as Routes;