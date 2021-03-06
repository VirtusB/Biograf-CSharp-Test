import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/alertify.service';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  user: User;
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;

  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private fb: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.bsConfig = {
      containerClass: 'theme-default'
    },
    this.createRegisterForm();
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      username: ['', Validators.required],
      name: ['', Validators.required],
      city: ['', Validators.required],
      country: ['', Validators.required],
      email: ['', Validators.required],
      phoneNumber: ['', [Validators.required, Validators.minLength(8), Validators.maxLength(20)]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]],
      confirmPassword: ['', Validators.required]
    }, {validator: this.passwordMatchValidator});
  }

  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : {'mismatch': true};
  }

  register() {
    if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(() => {
        this.alertify.success('Din konto blev oprettet');
      }, error => {
        this.alertify.error(error);
      }, () => {
        this.authService.login(this.user).subscribe(() => {
          this.router.navigate(['/movies']);
        });
      });
    }

  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  get username() {
    return this.registerForm.get('username');
   }

   get name() {
    return this.registerForm.get('name');
   }

   get city() {
    return this.registerForm.get('city');
   }

   get country() {
    return this.registerForm.get('country');
   }

   get email() {
    return this.registerForm.get('email');
   }

   get phoneNumber() {
    return this.registerForm.get('phoneNumber');
   }

   get password() {
    return this.registerForm.get('password');
   }

   get confirmPassword() {
    return this.registerForm.get('confirmPassword');
   }

}
