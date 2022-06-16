import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationEWebRequestsComponent } from './application-e-web-requests.component';

describe('ApplicationEWebRequestsComponent', () => {
  let component: ApplicationEWebRequestsComponent;
  let fixture: ComponentFixture<ApplicationEWebRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationEWebRequestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationEWebRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
