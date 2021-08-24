import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  email: string;

  constructor(
    private http: HttpClient) {
    if (this.isUserRegistered()) {
      this.email = localStorage.getItem('email');
    }
  }

  async installStorage(email: string) {
    localStorage.setItem('email', email);
  }

  isUserRegistered() {
    const email = !!localStorage.getItem('email');
    if (email)
      return true;
    return false;
  }

}
