import { Component, inject, Input } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Category } from '../../interfaces/category.interface';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [
    MatCardModule,
    MatIconModule,
    MatButtonModule,
  ],
  templateUrl: './category-card.component.html',
  styleUrl: './category-card.component.scss'
})
export class CategoryCardComponent {

  private router = inject(Router);

  @Input() category!: Category;
  
  navigate() { 
    this.router.navigate([ this.category.route ]); 
  }
}
