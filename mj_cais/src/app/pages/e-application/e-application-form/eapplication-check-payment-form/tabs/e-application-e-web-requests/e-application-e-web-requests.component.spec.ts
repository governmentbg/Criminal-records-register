import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EApplicationEWebRequestsComponent } from './e-application-e-web-requests.component';

describe('EApplicationEWebRequestsComponent', () => {
  let component: EApplicationEWebRequestsComponent;
  let fixture: ComponentFixture<EApplicationEWebRequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EApplicationEWebRequestsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EApplicationEWebRequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
