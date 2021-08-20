import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  constructor(
    private service: RegisterService,
    private route: Router,
    private auth: AuthService
  ) { }

  title = 'E-commerce App';
  ngOnInit() {
    if (this.isUserRegistered()) {
        this.Logout();
    }
  }

  Logout() {
    this.service.Logout().subscribe(succ => {
      localStorage.clear();
      console.log('authoization return false');
      this.route.navigate(['']).then(x=> {window.location.reload()});
    }, err => console.log((err))
    );
  }


  isUserRegistered() {
    const email = !!localStorage.getItem('email');
    const role = !!localStorage.getItem('role');
    if (email) {
      return true;
    }
    return false;
  }

}
