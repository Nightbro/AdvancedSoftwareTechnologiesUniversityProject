import { AfterViewInit, Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Login } from '../../Core/Models/login';
import { Role } from '../../Core/Models/role';
import { User } from '../../Core/Models/user';
import { AuthenticationService } from '../../Core/Service/authentication.service';
import * as sha512 from 'js-sha512';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, AfterViewInit, OnDestroy {
  subscription: Subscription;

  hidePassword: boolean = true;
  error: string = "";
  signin: FormGroup = new FormGroup({
    username: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required])
  });
  get usernameInput() { return this.signin.get('username'); }
  get passwordInput() { return this.signin.get('password'); } 

  constructor(private router: Router, private authenticationService: AuthenticationService) { }

  ngOnInit(): void {

  }
  ngAfterViewInit(): void {
    this.subscription = this.authenticationService.IsAuthenticated().subscribe(resp => {
      if (resp) {
        this.router.navigate(['/home']);
      }
    });
  }
  login(): void {
    if (!this.signin.valid) return;
    let auth = new Login();
    auth.Username = this.usernameInput.value;
    auth.Password = sha512.sha512(this.passwordInput.value);
   // auth.Password = this.passwordInput.value;
    this.authenticationService.Authorize(auth).subscribe(token => {
      this.router.navigate(['/home']);
    }, error => {
        this.error = error;
    });
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  newUser: User = null;
  CreateAProfile() {
    this.newUser = new User();
    this.newUser.Id = 0;
    this.newUser.Role = new Role();
    this.newUser.Role.Id = 1;
    this.newUser.RoleID = 1;
  }
}
