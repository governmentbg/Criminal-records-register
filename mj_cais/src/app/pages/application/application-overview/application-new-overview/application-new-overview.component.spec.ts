import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationNewOverviewComponent } from './application-new-overview.component';

describe('ApplicationNewOverviewComponent', () => {
  let component: ApplicationNewOverviewComponent;
  let fixture: ComponentFixture<ApplicationNewOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationNewOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationNewOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
