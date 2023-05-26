import { Component } from '@angular/core';
import { Subscription } from 'rxjs';
import { AuthService } from '../Helpers/Auth/auth.service';
import { delay, startWith } from 'rxjs/operators';
import { AuthenticationService } from '../Core/Service/authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  subscription: Subscription;
  authentication: boolean;
  sidenavSticky: boolean = true;
  photo: string = null;
  sidenavModeSticky = "over"; //side, push, over
  sidenavModeOnScreen = "push"; //side, push, over
  EmployeeName: string = "Guest Access";
  EmployeeRole: string = "Guest";
  constructor(private authenticationService: AuthenticationService) {
  }
  ngAfterViewInit() {
    this.subscription = this.authenticationService.IsAuthenticated().subscribe(resp => {
      if (resp) {
        this.authenticationService.GetUserInfo().subscribe(resp => {
          if (resp) {
            this.EmployeeName = resp.FirstName + " " + resp.LastName;
            this.EmployeeRole = resp.Role.Name;
          }
        }, error => { 
            console.error(error);
        });
        this.authenticationService.GetUserPhoto().subscribe(response => {
          this.photo = "data:" + response.OriginalFormat + ";base64," + response.ImageFile;

        }, error => { 
            console.error(error);
        }) 
      }
    });
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
