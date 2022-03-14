import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CaisGridPagerComponent } from './cais-grid-pager.component';

describe('CaisGridPagerComponent', () => {
  let component: CaisGridPagerComponent;
  let fixture: ComponentFixture<CaisGridPagerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CaisGridPagerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CaisGridPagerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
