import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EApplicationStatusHistoryComponent } from './e-application-status-history.component';

describe('EApplicationStatusHistoryComponent', () => {
  let component: EApplicationStatusHistoryComponent;
  let fixture: ComponentFixture<EApplicationStatusHistoryComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EApplicationStatusHistoryComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EApplicationStatusHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
