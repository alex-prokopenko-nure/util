import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TourService } from '../services/tour.service';
import { Tour } from '../viewmodels/tour';
import { Excursion } from '../viewmodels/excursion';
import { Client } from '../viewmodels/client';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ExcursionService } from '../services/excursion.service';
import { ClientService } from '../services/client.service';
import { SightService } from '../services/sight.service';
import { ExcursionSightService } from '../services/excursionsight.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { TourFormComponent } from '../tourform/tourform.component';
import { Sight } from '../viewmodels/sight';
import { AuthService } from '../services/auth.service';
import { DialogComponent } from '../dialog/dialog.component';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './tour.component.html',
  styleUrls: ['./tour.component.css'],
})

export class TourComponent implements OnInit {
  public tours: Tour[];
  today: string;
  excursions: Excursion[] = [];
  clients: Client[] = [];
  tourFormRef: MatDialogRef<TourFormComponent>;
  dialogRef: MatDialogRef<DialogComponent>;
  client: Client = new Client();
  excursion: Excursion = new Excursion();
  sight: Sight = new Sight();
  userName: string;

  constructor(private dialog: MatDialog,
    private clientService: ClientService,
    private excursionService: ExcursionService,
    private tourService: TourService,
    private sightService: SightService,
    private authService: AuthService,
    http: HttpClient) {
    tourService = new TourService(http);
    excursionService = new ExcursionService(http);
    clientService = new ClientService(http);
    this.tourService.getTours().subscribe(result => this.tours = result);
    this.excursionService.getExcursions().subscribe(result => this.excursions = result);
    this.clientService.getClients().subscribe(result => this.clients = result);
    this.userName = localStorage.getItem('user_name');
  }

  ngOnInit() {
  }

  openDialog(t?) {
    this.tourService.mode = "none";
    this.tourFormRef = this.dialog.open(TourFormComponent, {
      data: {
        tour: t ? t : new Tour()
      }
    });
    this.tourFormRef.afterClosed().subscribe(tour => {
      this.tourService.getTours().subscribe(result => this.tours = result);
      this.excursionService.getExcursions().subscribe(result => this.excursions = result);
      this.clientService.getClients().subscribe(result => this.clients = result);
    })
  }

  deleteTour(id: string) {
    this.dialogRef = this.dialog.open(DialogComponent);
    this.dialogRef.afterClosed().subscribe(
      result => {
        if (result) {
          this.tourService.deleteTour(id).subscribe(result => {
            let found = false;
            for (let i = 0; i < this.tours.length; ++i) {
              if (found)
                this.tours[i - 1] = this.tours[i];
              if (this.tours[i].id == result) {
                found = true;
              }
            }
            this.tours.pop();
          });
        }
      }
    )
  }

  getClient(t: Tour) {
    for (let i = 0; i < this.clients.length; ++i) {
      if (this.clients[i].id == t.clientId) {
        return this.clients[i].name;
      }
    }
  }

  getExcursion(t: Tour) {
    for (let i = 0; i < this.excursions.length; ++i) {
      if (this.excursions[i].id == t.excursionId) {
        return this.excursions[i].name;
      }
    }
  }
}
