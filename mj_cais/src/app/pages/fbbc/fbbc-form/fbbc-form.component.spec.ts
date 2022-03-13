import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FbbcFormComponent } from './fbbc-form.component';

describe('FbbcFormComponent', () => {
  let component: FbbcFormComponent;
  let fixture: ComponentFixture<FbbcFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FbbcFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FbbcFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
