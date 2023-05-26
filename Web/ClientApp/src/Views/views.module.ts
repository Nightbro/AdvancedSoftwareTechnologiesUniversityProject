import { AgmCoreModule } from '@agm/core';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from '../app/app-routing.module';
import { ExtraModule } from '../Core/extra/extra.module';
import { RolesComponent } from './Admin/roles/roles.component';
import { UpsertRoleModalComponent } from './Admin/roles/upsert-role-modal/upsert-role-modal.component';
import { UsersComponent } from './Admin/users/users.component';
import { LeftPanelComponent } from './Core/left-panel/left-panel.component';
import { ProfileComponent } from './Core/profile/profile.component';
import { AddMedicineModalComponent } from './Data/medicine/add-medicine/add-medicine.component';
import { MedicineComponent } from './Data/medicine/medicine.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { AddMedicineInPharmacyModalComponent } from './Pharmacy/pharmacy-medicine/add-medicine/add-medicine.component';
import { PharmacyMedicineComponent } from './Pharmacy/pharmacy-medicine/pharmacy-medicine.component';
import { PharmacyComponent } from './Pharmacy/pharmacy/pharmacy.component';
import { ProductsComponent } from './products/products.component';


const declarations = [
  LoginComponent,
  LoginComponent,
  HomeComponent,
  ProductsComponent,
  LeftPanelComponent,
  RolesComponent,
  UpsertRoleModalComponent,
  ProfileComponent,
  UsersComponent,
  MedicineComponent,
  PharmacyComponent,
  PharmacyMedicineComponent,
  AddMedicineModalComponent,
  AddMedicineInPharmacyModalComponent

]

@NgModule({
  declarations: declarations,
  imports: [
    CommonModule,
    AppRoutingModule,
    ExtraModule,
    AgmCoreModule.forRoot({
      //apiKey: 'AIzaSyCYaZJNTjh3HcM_pF-baen1e4zuicLy8ys'
      apiKey: 'AIzaSyAl5kWUn7Ar9Y_-ps5WJOFfse53JlPgIxI'
    })
  ],
  entryComponents: [UpsertRoleModalComponent, AddMedicineModalComponent, AddMedicineInPharmacyModalComponent],
  exports: declarations
})
export class ViewsModule { }
