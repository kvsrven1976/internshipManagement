import { Routes } from '@angular/router';
import { Home } from './home/home';
import { IconGrid } from './icon-grid/icon-grid';
import { Contact } from './contact/contact';
import { About } from './about/about';
import { Register } from './register/register';
import { Login } from './login/login';
import { StudentHomepage } from './student-homepage/student-homepage';
import { StudentProfile } from './student-profile/student-profile';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  {
    path: 'home',
    component: Home,
    children: [
      { path: '', component: About }, // default inside home
      { path: 'contact', component: Contact }, // nested contact
      { path: 'about', component: About }, // nested about
      { path: 'register', component: Register }, // nested about
      { path: 'projects', component: IconGrid }, // default inside home
      { path: 'login', component: Login }, // default inside home
    ]
  },
  {
    path:"student",
    component:StudentHomepage,
    children:[
      {path:"profile",component:StudentProfile}
    ]  }

];
