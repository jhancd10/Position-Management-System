import { ComponentFixture, TestBed } from '@angular/core/testing';

import RecruitersListComponent from './recruiters-list.component';

describe('RecruitersListComponent', () => {
  let component: RecruitersListComponent;
  let fixture: ComponentFixture<RecruitersListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RecruitersListComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RecruitersListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
