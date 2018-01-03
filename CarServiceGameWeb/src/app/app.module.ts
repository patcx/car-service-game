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
import { TokenService } from './services/token.service';
import { SortPipe } from './pages/ranking-page/sort.pipe';
import { HistoryOrdersComponent } from './pages/office-page/history-orders/history-orders.component';
import { OrdersViewModel } from './view-model/orders-view-model';
import { GarageViewModel } from './view-model/garage-view-model';
import { WorkersViewModel } from './view-model/workers-view-model';
import { GarageService } from './services/garage.service';
import { OrderService } from './services/order.service';


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
    HistoryOrdersComponent,
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
    TokenService,

    // ViewModels
    GarageViewModel,
    WorkersViewModel,
    OrdersViewModel, 

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
