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
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginFormComponent } from './login/login.component';
import { RegisterFormComponent } from './register/register.component';
import { TourComponent } from './tour/tour.component';
import { TourService } from './services/tour.service';
import { ExcursionService } from './services/excursion.service';
import { ClientService } from './services/client.service';
import { TourFormComponent } from './tourform/tourform.component';
import { ExcursionSightService } from './services/excursionsight.service';
import { SightService } from './services/sight.service';
import { AuthService } from './services/auth.service';
import { HttpModule } from '@angular/http';
import { AuthInterceptor } from './httphelpers/interceptor';
import {
  AuthGuardService as AuthGuard, AuthGuardService
} from './httphelpers/auth.guard';
import {
  UnauthGuardService as UnauthGuard, UnauthGuardService
} from './httphelpers/unauth.guard';
import { DialogComponent } from './dialog/dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginFormComponent,
    RegisterFormComponent,
    TourComponent,
    TourFormComponent,
    DialogComponent
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
    HttpModule,
    MatProgressSpinnerModule
  ],
  providers: [TourService, ExcursionService, ClientService, ExcursionSightService, SightService, AuthService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }, AuthGuardService, UnauthGuardService], 
  bootstrap: [AppComponent],
  entryComponents: [TourFormComponent, DialogComponent]
})
export class AppModule { }
