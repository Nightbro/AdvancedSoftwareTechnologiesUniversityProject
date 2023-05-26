import { AfterViewInit, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Pharmacy, PharmacyMedicine } from '../../../Core/Models/pharmacy';
import { AuthenticationService } from '../../../Core/Service/authentication.service';
import { PharmacyService } from '../../../Core/Service/pharmacy.service';
import { List } from 'linqts';
import { Claim } from '../../../Core/Models/claim';
import { forkJoin } from 'rxjs';
import { Medicine } from '../../../Core/Models/medicine';
import { AddMedicineData, AddMedicineInPharmacyModalComponent } from './add-medicine/add-medicine.component';

@Component({
  selector: 'app-pharmacy-medicine',
  templateUrl: './pharmacy-medicine.component.html',
  styleUrls: ['./pharmacy-medicine.component.scss']
})
export class PharmacyMedicineComponent implements OnInit, AfterViewInit {

  loading: boolean = true;
  error: string = null;

  displayedColumns: string[] = ['Id', 'Name'];
  displayedColumnsMedicine: string[] = ['IdMedicine', 'Price', 'Quantity'];
  dataSource: MatTableDataSource<Pharmacy>;
  dataSourcePharmacy: MatTableDataSource<PharmacyMedicine>;
  @ViewChild('pharmacyPaginator') paginator: MatPaginator;
  @ViewChild('medicineSort') medicineSort: MatSort;
  @ViewChild('medicinePaginator') medicinePaginator: MatPaginator;
  @ViewChild('pharmacySort') sort: MatSort;
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
  medicine: List<Medicine> = null;

  constructor(private _service: PharmacyService, public dialog: MatDialog, private _snackBar: MatSnackBar, private _authService: AuthenticationService) { }

  ngOnInit(): void {
  }
  claims: List<Claim> = null;


  getMedicineName(id: number) {
    return this.medicine.Where(x => x.Id == id).First().Name;
  }
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
    forkJoin([this._service.GetPharmacies(), this._service.GetMedicine()]).subscribe(result => {
      let pharmacies = new List<Pharmacy>(result[0]);
      this.medicine = new List<Medicine>(result[1]);
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
      this.dataSourcePharmacy = new MatTableDataSource<PharmacyMedicine>(row.Medicines);
      this.dataSourcePharmacy.paginator = this.medicinePaginator;
      this.medicineSort.direction = 'asc';
      this.medicineSort.active = 'name';
      this.dataSourcePharmacy.sort = this.medicineSort;
    }
  }

  add(): void {
    let newData = new Pharmacy();
    newData.Id = 0;
    this.select(newData);
  }

  downloadSample(): void {

  }

  upload(): void {

  }

  dataForm: FormGroup = new FormGroup({
    file: new FormControl('', [Validators.required])
  });

  OnFileChange(event): void {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      const formData = new FormData();
      formData.append("CSVUpload", file);
      this._service.Upload(formData).subscribe(resp => {
        this.dataForm.get("file").setValue("");
        this._snackBar.open("Data has been processed succesfuly", "OK", { duration: 2000 });
        this.loadMedicine();
      }, error => {
        this._snackBar.open("Couldnt process the CSV file, error: " + error, "Ok");

      });
    }
  }
  updateMedicine(row: PharmacyMedicine) {
    let data = new AddMedicineData()
    if (row == null) {
      row = new PharmacyMedicine();
      row.IdPharmacy = this.selected.Id;
      data.isNew = true;
      data.medicines = this.medicine;

    }
    data.current = row;


    let dialogRef = this.dialog.open(AddMedicineInPharmacyModalComponent, {
      width: '300px',
      data: data,
      panelClass: 'widget-container-modal',
      hasBackdrop: false
    });

    dialogRef.afterClosed().subscribe(resp => {
      if (resp.save) this.loadMedicine();
    })
  }
}
