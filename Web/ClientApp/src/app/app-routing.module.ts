import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../Helpers/Auth/auth.guard';
import { RolesComponent } from '../Views/Admin/roles/roles.component';
import { UsersComponent } from '../Views/Admin/users/users.component';
import { ProfileComponent } from '../Views/Core/profile/profile.component';
import { MedicineComponent } from '../Views/Data/medicine/medicine.component';
import { HomeComponent } from '../Views/home/home.component';
import { LoginComponent } from '../Views/login/login.component';
import { LogoutComponent } from '../Views/logout/logout.component';
import { PharmacyMedicineComponent } from '../Views/Pharmacy/pharmacy-medicine/pharmacy-medicine.component';
import { PharmacyComponent } from '../Views/Pharmacy/pharmacy/pharmacy.component';
import { ProductsComponent } from '../Views/products/products.component';


const routes: Routes = [
  { path: '', redirectTo:'/home', pathMatch:'full' },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent, canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'products', component: ProductsComponent, canActivate: [AuthGuard] },
  { path: 'admin/roles', component: RolesComponent, canActivate: [AuthGuard] },
  { path: 'admin/users', component: UsersComponent, canActivate: [AuthGuard] },
  { path: 'data/medicine', component: MedicineComponent, canActivate: [AuthGuard] },
  { path: 'pharmacy/pharmacy', component: PharmacyComponent, canActivate: [AuthGuard] },
  { path: 'pharmacy/medicine', component: PharmacyMedicineComponent, canActivate: [AuthGuard] }


];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
