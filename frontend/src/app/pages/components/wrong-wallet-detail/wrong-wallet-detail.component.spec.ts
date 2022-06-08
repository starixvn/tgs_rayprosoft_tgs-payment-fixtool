import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WrongWalletDetailComponent } from './wrong-wallet-detail.component';

describe('WrongWalletDetailComponent', () => {
  let component: WrongWalletDetailComponent;
  let fixture: ComponentFixture<WrongWalletDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WrongWalletDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WrongWalletDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
