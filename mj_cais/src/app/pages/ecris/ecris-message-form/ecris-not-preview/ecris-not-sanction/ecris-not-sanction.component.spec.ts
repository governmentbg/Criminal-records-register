import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcrisNotSanctionComponent } from './ecris-not-sanction.component';

describe('EcrisNotSanctionComponent', () => {
  let component: EcrisNotSanctionComponent;
  let fixture: ComponentFixture<EcrisNotSanctionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EcrisNotSanctionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EcrisNotSanctionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
