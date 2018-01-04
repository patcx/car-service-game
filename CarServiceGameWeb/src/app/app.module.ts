import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule, Routes, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Http, HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { DashboardPageComponent } from './pages/dashboard-page/dashboard-page.component';
import { GaragePageComponent } from './pages/garage-page/garage-page.component';
import { OrdersPageComponent } from './pages/orders-page/orders-page.component';
import { RankingPageComponent } from './pages/ranking-page/ranking-page.component';
import { WorkersPageComponent } from './pages/workers-page/workers-page.component';
import { OfficePageComponent } from './pages/office-page/office-page.component';
import { LoginService } from './services/login.service';
import { RankingService } from './services/ranking.service';
import { AccountService } from './services/account.service';
import { SortPipe } from './pages/ranking-page/sort.pipe';
import { GarageService } from './services/garage.service';
import { OrderService } from './services/order.service';
import { LoaderComponent } from './pages/loader/loader.component';


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
    OfficePageComponent,
    SortPipe,
    LoaderComponent,
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes),
    FormsModule,
    CommonModule,
    HttpModule,
  ],
  exports: [
    RouterModule
  ],
  providers: [
    [{provide: 'LoginService', useClass: LoginService}],
    [{provide: 'RankingService', useClass: RankingService}],
    [{provide: 'OrderService', useClass: OrderService}],
    [{provide: 'GarageService', useClass: GarageService}],
    AccountService,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
