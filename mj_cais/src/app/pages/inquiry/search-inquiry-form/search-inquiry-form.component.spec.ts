import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchInquiryFormComponent } from './search-inquiry-form.component';

describe('SearchInquiryFormComponent', () => {
  let component: SearchInquiryFormComponent;
  let fixture: ComponentFixture<SearchInquiryFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchInquiryFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchInquiryFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
