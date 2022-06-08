import { ServicesModule } from './services/services.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WrongWalletsComponent } from './wrong-wallets/wrong-wallets.component';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { PaymentDetailComponent } from './payment-detail/payment-detail.component';
import { ComponentsModule } from './components/components.module';
import { MatGridListModule } from '@angular/material/grid-list';
import { AppRoutingModule } from '../app-routing.module';

@NgModule({
  declarations: [
    WrongWalletsComponent,
    PaymentDetailComponent
  ],
  imports: [
    CommonModule,
    MatTableModule,
    MatButtonModule,
    MatGridListModule,
    ServicesModule,
    ComponentsModule,
    AppRoutingModule
  ]
})
export class PagesModule { }
