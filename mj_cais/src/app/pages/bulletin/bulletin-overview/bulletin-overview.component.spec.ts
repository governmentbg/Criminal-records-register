import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulletinOverviewComponent } from './bulletin-overview.component';

describe('BulletinOverviewComponent', () => {
  let component: BulletinOverviewComponent;
  let fixture: ComponentFixture<BulletinOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BulletinOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BulletinOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
