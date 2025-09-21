import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-register',
  imports: [ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  profileForm!: FormGroup;
  constructor(private fb: FormBuilder) { }
  ngOnInit(): void {
    this.profileForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      countryCode: ['+91'], // default
      mobile: ['', [Validators.required, Validators.pattern('^[0-9]{7,15}$')]],
      gender: [''],
      category: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.profileForm.valid) {
      console.log('Form value', this.profileForm.value);
      // TODO: call your API or emit event
    } else {
      this.profileForm.markAllAsTouched();
    }
  }
}
