import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {HttpClientModule} from '@angular/common/http';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { BsDropdownModule, TabsModule, BsDatepickerModule, PaginationModule, ButtonsModule, CarouselModule, TooltipModule, ModalModule } from 'ngx-bootstrap';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { NavComponent } from './nav/nav.component';
import { ErrorInterceptorProvider } from './_services/error.interceptor';
import { AlertifyService } from './_services/alertify.service';
import { AuthService } from './_services/auth.service';
import { appRoutes } from './routes';
import { AuthGuard } from './_guards/auth.guard';
import { UserService } from './_services/user.service';
import { JwtModule } from '@auth0/angular-jwt';
import {TimeAgoPipe} from 'time-ago-pipe';
import { MovieListComponent } from './movies/movie-list/movie-list.component';
import { MovieListResolver } from './_resolvers/movie-list.resolver';
import { MovieCardComponent } from './movies/movie-card/movie-card.component';
import { MovieService } from './_services/movie.service';
import { MinuteSecondsPipe } from './pipes/MinuteSecondsPipe';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditResolver } from './_resolvers/member-edit.resolver';

import { LOCALE_ID } from '@angular/core';
import localeDa from '@angular/common/locales/da';
import { registerLocaleData } from '@angular/common';
import { MemberOrdersComponent } from './members/member-orders/member-orders.component';
import { ReservationService } from './_services/reservation.service';
import { MovieDetailResolver } from './_resolvers/movie-detail.resolver';
import { MovieDetailComponent } from './movies/movie-detail/movie-detail.component';
import { FormatStarsPipe } from './pipes/FormatStarsPipe';
import { ShowService } from './_services/show.service';
import { ShowListResolver } from './_resolvers/show-list.resolver';
import { ShowGridComponent } from './shows/show-grid/show-grid.component';
import { ShowListComponent } from './shows/show-list/show-list.component';
import { ReservationCheckoutComponent } from './reservations/reservation-checkout/reservation-checkout.component';
import { MemberDiscountStepComponent } from './members/member-discount-step/member-discount-step.component';
import { PersonaleAdminGuard } from './_guards/personale.admin.guards';
import { ReservationValidationComponent } from './reservations/reservation-validation/reservation-validation.component';
import { AdminControlPanelComponent } from './admin/admin-control-panel/admin-control-panel.component';
import { EditUsersTabComponent } from './admin/edit-users-tab/edit-users-tab.component';
import { UserListComponent } from './admin/user-list/user-list.component';
import { UserListResolver } from './_resolvers/user-list.resolver';
import { EditUserModalComponent } from './admin/edit-user-modal/edit-user-modal.component';
import { CreateMovieTabComponent } from './admin/create-movie-tab/create-movie-tab.component';
import { ManageDiscountsTabComponent } from './admin/manage-discounts-tab/manage-discounts-tab.component';
import { DiscountService } from './_services/discount.service';
import { EditDiscountModalComponent } from './admin/edit-discount-modal/edit-discount-modal.component';
import { CreateShowTabComponent } from './admin/create-show-tab/create-show-tab.component';
registerLocaleData(localeDa);

export function tokenGetter() {
    return localStorage.getItem('token');
}

@NgModule({
   declarations: [
      AppComponent,
      HomeComponent,
      RegisterComponent,
      NavComponent,
      TimeAgoPipe,
      MovieListComponent,
      MovieCardComponent,
      MinuteSecondsPipe,
      FormatStarsPipe,
      MemberEditComponent,
      MemberOrdersComponent,
      MovieDetailComponent,
      ShowGridComponent,
      ShowListComponent,
      ReservationCheckoutComponent,
      MemberDiscountStepComponent,
      ReservationValidationComponent,
      AdminControlPanelComponent,
      EditUsersTabComponent,
      UserListComponent,
      EditUserModalComponent,
      CreateMovieTabComponent,
      ManageDiscountsTabComponent,
      EditDiscountModalComponent,
      CreateShowTabComponent
   ],
   entryComponents: [EditUserModalComponent, EditDiscountModalComponent],
   imports: [
      BrowserModule,
      HttpClientModule,
      FormsModule,
      ReactiveFormsModule,
      BsDropdownModule.forRoot(),
      BsDatepickerModule.forRoot(),
      PaginationModule.forRoot(),
      ButtonsModule.forRoot(),
      TabsModule.forRoot(),
      TooltipModule.forRoot(),
      ModalModule.forRoot(),
      CarouselModule.forRoot(),
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
          config: {
              tokenGetter: tokenGetter,
              whitelistedDomains: ['localhost:5000'],
              blacklistedRoutes: ['localhost:5000/api/auth']
          }
      })
   ],
   providers: [
    AuthService,
    ErrorInterceptorProvider,
    AlertifyService,
    AuthGuard,
    PersonaleAdminGuard,
    UserService,
    MovieListResolver,
    MovieService,
    ReservationService,
    MovieDetailResolver,
    MemberEditResolver,
    ShowService,
    DiscountService,
    ShowListResolver,
    UserListResolver,
    { provide: LOCALE_ID, useValue: 'da' }
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }