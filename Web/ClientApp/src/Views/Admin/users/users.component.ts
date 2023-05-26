import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { User } from '../../../Core/Models/user';
import { AuthenticationService } from '../../../Core/Service/authentication.service';
import { UserService } from '../../../Core/Service/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements AfterViewInit {
  loadingUsers: boolean = true;
  errorUsers: string = null;
  canDelete = true;
  displayedColumns: string[] = ['Id', 'FirstName', 'LastName'];
  public selectedUser: User = null;
  dataSource: MatTableDataSource<User>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  constructor(private authService: AuthenticationService, private userService: UserService, private _snackBar: MatSnackBar) { }

  ngAfterViewInit() {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loadingUsers = true;
    this.errorUsers = null;
    this.selectedUser = null;
    this.userService.GetAllUsers().subscribe(resp => {
      this.dataSource = new MatTableDataSource<User>(resp);
      this.dataSource.paginator = this.paginator;
      this.sort.direction = 'asc';
      this.sort.active = 'LastName';
      this.dataSource.sort = this.sort;
      this.loadingUsers = false;
    }, error => {
        this.errorUsers = error;

    });
  }
  addNewUser() {
    this.selectedUser = new User();
    this.selectedUser.Id = 0;
    this.selectedUser.RoleID = 1;
    
  }
  upsertUser(user: User) {

  }
  selectUser(user: User) {
    if (this.selectedUser && user.Id == this.selectedUser.Id) {
      this.selectedUser = null;
    } else {
      this.selectedUser = user;
    }
  } 
}
