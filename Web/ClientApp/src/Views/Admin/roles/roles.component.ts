import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { List } from 'linqts';
import { Claim } from '../../../Core/Models/claim';
import { Role } from '../../../Core/Models/role';
import { RoleService } from '../../../Core/Service/role.service';
import { UpsertRoleModalComponent } from './upsert-role-modal/upsert-role-modal.component';

class RoleClaimSetup {
  claim: Claim;
  checked: boolean;
  get Name(): string {
    return this.claim.Name;
  }
  get Id(): number {
    return this.claim.Id;
  }
}



@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html', 
  styleUrls: ['./roles.component.scss']
})
export class RolesComponent implements OnInit, AfterViewInit  {
  loadingRoles: boolean = true;
  loadingClaims: boolean = true;
  loadingRoleClaims: boolean = true;
  errorRole: string = null;
  errorClaims: string = null;
  errorRoleClaim: string = null;

  displayedColumns: string[] = ['Id', 'Name'];
  dataSource : MatTableDataSource<Role>; 
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  claims: List<Claim>;
  roleClaims: RoleClaimSetup[];
  selectedRole: Role = null;
  constructor(private _service: RoleService, public dialog: MatDialog) { }

  ngOnInit(): void {
  }
  loadClaims(): void {
    this.loadingClaims = true;
    this.errorClaims = null;
    this._service.GetClaims().subscribe(resp => {
      this.claims = new List<Claim>(resp);
      console.log(resp);
      this.loadingClaims = false;
    }, error => {
        this.errorClaims = error;

    });
  }
  loadRoles(): void {
    this.loadingRoles = true;
    this.errorRole = null;
    this._service.GetRoles().subscribe(resp => {
      this.dataSource = new MatTableDataSource<Role>(resp);
      this.dataSource.paginator = this.paginator;
      this.sort.direction = 'asc';
      this.sort.active = 'name';
      this.dataSource.sort = this.sort;
      this.loadingRoles = false;
    }, error => {
        this.errorRole = error;
    });
  }

  selectRole(role: Role): void {
    this.roleClaims = [];
    if (this.selectedRole && role.Id == this.selectedRole.Id) {
      this.selectedRole = null;
    } else {
      this.selectedRole = role;
      let a = new List<Claim>(role.Claims);
      this.claims.ForEach(x => {
        let newRoleClaim = new RoleClaimSetup();
        newRoleClaim.claim = x;
        newRoleClaim.checked = a.Any(claim => claim.Id == x.Id);
        this.roleClaims.push(newRoleClaim);
      });
    }
  }

  ngAfterViewInit() {
    this.loadRoles();
    this.loadClaims();
    


  }

  UpdateClaim(rClaim: RoleClaimSetup) {
    if (rClaim.checked) {
      this._service.DeleteRoleClaim(this.selectedRole.Id, rClaim.Id).subscribe(resp => {
       
      }, error => {
          this.errorRoleClaim = error;
      });
      //delete
    } else {
      this._service.AddRoleClaim(this.selectedRole.Id, rClaim.Id).subscribe(resp => {

      }, error => {
        this.errorRoleClaim = error;
      });
      //add
    }
  }
  upserRole(role: Role) {
    if (!role) {
      role = new Role();
      role.Id = -1;
    }
    let dialogRef = this.dialog.open(UpsertRoleModalComponent, {
      width: '300px',
      data: role,
      panelClass: 'widget-container'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.cancel) return;
      this.loadingRoles = true;
      if (result.save) {
        if (role.Id == -1) {
          this._service.AddRole(role).subscribe(result => {
            this.loadRoles();
          }, error => {
              this.errorRole = error;
          });
        } else {
          this._service.UpdateRole(role).subscribe(result => {
            this.loadRoles();
          }, error => {
              this.errorRole = error;
          });
        }
      }
      if (result.delete) {
        this._service.DeleteRole(role).subscribe(result => {
          this.loadRoles();
        }, error => {
            this.errorRole = error;

        });
      }
    });
  }

}
