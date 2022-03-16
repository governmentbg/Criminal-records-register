import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultipleChooseComponent } from './multiple-choose.component';

describe('MultipleChooseComponent', () => {
  let component: MultipleChooseComponent;
  let fixture: ComponentFixture<MultipleChooseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultipleChooseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MultipleChooseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
