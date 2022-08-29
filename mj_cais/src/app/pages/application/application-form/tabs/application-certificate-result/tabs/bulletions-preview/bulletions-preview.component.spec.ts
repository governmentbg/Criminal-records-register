import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulletionsPreviewComponent } from './bulletions-preview.component';

describe('BulletionsPreviewComponent', () => {
  let component: BulletionsPreviewComponent;
  let fixture: ComponentFixture<BulletionsPreviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BulletionsPreviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BulletionsPreviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
