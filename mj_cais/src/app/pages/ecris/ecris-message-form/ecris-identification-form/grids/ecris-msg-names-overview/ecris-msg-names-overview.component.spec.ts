import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EcrisMsgNamesOverviewComponent } from './ecris-msg-names-overview.component';

describe('EcrisMsgNamesOverviewComponent', () => {
  let component: EcrisMsgNamesOverviewComponent;
  let fixture: ComponentFixture<EcrisMsgNamesOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EcrisMsgNamesOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EcrisMsgNamesOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
