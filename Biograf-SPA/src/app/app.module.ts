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
import { UserListResolver } from './_resolvers/user-list.resolver';
import { EditUserModalComponent } from './admin/edit-user-modal/edit-user-modal.component';
import { ManageDiscountsTabComponent } from './admin/manage-discounts-tab/manage-discounts-tab.component';
import { DiscountService } from './_services/discount.service';
import { EditDiscountModalComponent } from './admin/edit-discount-modal/edit-discount-modal.component';
import { AddDiscountModalComponent } from './admin/add-discount-modal/add-discount-modal.component';
import { AddUserModalComponent } from './admin/add-user-modal/add-user-modal.component';
import { ManageUsersTabComponent } from './admin/manage-users-tab/manage-users-tab.component';
import { ManageShowsTabComponent } from './admin/manage-shows-tab/manage-shows-tab.component';
import { ManageMoviesTabComponent } from './admin/manage-movies-tab/manage-movies-tab.component';
import { EditShowModalComponent } from './admin/edit-show-modal/edit-show-modal.component';
import { AddShowModalComponent } from './admin/add-show-modal/add-show-modal.component';
import { AdminShowsListComponent } from './admin/admin-shows-list/admin-shows-list.component';
import { AdminUsersListComponent } from './admin/admin-users-list/admin-users-list.component';
import { AdminMoviesListComponent } from './admin/admin-movies-list/admin-movies-list.component';
import { EditMovieModalComponent } from './admin/edit-movie-modal/edit-movie-modal.component';
import { AddMovieModalComponent } from './admin/add-movie-modal/add-movie-modal.component';
import { MemberSettingsComponent } from './members/member-settings/member-settings.component';
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
      EditUserModalComponent,
      ManageDiscountsTabComponent,
      EditDiscountModalComponent,
      AddDiscountModalComponent,
      AddUserModalComponent,
      ManageUsersTabComponent,
      ManageShowsTabComponent,
      ManageMoviesTabComponent,
      EditShowModalComponent,
      AddShowModalComponent,
      AdminShowsListComponent,
      AdminUsersListComponent,
      AdminMoviesListComponent,
      EditMovieModalComponent,
      AddMovieModalComponent,
      MemberSettingsComponent
   ],
   entryComponents: [
    EditUserModalComponent,
    EditDiscountModalComponent,
    AddDiscountModalComponent,
    AddUserModalComponent,
    AddShowModalComponent,
    EditShowModalComponent,
    EditMovieModalComponent,
    AddMovieModalComponent
],
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
