import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FbbcOverviewComponent } from './fbbc-overview.component';

describe('FbbcOverviewComponent', () => {
  let component: FbbcOverviewComponent;
  let fixture: ComponentFixture<FbbcOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FbbcOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FbbcOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
