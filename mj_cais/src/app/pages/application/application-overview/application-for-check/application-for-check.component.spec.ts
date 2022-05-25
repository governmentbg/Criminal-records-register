import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationForCheckComponent } from './application-for-check.component';

describe('ApplicationForCheckComponent', () => {
  let component: ApplicationForCheckComponent;
  let fixture: ComponentFixture<ApplicationForCheckComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationForCheckComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationForCheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
