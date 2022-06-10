import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FixWithdrawDialogComponent } from './fix-withdraw-dialog.component';

describe('FixPaidDialogComponent', () => {
  let component: FixWithdrawDialogComponent;
  let fixture: ComponentFixture<FixWithdrawDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FixWithdrawDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FixWithdrawDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
