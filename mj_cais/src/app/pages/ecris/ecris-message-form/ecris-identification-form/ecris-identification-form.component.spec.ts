import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcrisIdentificationFormComponent } from './ecris-identification-form.component';

describe('EcrisIdentificationFormComponent', () => {
  let component: EcrisIdentificationFormComponent;
  let fixture: ComponentFixture<EcrisIdentificationFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EcrisIdentificationFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EcrisIdentificationFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
