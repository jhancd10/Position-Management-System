import { Routes } from "@angular/router";

export default [
    {
        path: '',
        loadComponent: () => import('./positions-list/positions-list.component'),
    }
] as Routes;