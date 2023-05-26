import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { UpsertRoleModalComponent } from './upsert-role-modal.component';

describe('UpsertRoleModalComponent', () => {
  let component: UpsertRoleModalComponent;
  let fixture: ComponentFixture<UpsertRoleModalComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ UpsertRoleModalComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UpsertRoleModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
