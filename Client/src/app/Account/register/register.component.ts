import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { RegisterModel } from './../../models/register-model';
import { RegisterService } from './../../services/register.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private fb:FormBuilder,private route: Router,private service:RegisterService) { }

  userForm: FormGroup;

  model: RegisterModel;
  message: string;

  isbusy: boolean;

  messageValidate = {
    userName: {
      required: 'User Name is Required.',
      matchUserName: '',
    },
    email: {
      required: 'Email is Required.',
      notValid: 'Email is not correct!',
      matchEmail: ''
    },
    password: {
      required: 'Password is Required.',
      minLength: 'Password should be more than 5 char or digit',
      notMatch: '',
    },
    passwordConfirm: {
      required: 'Password Confirm is Required.',
      minLength: 'Password should be more than 5 char or digit',
      isMatch: 'Password not match.'
    }
  };

  ngOnInit(): void {
    this.isbusy = false;
    this.message = '';
    this.model={
      userName:'',
      email:'',
      password:''
    };

    this.userForm = this.fb.group({
      userName:['',[Validators.required]],
      email:['',[Validators.required,Validators.email]],
      password:['',[Validators.required,Validators.minLength(6)]],
      passwordConfirm:['',[Validators.required,Validators.minLength(6)]]
    });


    this.userForm.valueChanges.subscribe(x => {
      if (this.userForm.status == 'VALID') {
        this.isbusy = true;
      }
    }, ex => console.log(ex))

  }

  validateRegisterModel(){
    this.model.userName = this.userForm.value.userName;
    this.model.email = this.userForm.value.email;
    this.model.password = this.userForm.value.password;
  }

  isPasswordMatch() {
    if (this.userForm.value.password !== '' && this.userForm.value.passwordConfirm !== '') {
      if ((this.userForm.value.password !== this.userForm.value.passwordConfirm) &&
        this.userForm.value.password.length > 5 && this.userForm.value.passwordConfirm.length > 5)
          return true;
    }
    return false;
  }

  isUserNameExist() {
    const name = this.userForm.value.userName;
    if (name != null && name != '' && this.isbusy === false) {
      this.service.UserNameExist(name).subscribe(x => {
        this.messageValidate.userName.matchUserName = 'This User name is used before!';
      }, ex => console.log(ex));
      return true;
    } else {
      this.messageValidate.userName.matchUserName = null;
    }
    return false;
  }

  isEmailExist() {
    const email = this.userForm.value.email;
    if (email != null && email != '' && this.isbusy === false) {
      this.service.EmailExist(email).subscribe(x => {
        this.messageValidate.email.matchEmail = 'This Email is used before!';
      }, ex => console.log(ex));
      return true;
    } else {
      this.messageValidate.email.matchEmail = null;
    }
    return false;
  }

  register() {
    if (this.userForm.valid) {
      this.validateRegisterModel();
      this.service.Register(this.model).subscribe(succes => {
        this.message = "Registration Success.";
        this.userForm.reset();
        this.userForm.value.password = '';
        this.route.navigate(['Login']);
      }, err => console.log(err));
    }
  }


}
