import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Medicine } from '../../../Core/Models/medicine';
import { PharmacyService } from '../../../Core/Service/pharmacy.service';
import { Config } from '../../../Helpers/config';
import { AddMedicineModalComponent } from './add-medicine/add-medicine.component';

@Component({
  selector: 'app-medicine',
  templateUrl: './medicine.component.html',
  styleUrls: ['./medicine.component.scss']
})
export class MedicineComponent implements OnInit, AfterViewInit {
  loading: boolean = true;
  error: string = null;

  displayedColumns: string[] = ['Id', 'Name'];
  dataSource: MatTableDataSource<Medicine>;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  numberRegEx = /\d*/;

  form: FormGroup = new FormGroup({
    file: new FormControl(''),
    barcode: new FormControl({ disabled: true}),
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required])
  });
  get barcodeInput() { return this.form.get('barcode'); }
  get nameInput() { return this.form.get('name'); }
  get descriptionInput() { return this.form.get('description'); }
  selectedMedicine: Medicine = null;
  formData: FormData = null;

  constructor(private _service: PharmacyService, public dialog: MatDialog, private _snackBar: MatSnackBar) { }

  ngOnInit(): void {
  }

  loadMedicine() {
    this.loading = true;
    this.selectedMedicine = null;
    this.error = null;
    this._service.GetMedicine().subscribe(resp => {
      this.dataSource = new MatTableDataSource<Medicine>(resp);
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

  selectMedicine(row: Medicine) {
    if (this.selectedMedicine && this.selectedMedicine.Id == row.Id) {
      this.selectedMedicine = null;
    } else {
      this.selectedMedicine = row;
      this.formData = new FormData();
      this.barcodeInput.setValue(this.selectedMedicine.Id);
      this.nameInput.setValue(this.selectedMedicine.Name);
      this.descriptionInput.setValue(this.selectedMedicine.Description);
    }
  }

  get photo() {
    return "data:" + this.selectedMedicine.OriginalFormat + ";base64," + this.selectedMedicine.ImageFile;
  }

  add() {
    let newMed = new Medicine();

    let dialogRef = this.dialog.open(AddMedicineModalComponent, {
      width: '300px',
      data: newMed,
      panelClass: 'widget-container-modal'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.cancel) return;
    
      if (result.save) {
        newMed.ImageName = newMed.Name;
        newMed.OriginalFormat = "image/jpg";
        newMed.ImageFile = Config.getDummyMedicineImage;
        this.loading = true;
        this._service.AddMedicine(newMed).subscribe(result => {
          this.loadMedicine();
        }, error => {
          this.error = error;
        });
      }
    });
  }

  OnFileChange(event): void {
    if (event.target.files.length > 0) {
      const file = (event.target.files as FileList)[0];
      this.formData.append("image", file, this.selectedMedicine.Name);
    }
  }


  UpdateData(): void {
    let data: Medicine = { ...this.selectedMedicine };
    data.Name = this.nameInput.value;
    data.Description = this.descriptionInput.value;
    data.Id = this.barcodeInput.value;

    this.formData.append("medicine", JSON.stringify(data));

    this._service.UpdateMedicine(this.formData).subscribe(resp => {
      this._snackBar.open("Medicine has been updated", "Ok", { duration: 2000 });
      this.loadMedicine();
    }, error => {
      this._snackBar.open("Couldnt update medicine, error: " + error, "Ok");
    });
  }

  DeleteData(): void {
    this._service.DeleteMedicine(this.selectedMedicine).subscribe(resp => {
      this._snackBar.open("Medicine has been deleted", "Ok", { duration: 2000 });
      this.loadMedicine();
    }, error => {
      this._snackBar.open("Couldnt delete  medicine, error: " + error, "Ok");
    });
  }
}
