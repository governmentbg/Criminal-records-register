import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SearchByEgnDialogComponent } from './search-by-egn-dialog.component';

describe('SearchByEgnDialogComponent', () => {
  let component: SearchByEgnDialogComponent;
  let fixture: ComponentFixture<SearchByEgnDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SearchByEgnDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SearchByEgnDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
