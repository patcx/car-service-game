import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, RouterLink } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { DashboardPageComponent } from './pages/dashboard-page/dashboard-page.component';
import { GaragePageComponent } from './pages/garage-page/garage-page.component';
import { OrdersPageComponent } from './pages/orders-page/orders-page.component';
import { RankingPageComponent } from './pages/ranking-page/ranking-page.component';
import { WorkersPageComponent } from './pages/workers-page/workers-page.component';
import { OfficePageComponent } from './pages/office-page/office-page.component';
import { LoginService } from './services/login.service';


const routes: Routes = [
  { path: 'Garage', component: GaragePageComponent },
  { path: 'Orders', component: OrdersPageComponent },
  { path: 'Workers', component: WorkersPageComponent },
  { path: 'Office', component: OfficePageComponent },
  { path: 'Ranking', component: RankingPageComponent },
];


@NgModule({
  declarations: [
    AppComponent,
    LoginPageComponent,
    DashboardPageComponent,
    GaragePageComponent,
    OrdersPageComponent,
    RankingPageComponent,
    WorkersPageComponent,
    OfficePageComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ],
  providers: [
    LoginService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
