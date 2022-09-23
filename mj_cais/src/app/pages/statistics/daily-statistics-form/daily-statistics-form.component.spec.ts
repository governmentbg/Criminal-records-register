import { ComponentFixture, TestBed } from "@angular/core/testing";

import { DailyStatisticsFormComponent } from "./daily-statistics-form.component";

describe("DailyStatisticsComponent", () => {
  let component: DailyStatisticsFormComponent;
  let fixture: ComponentFixture<DailyStatisticsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DailyStatisticsFormComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DailyStatisticsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it("should create", () => {
    expect(component).toBeTruthy();
  });
});
