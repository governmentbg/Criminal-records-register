import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationTaxFreeOverviewComponent } from './application-tax-free-overview.component';

describe('ApplicationTaxFreeOverviewComponent', () => {
  let component: ApplicationTaxFreeOverviewComponent;
  let fixture: ComponentFixture<ApplicationTaxFreeOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationTaxFreeOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationTaxFreeOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
