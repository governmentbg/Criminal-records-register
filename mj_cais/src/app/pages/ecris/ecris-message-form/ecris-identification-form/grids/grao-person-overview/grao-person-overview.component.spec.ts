import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GraoPersonOverviewComponent } from './grao-person-overview.component';

describe('GraoPersonOverviewComponent', () => {
  let component: GraoPersonOverviewComponent;
  let fixture: ComponentFixture<GraoPersonOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GraoPersonOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GraoPersonOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
