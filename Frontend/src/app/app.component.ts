import { Component } from '@angular/core';
import { HeaderComponent } from "./shared/ui/header/header.component";
import { SideNavComponent } from "./shared/ui/side-nav/side-nav.component";

@Component({
  selector: 'app-root',
  imports: [HeaderComponent, SideNavComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  title = 'Position Management System';

}
