import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationForSigningComponent } from './application-for-signing.component';

describe('ApplicationForSigningComponent', () => {
  let component: ApplicationForSigningComponent;
  let fixture: ComponentFixture<ApplicationForSigningComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationForSigningComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationForSigningComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
