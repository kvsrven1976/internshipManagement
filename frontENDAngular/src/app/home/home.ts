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

  username: any;
  password: any;

  goToAbout() {
    this.router.navigate(['/home/about']);
  }
  goToContact() {
    this.router.navigate(['/home/contact']);
  }
  register() {
    this.router.navigate(['/home/register']);
  }
  goToprojects() {
    this.router.navigate(['/home']);
  }
  onLogin() {
    throw new Error('Method not implemented.');
  }

}
