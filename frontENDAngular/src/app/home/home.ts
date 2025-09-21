import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [CommonModule, FormsModule, RouterOutlet],
  templateUrl: './home.html',
  styleUrl: './home.css'
})


export class Home {
  constructor(private router: Router) { }


  goToAbout() {
    this.router.navigate(['/home/about']);
  }
  goToContact() {
    this.router.navigate(['/home/contact']);
  }
   goToLogin() {
    this.router.navigate(['/home/login']);
  }
  register() {
    this.router.navigate(['/home/register']);
  }
  goToprojects() {
    this.router.navigate(['/home/projects']);
  }
  onLogin() {
    throw new Error('Method not implemented.');
  }

}
