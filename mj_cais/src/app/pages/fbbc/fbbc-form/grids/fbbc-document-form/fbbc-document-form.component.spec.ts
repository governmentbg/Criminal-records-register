import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FbbcDocumentFormComponent } from './fbbc-document-form.component';

describe('FbbcDocumentFormComponent', () => {
  let component: FbbcDocumentFormComponent;
  let fixture: ComponentFixture<FbbcDocumentFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FbbcDocumentFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FbbcDocumentFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
