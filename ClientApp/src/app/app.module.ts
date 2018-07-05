import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatButtonModule, MatMenuModule, MatToolbarModule, MatIconModule, MatCardModule, MatFormFieldModule, MatInputModule, MatTooltipModule } from '@angular/material';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { TourComponent } from './tour/tour.component';
import { TourService } from './tour.service';
import { ExcursionService } from './excursion.service';
import { ClientService } from './client.service';
import { TourFormComponent } from './tourform/tourform.component';
import { ExcursionSightService } from './excursionsight.service';
import { SightService } from './sight.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    TourComponent,
    TourFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'tour', component: TourComponent },
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
    MatDialogModule
  ],
  providers: [TourService, ExcursionService, ClientService, ExcursionSightService, SightService], 
  bootstrap: [AppComponent],
  entryComponents: [TourFormComponent]
})
export class AppModule { }
