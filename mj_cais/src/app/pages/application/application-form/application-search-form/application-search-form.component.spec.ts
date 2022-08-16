import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationSearchFormComponent } from './application-search-form.component';

describe('ApplicationSearchFormComponent', () => {
  let component: ApplicationSearchFormComponent;
  let fixture: ComponentFixture<ApplicationSearchFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationSearchFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationSearchFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
