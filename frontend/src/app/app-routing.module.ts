import { PaymentDetailComponent } from './pages/payment-detail/payment-detail.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WrongWalletsComponent } from './pages/wrong-wallets/wrong-wallets.component';

const routes: Routes = [
  { path: 'wrong-wallets', component: WrongWalletsComponent },
  { path: '', component: WrongWalletsComponent },
  { path: 'payment-detail/:walletId', component: PaymentDetailComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
