import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcrisNotDescisionComponent } from './ecris-not-descision.component';

describe('EcrisNotDescisionComponent', () => {
  let component: EcrisNotDescisionComponent;
  let fixture: ComponentFixture<EcrisNotDescisionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EcrisNotDescisionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EcrisNotDescisionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
