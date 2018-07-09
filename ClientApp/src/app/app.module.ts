import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule, MatMenuModule, MatToolbarModule, MatIconModule, MatCardModule, MatFormFieldModule, MatInputModule, MatTooltipModule } from '@angular/material';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginFormComponent } from './login/login.component';
import { RegisterFormComponent } from './register/register.component';
import { TourComponent } from './tour/tour.component';
import { TourService } from './tour.service';
import { ExcursionService } from './excursion.service';
import { ClientService } from './client.service';
import { TourFormComponent } from './tourform/tourform.component';
import { ExcursionSightService } from './excursionsight.service';
import { SightService } from './sight.service';
import { AuthService } from './auth.service';
import { HttpModule } from '@angular/http';
import { AuthInterceptor } from './interceptor';
import {
  AuthGuardService as AuthGuard, AuthGuardService
} from './auth.guard';
import {
  UnauthGuardService as UnauthGuard, UnauthGuardService
} from './unauth.guard';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginFormComponent,
    RegisterFormComponent,
    TourComponent,
    TourFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginFormComponent, pathMatch: 'full', canActivate: [UnauthGuard] },
      { path: 'register', component: RegisterFormComponent, canActivate: [UnauthGuard] },
      { path: 'tour', component: TourComponent, canActivate: [AuthGuard] },
    ]),
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatButtonModule,
    MatMenuModule,
    MatToolbarModule,
    MatIconModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatTooltipModule,
    MatTableModule,
    MatDialogModule,
    HttpModule
  ],
  providers: [TourService, ExcursionService, ClientService, ExcursionSightService, SightService, AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }, AuthGuardService, UnauthGuardService], 
  bootstrap: [AppComponent],
  entryComponents: [TourFormComponent]
})
export class AppModule { }
