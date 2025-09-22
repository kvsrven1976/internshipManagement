import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-student-homepage',
  imports: [CommonModule, FormsModule, RouterOutlet],
  templateUrl: './student-homepage.html',
  styleUrl: './student-homepage.css'
})
export class StudentHomepage {
  constructor(private router: Router) { }

   profile() {
    this.router.navigate(['/student/profile']);
  }
   goToprojects() {
    this.router.navigate(['/home/login']);
  }
  register() {
    this.router.navigate(['/home/register']);
  }
  logout() {
    this.router.navigate(['/student/logout']);
  }
}
