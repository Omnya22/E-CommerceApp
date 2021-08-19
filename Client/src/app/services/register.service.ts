import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RegisterModel } from './../models/register-model';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { LoginModel } from '../models/login-model';
import { Users } from '../models/users';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {

  constructor(private http: HttpClient) { }
  baseUrl = "https://localhost:44382/Account/";
  headers = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
    withCredentials: true,
  };

  Register(model:RegisterModel):Observable<RegisterModel>{

    return this.http.post<RegisterModel>(this.baseUrl+'Register',model,this.headers).pipe();
  }

  Login(log: LoginModel): Observable<LoginModel> {
    return this.http.post<LoginModel>(this.baseUrl + 'Login', log,this.headers).pipe();
  }

  Logout() {
    return this.http.get(this.baseUrl + 'Logout', { withCredentials: true }).pipe();
  }

  GetAllUsers(): Observable<Users[]> {
    return this.http.get<Users[]>(this.baseUrl + 'GetAllUsers').pipe();
  }


  UserNameExist(username: string) {
    return this.http.get(this.baseUrl + 'UserExists?username=' + username).pipe();
  }

  EmailExist(email: string) {
    return this.http.get(this.baseUrl + 'EmailExists?email=' + email).pipe();
  }
}
