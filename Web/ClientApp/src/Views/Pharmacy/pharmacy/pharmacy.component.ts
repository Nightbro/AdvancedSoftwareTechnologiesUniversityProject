import { AfterViewInit, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Pharmacy } from '../../../Core/Models/pharmacy';
import { AuthenticationService } from '../../../Core/Service/authentication.service';
import { PharmacyService } from '../../../Core/Service/pharmacy.service';
import { List } from 'linqts';
import { Claim } from '../../../Core/Models/claim';

@Component({
  selector: 'app-pharmacy',
  templateUrl: './pharmacy.component.html',
  styleUrls: ['./pharmacy.component.scss']
})
export class PharmacyComponent implements OnInit, AfterViewInit {

  loading: boolean = true;
  error: string = null;

  displayedColumns: string[] = ['Id', 'Name'];
  dataSource: MatTableDataSource<Pharmacy>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  numberRegEx = /\d*/;

  form: FormGroup = new FormGroup({
    barcode: new FormControl({ disabled: true }),
    name: new FormControl('', [Validators.required]),
    longitude: new FormControl('', [Validators.required]),
    latitude: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required])
  });
  get nameInput() { return this.form.get('name'); }
  get latitudeInput() { return this.form.get('latitude'); }
  get longitudeInput() { return this.form.get('longitude'); }
  get descriptionInput() { return this.form.get('description'); }
  selected: Pharmacy = null;
  formData: FormData = null;

  constructor(private _service: PharmacyService, public dialog: MatDialog, private _snackBar: MatSnackBar, private _authService: AuthenticationService) { }

  ngOnInit(): void {
  }
  claims: List<Claim> = null;

  containsClaim(requestedClaimId: number) {
    if (!this.claims) {
      if (this._authService.user) {
        let role = this._authService.user.Role;
        this.claims = new List<Claim>(role.Claims);
      }

    }
    if (this.claims) return this.claims.Any(x => x.Id == requestedClaimId);
    return false;
  }


  loadMedicine() {
    this.loading = true;
    this.selected = null;
    this.error = null;
    this._service.GetPharmacies().subscribe(resp => {
      let pharmacies = new List<Pharmacy>(resp);

      if (this.containsClaim(4)) {
        //do nothing
      } else if (this.containsClaim(5)) {
        pharmacies = pharmacies.Where(x => x.OwnerId == this._authService.user.Id);
      } else {
        pharmacies = new List<Pharmacy>();
      }

      this.dataSource = new MatTableDataSource<Pharmacy>(pharmacies.ToArray());
      this.dataSource.paginator = this.paginator;
      this.sort.direction = 'asc';
      this.sort.active = 'name';
      this.dataSource.sort = this.sort;
      this.loading = false;
    }, error => {
      this.error = error;
    });
  }

  ngAfterViewInit(): void {
    this.loadMedicine();
  }

  select(row: Pharmacy) {
    if (this.selected && this.selected.Id == row.Id) {
      this.selected = null;
    } else {
      this.selected = row;
      this.nameInput.setValue(this.selected.Name);
      this.longitudeInput.setValue(this.selected.Longitude);
      this.latitudeInput.setValue(this.selected.Latitude);
      this.descriptionInput.setValue(this.selected.Description);
    }
  }

  add(): void {
    let newData = new Pharmacy();
    newData.Id = 0;
    this.select(newData);
  }

  UpdateData(): void {
    let data: Pharmacy = { ...this.selected };
    data.Name = this.nameInput.value;
    data.Description = this.descriptionInput.value;
    data.Latitude = this.latitudeInput.value;
    data.Longitude = this.longitudeInput.value;
    data.OwnerId = this._authService.user.Id;
   
      this._service.UpdatePharmacy(data).subscribe(resp => {
        this._snackBar.open("Data has been updated", "Ok", { duration: 2000 });
        this.loadMedicine();
      }, error => {
        this._snackBar.open("Couldnt update, error: " + error, "Ok");
      });
    
  }

  DeleteData(): void {
    this._service.DeletePharmacy(this.selected).subscribe(resp => {
      this._snackBar.open("Medicine has been deleted", "Ok", { duration: 2000 });
      this.loadMedicine();
    }, error => {
      this._snackBar.open("Couldnt delete  medicine, error: " + error, "Ok");
    });
  }

}
