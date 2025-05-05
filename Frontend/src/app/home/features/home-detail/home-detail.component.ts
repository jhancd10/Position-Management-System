import { Component, inject } from '@angular/core';
import { HomeService } from '../../services/home.service';
import { MatGridListModule } from '@angular/material/grid-list';
import { CategoryCardComponent } from '../../ui/category-card/category-card.component';

@Component({
  selector: 'app-home-detail',
  standalone: true,
  imports: [
    MatGridListModule,
    CategoryCardComponent
  ],
  templateUrl: './home-detail.component.html',
  styleUrl: './home-detail.component.scss'
})
export default class HomeDetailComponent {

  private homeService = inject(HomeService);

  categories = this.homeService.categories;
  
  columns = 3;

  constructor() {
    
    // Responsive columns based on window width
    this.setColumnsCount(window.innerWidth);
    
    window.addEventListener('resize', () => {
      this.setColumnsCount(window.innerWidth);
    });
  }

  private setColumnsCount(width: number) {    
    if (width < 600) this.columns = 1; 
    else if (width < 1000) this.columns = 2;
    else this.columns = 3;
  }
}
