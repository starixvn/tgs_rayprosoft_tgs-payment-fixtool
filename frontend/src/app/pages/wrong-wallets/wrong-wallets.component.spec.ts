import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WrongWalletsComponent } from './wrong-wallets.component';

describe('WrongWalletsComponent', () => {
  let component: WrongWalletsComponent;
  let fixture: ComponentFixture<WrongWalletsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WrongWalletsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WrongWalletsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
