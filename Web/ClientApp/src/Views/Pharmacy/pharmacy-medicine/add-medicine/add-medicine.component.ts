import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Medicine } from '../../../../Core/Models/medicine';
import { Role } from '../../../../Core/Models/role';
import { List } from 'linqts';
import { PharmacyMedicine } from '../../../../Core/Models/pharmacy';
import { PharmacyService } from '../../../../Core/Service/pharmacy.service';
import { MatSnackBar } from '@angular/material/snack-bar';


export class AddMedicineData {
  medicines: List<Medicine>;
  isNew: boolean = false;
  current: PharmacyMedicine;
}


@Component({
  selector: 'app-add-medicine-in-pharmacy--modal',
  templateUrl: './add-medicine.component.html',
  styleUrls: ['./add-medicine.component.scss']
})
export class AddMedicineInPharmacyModalComponent implements OnInit {
  error: string = "";
  form: FormGroup = new FormGroup({
    barcode: new FormControl('', [Validators.required]),
    price: new FormControl('', [Validators.required]),
    quantity: new FormControl('', [Validators.required])
  });
  get barcodeInput() { return this.form.get('barcode'); }
  get nameInput() { return this.form.get('price'); }
  get quantityInput() { return this.form.get('quantity'); }

  constructor(public dialogRef: MatDialogRef<AddMedicineInPharmacyModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddMedicineData, private service: PharmacyService, private _snackBar: MatSnackBar) {

    this.barcodeInput.setValue(data.current.IdMedicine);
    this.nameInput.setValue(data.current.Price);
    this.quantityInput.setValue(data.current.Quantity);
  }

  ngOnInit(): void {
  }

  OnSave(): void {
    if (!this.form.valid) return;
    this.data.current.IdMedicine = this.barcodeInput.value;
    this.data.current.Price = this.nameInput.value;
    this.data.current.Quantity = this.quantityInput.value;

    this.service.UpdatePharmacyMedicine(this.data.current).subscribe(resp => {
      this.dialogRef.close({ save: true, data: this.data })

    }, error => {
      this._snackBar.open("Couldnt update medicine for pharmacy,reason:" + error, "OK")
    });
  }
  OnCancel(): void {
    this.dialogRef.close({ cancel: true });
  }

}
