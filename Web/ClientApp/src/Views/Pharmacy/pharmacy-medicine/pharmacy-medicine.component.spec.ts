import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PharmacyMedicineComponent } from './pharmacy-medicine.component';

describe('PharmacyMedicineComponent', () => {
  let component: PharmacyMedicineComponent;
  let fixture: ComponentFixture<PharmacyMedicineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PharmacyMedicineComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PharmacyMedicineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
