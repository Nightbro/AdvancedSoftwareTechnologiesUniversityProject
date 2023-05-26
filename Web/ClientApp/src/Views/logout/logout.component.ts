import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../Core/Service/authentication.service';
import { AuthService } from '../../Helpers/Auth/auth.service';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.scss']
})
export class LogoutComponent implements OnInit, OnDestroy {

  constructor(private router: Router, private helpers: AuthService, private authService: AuthenticationService) { }
  ngOnInit() {
 
         this.authService.Logout().subscribe(resp => {
           window.location.reload();

         });
    
  }
  ngOnDestroy() {
    
  }
}
