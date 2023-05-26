import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Subscription } from 'rxjs';
import { Role } from '../../../Core/Models/role';
import { User } from '../../../Core/Models/user';
import { UserPhoto } from '../../../Core/Models/user-photo';
import { AuthenticationService } from '../../../Core/Service/authentication.service';
import { RoleService } from '../../../Core/Service/role.service';
import { UserService } from '../../../Core/Service/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  loading: boolean = true;
  @Input() userId: number = -1;
  @Input() canDelete = false;
  selectedRoleId: string = "1";
  getPhotoSub: Subscription;
  roles: Role[] = [];
  _user: User = this.authService.user;
  @Output() userDataUpdated = new EventEmitter<boolean>();
  get user(): User {
    return this._user;
  }
  @Input() set user(value: User) {
    this._user = value;
    if (this.getPhotoSub) this.getPhotoSub.unsubscribe();
    this.initializeUser();
  }
  hidePassword: boolean = true;
  photo: string = null;
  userInfo: FormGroup = new FormGroup({
    fname: new FormControl('', [Validators.required]),
    lname: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    username: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email])
  });
  pictureForm: FormGroup = new FormGroup({
    file: new FormControl('', [Validators.required])
  });   
  updatePassword: FormGroup = new FormGroup({
    password: new FormControl('', [Validators.required, Validators.minLength(6)])
  });  
  get passInput() { return this.updatePassword.get('password'); }
  get fnInput() { return this.userInfo.get('fname'); }
  get lnInput() { return this.userInfo.get('lname'); } 
  get emailInput() { return this.userInfo.get('email'); }
  constructor( public authService: AuthenticationService, private userService: UserService, private _snackBar: MatSnackBar, private _roleService: RoleService) { }

  initializeUser(): void {
    this.loading = true;
    this.fnInput.setValue(this._user.FirstName);
    this.lnInput.setValue(this._user.LastName);
    this.emailInput.setValue(this._user.Email);
    this.selectedRoleId = "1";
    if (this.user.Role)  this.selectedRoleId = this.user.Role.Id.toString();
    
    this.getPhoto();
    if (this.canDelete) {
      this._roleService.GetRoles().subscribe(resp => {
        this.roles = resp;
      }, error => {
          this._snackBar.open("There was an issue with loading Roles", "Ok", { duration: 2000 });
      });
    }

  }
  ngAfterViewInit(): void {
    this.initializeUser();

   
  } 
  ngOnInit(): void {
  }

  getPhoto(): void {
    if (this.user.Id != 0) {
      this.getPhotoSub = this.authService.GetUserPhoto(this._user.Id).subscribe(response => {
        this.photo = "data:" + response.OriginalFormat + ";base64," + response.ImageFile;
        this.loading = false;
      }, error => {
        this._snackBar.open("Couldnt get user photo, error: " + error, "Ok", { duration: 2000 });

      });
    }
    else {
      this.loading = false;
    }
  }
    
  updatePass(): void {
    this.userService.UpdateRole(this._user.UserName, this.passInput.value).subscribe(resp => {
      this._snackBar.open("Password saved ", "Ok", { duration: 10000 });

    }, error => {
        this._snackBar.open("Couldnt save password, error: " + error, "Ok", { duration: 10000 } );
    });
  }
  DeleteUser(): void {
    if (this.user.Id == 0) return;
    this.userService.DeleteUser(this._user.Id).subscribe(resp => {
      this._snackBar.open("User deleted", "Ok", { duration: 2000 });
      this.userDataUpdated.emit(true);


    }, error => {
        this._snackBar.open("Couldnt delete user, error: " + error, "Ok", { duration: 10000 });

    });
  }
  updateUserData(): void {
    let data: User = new User();
    data.Id = this._user.Id;
    data.Role = this._user.Role; 
    data.RoleID = this._user.RoleID;
    data.FirstName = this.fnInput.value;
    data.LastName = this.lnInput.value;
    data.Email = this.emailInput.value;
    data.UserName = this._user.UserName;
    if (this.canDelete && this.roles.length > 0) data.RoleID = +this.selectedRoleId;

    if (this.authService.user) {
      this.userService.UpdateUser(data).subscribe(resp => {
        this._snackBar.open("Data saved", "Ok", { duration: 2000 });
        this.userDataUpdated.emit(true);
      }, error => {
        this._snackBar.open("Couldnt save info, error: " + error, "Ok", { duration: 2000 });
      });
    } else {
      data.UserName = this.userInfo.get('username').value;
      this.userService.RegisterUser(data, this.userInfo.get('password').value).subscribe(resp => {
        window.location.reload();
      }, error => {
        this._snackBar.open("Couldnt save info, error: " + error, "Ok", { duration: 2000 });
      });
    }
  }

  OnFileChange(event): void {
    if (event.target.files.length > 0) { 
      const file = event.target.files[0];
      const formData = new FormData();
      formData.append(this._user.Id.toString(), file, this._user.UserName);
      this.userService.UploadPicture(formData).subscribe(resp => {
        this.getPhoto();
        this._snackBar.open("Photo is saved succesfuly", "Ok", { duration: 2000 });
        this.pictureForm.get('file').setValue("");
      }, error => {
          this._snackBar.open("Couldnt save user photo, error: " + error, "Ok", { duration: 2000 });

      });
    }
  }

    
}
