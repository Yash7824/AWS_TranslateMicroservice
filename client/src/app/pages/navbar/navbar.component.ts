import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {

  imagePath: string;

  constructor(){
    this.imagePath = "assets/Images/icici_logo-removebg-preview.png"
  }
}
