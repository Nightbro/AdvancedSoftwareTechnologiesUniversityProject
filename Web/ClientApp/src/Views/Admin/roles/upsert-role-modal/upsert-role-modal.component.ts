import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Role } from '../../../../Core/Models/role';

@Component({
  selector: 'app-upsert-role-modal',
  templateUrl: './upsert-role-modal.component.html',
  styleUrls: ['./upsert-role-modal.component.scss']
})
export class UpsertRoleModalComponent implements OnInit {
  error: string = "";
  form : FormGroup = new FormGroup({
    title: new FormControl('', [Validators.required])
  });
  get titleInput() { return this.form.get('title'); }

  constructor(public dialogRef: MatDialogRef<UpsertRoleModalComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Role) {
    this.titleInput.setValue(this.data.Name);
  }

  ngOnInit(): void {
  }

  OnSave(): void {
    if (!this.form.valid) return;
    this.data.Name = this.titleInput.value;
    this.dialogRef.close({save:true, data: this.data })
  }
  OnDelete(): void {
    this.dialogRef.close({ delete: true, data: this.data })
  }
  OnCancel(): void {
    this.dialogRef.close({ cancel: true });
  }

}
