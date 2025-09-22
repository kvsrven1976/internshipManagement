import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-student-profile',
  imports: [CommonModule],
  templateUrl: './student-profile.html',
  styleUrl: './student-profile.css'
})
export class StudentProfile {
  student = {
    id: 'STU123',
    fullName: 'Shubham Jha',
    email: 'shubhamjha@gmail.com',
    mobile: '9876543210',
    gender: 'Male',
    dob: '29 March 2000',
    enrollment: 'ENR56789',
    college: 'XYZ Engineering College',
    university: 'ABC University',
    course: 'B.Tech',
    branch: 'Computer Science',
    year: 'Final Year',
    cgpa: '8.5',
    technicalSkills: ['Java', 'Angular', 'Python'],
    softSkills: ['Teamwork', 'Communication'],
    projects: ['Web Portal', 'AI Chatbot'],
    resumeLink: '#',
    linkedin: 'https://linkedin.com/in/shubham'
  };
}
