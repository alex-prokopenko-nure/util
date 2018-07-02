import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TourService } from '../tour.service';
import { Tour } from '../tour';
import { Excursion } from '../excursion';
import { Client } from '../client';
import { FormControl } from '@angular/forms';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
import { ExcursionService } from '../excursion.service';
import { ClientService } from '../client.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
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
  dialogRef: MatDialogRef<DialogComponent>;

  constructor(private dialog: MatDialog, private clientService: ClientService, private excursionService: ExcursionService, private tourService: TourService, http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    tourService = new TourService(http, baseUrl);
    excursionService = new ExcursionService(http, baseUrl);
    clientService = new ClientService(http, baseUrl);
    this.tourService.getTours().subscribe(result => {
      this.tours = result;
    });
    this.excursionService.getExcursions().subscribe(result => this.excursions = result);
    this.clientService.getClients().subscribe(result => this.clients = result);
  }

  ngOnInit() {
  }

  openDialog(t?) {
    this.dialogRef = this.dialog.open(DialogComponent, {
      data: {
        tour: t ? t : new Tour()
      }
    });
    this.dialogRef.afterClosed().subscribe(tour => {
      let index = this.tourExists(tour.id);
      if (index == -1)
        this.tours.push(tour);
    })
  }

  deleteTour(id: number) {
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

  tourExists(id: number) {
    for (let i = 0; i < this.tours.length; ++i) {
      if (this.tours[i].id == id) {
        return i;
      }
    }
    return -1;
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
