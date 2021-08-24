import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/models/login-model';
import { AuthService } from 'src/app/services/auth.service';
import { RegisterService } from 'src/app/services/register.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

    constructor(private fb:FormBuilder,private route: Router,private service: RegisterService ,private auth: AuthService) { }

    message: string;
    loginForm: FormGroup;
    logModel: LoginModel;
    messageValidate = {
      email: {
        required: 'Email is Required.',
      },
      pass: {
        required: 'Password Confirm is Required.',
      },
    };

    ngOnInit() {
      this.message = '';

      this.logModel = {
        email: '',
        password: '',
      };

      this.loginForm = this.fb.group({
        email: ['', Validators.required],
        password: ['', Validators.required]
      });

    }

    Login() {
      if (this.loginForm.valid) {
        this.ValidateModel();
        this.service.Login(this.logModel).subscribe(success => {
          const email = this.loginForm.value.email;
          this.auth.installStorage(email);
          this.route.navigate(['Orders']);
        }, err => {
          console.log(err);
          this.message = "Login Failed";
        });
      }
    }

    ValidateModel() {
      this.logModel.email = this.loginForm.value.email;
      this.logModel.password = this.loginForm.value.password;
    }
  }
