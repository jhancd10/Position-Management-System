import { computed, effect, inject, Injectable, signal } from "@angular/core";
import { DepartmentsStateService } from "../../departments/services/departments-state.service";
import { RecruitersStateService } from "../../recruiters/services/recruiters-state.service";
import { PositionsStateService } from "../../positions/services/positions-state.service";
import { Category } from "../interfaces/category.interface";

@Injectable({
  providedIn: 'root',
})
export class HomeService {

    private departmentsStateService = inject(DepartmentsStateService);
    private recruitersStateService = inject(RecruitersStateService);
    private positionsStateService = inject(PositionsStateService);

    private departmentsTotal = computed(() => this.departmentsStateService.state().data.length);
    private recruitersTotal = computed(() => this.recruitersStateService.state().data.length);
    private positionsTotal = computed(() => this.positionsStateService.state().data.length);

    public categories = signal<Category[]>([]);

    constructor() {

        effect(() => {

            this.departmentsTotal();
            this.recruitersTotal();
            this.positionsTotal();

            this.categories.set(
            [
                {
                    title: 'Departments',
                    subtitle: 'Manage company departments',
                    icon: 'business',
                    total: this.departmentsTotal,
                    route: '/departments',
                },
                {
                    title: 'Recruiters',
                    subtitle: 'Manage recruiting team',
                    icon: 'people',
                    total: this.recruitersTotal,
                    route: '/recruiters',
                  },
                  {
                    title: 'Positions',
                    subtitle: 'Manage job positions',
                    icon: 'work',
                    total: this.positionsTotal,
                    route: '/positions',
                  },
            ]);
        });
    }
}