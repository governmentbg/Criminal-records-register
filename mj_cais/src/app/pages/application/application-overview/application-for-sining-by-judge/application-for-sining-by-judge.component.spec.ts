import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ApplicationForSiningByJudgeComponent } from './application-for-sining-by-judge.component';

describe('ApplicationForSiningByJudgeComponent', () => {
  let component: ApplicationForSiningByJudgeComponent;
  let fixture: ComponentFixture<ApplicationForSiningByJudgeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ApplicationForSiningByJudgeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ApplicationForSiningByJudgeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
