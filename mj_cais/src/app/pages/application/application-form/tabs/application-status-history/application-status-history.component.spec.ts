import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationStatusHistoryComponent } from './application-status-history.component';

describe('ApplicationStatusHistoryComponent', () => {
  let component: ApplicationStatusHistoryComponent;
  let fixture: ComponentFixture<ApplicationStatusHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationStatusHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationStatusHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
