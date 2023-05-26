import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Medicine } from '../../../../Core/Models/medicine';
import { Role } from '../../../../Core/Models/role';

@Component({
  selector: 'app-add-medicine-modal',
  templateUrl: './add-medicine.component.html',
  styleUrls: ['./add-medicine.component.scss']
})
export class AddMedicineModalComponent implements OnInit {
  error: string = "";
  form : FormGroup = new FormGroup({
    barcode: new FormControl('', [Validators.required]),
    name: new FormControl('', [Validators.required]),
    description: new FormControl('', [Validators.required])
  });
  get barcodeInput() { return this.form.get('barcode'); }
  get nameInput() { return this.form.get('name'); }
  get descriptionInput() { return this.form.get('description'); }

  constructor(public dialogRef: MatDialogRef<AddMedicineModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Medicine) {
  }

  ngOnInit(): void {
  }

  OnSave(): void {
    if (!this.form.valid) return;
    this.data.Id = this.barcodeInput.value;
    this.data.Name = this.nameInput.value;
    this.data.Description = this.descriptionInput.value;
    this.dialogRef.close({save:true, data: this.data })
  }
  OnCancel(): void {
    this.dialogRef.close({ cancel: true });
  }

}
