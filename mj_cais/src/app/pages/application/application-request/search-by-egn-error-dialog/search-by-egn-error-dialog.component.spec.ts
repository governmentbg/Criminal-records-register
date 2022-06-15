import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchByEgnErrorDialogComponent } from './search-by-egn-error-dialog.component';

describe('SearchByEgnDialogComponent', () => {
  let component: SearchByEgnErrorDialogComponent;
  let fixture: ComponentFixture<SearchByEgnErrorDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchByEgnErrorDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchByEgnErrorDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
