import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmTemplateDialogComponent } from './confirm-template-dialog.component';

describe('ConfirmTemplateDialogComponent', () => {
  let component: ConfirmTemplateDialogComponent;
  let fixture: ComponentFixture<ConfirmTemplateDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfirmTemplateDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmTemplateDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
