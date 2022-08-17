import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchInquiryOverviewComponent } from './search-inquiry-overview.component';

describe('SearchInquiryOverviewComponent', () => {
  let component: SearchInquiryOverviewComponent;
  let fixture: ComponentFixture<SearchInquiryOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchInquiryOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchInquiryOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
