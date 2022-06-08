import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FixPaidDialogComponent } from './fix-paid-dialog.component';

describe('FixPaidDialogComponent', () => {
  let component: FixPaidDialogComponent;
  let fixture: ComponentFixture<FixPaidDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FixPaidDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FixPaidDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
