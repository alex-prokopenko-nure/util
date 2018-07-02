import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ClientService } from '../client.service';
import { ExcursionService } from '../excursion.service';
import { TourService } from '../tour.service';
import { HttpClient } from '@angular/common/http';
import { Excursion } from '../excursion';
import { Tour } from '../tour';
import { Client } from '../client';
import { startWith, map } from 'rxjs/operators';


@Component({
  templateUrl: './dialog.component.html'
})

export class DialogComponent {
  form: FormGroup;
  tours: Tour[];
  excursions: Excursion[] = [];
  clients: Client[] = [];
  excursionCtrl: FormControl;
  clientCtrl: FormControl;
  filteredExcursions: Excursion[];
  filteredClients: Client[];
  tour: Tour = new Tour();
  today: string;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<DialogComponent>,
    private clientService: ClientService,
    private excursionService: ExcursionService,
    private tourService: TourService,
    http: HttpClient,
    @Inject('BASE_URL') baseUrl: string,
    @Inject(MAT_DIALOG_DATA) private data
  )
  {
    tourService = new TourService(http, baseUrl);
    excursionService = new ExcursionService(http, baseUrl);
    clientService = new ClientService(http, baseUrl);
    this.tourService.getTours().subscribe(result => this.tours = result);
    this.excursionService.getExcursions().subscribe(result => this.excursions = result);
    this.clientService.getClients().subscribe(result => this.clients = result);
    this.excursionCtrl = new FormControl();
    this.excursionCtrl.valueChanges
      .pipe(
        startWith(''),
        map(excursion => excursion ? this.filterExcursions(excursion) : this.excursions.slice())
      ).subscribe(result => {
        this.filteredExcursions = result;
        this.tour.excursionId = this.excursionExists(this.excursionCtrl.value);
      });
    this.clientCtrl = new FormControl();
    this.clientCtrl.valueChanges
      .pipe(
        startWith(''),
        map(client => client ? this.filterClients(client) : this.clients.slice())
      ).subscribe(result => {
        this.filteredClients = result;
        this.tour.clientId = this.clientExists(this.clientCtrl.value);
      });
  }

  ngOnInit() {
    this.tour = this.data.tour;
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1;
    var yyyy = today.getFullYear();
    var day, month;
    dd < 10 ? day = '0' + dd : day = dd;
    mm < 10 ? month = '0' + mm : month = mm;

    this.today = yyyy + '-' + month + '-' + day;
  }

  submit(form) {
    this.dialogRef.close(`${form.value.filename}`);
  }

  filterExcursions(name: string) {
    return this.excursions.filter(option =>
      option.name.toLowerCase().indexOf(name.toLowerCase()) === 0);
  }
  filterClients(name: string) {
    return this.clients.filter(option =>
      option.name.toLowerCase().indexOf(name.toLowerCase()) === 0);
  }

  clientExists(name: string) {
    for (let i = 0; i < this.clients.length; ++i) {
      if (this.clients[i].name == name)
        return this.clients[i].id;
    }
    return -1;
  }

  excursionExists(name: string) {
    for (let i = 0; i < this.excursions.length; ++i) {
      if (this.excursions[i].name == name)
        return this.excursions[i].id;
    }
    return -1;
  }

  chooseExcursion(excursion: Excursion) {
    this.excursionCtrl.setValue(excursion.name);
  }

  chooseClient(client: Client) {
    this.clientCtrl.setValue(client.name);
  }

  createTour() {
    if (this.tour.id == undefined) {
      if (this.tour.clientId == -1) {
        var client = new Client();
        client.name = this.clientCtrl.value;

        this.clientService.insertClient(client).subscribe(result => {
          this.clients.push(result);
          this.tour.clientId = result.id;
        })
      }
      if (this.tour.excursionId == -1) {
        var excursion = new Excursion();
        excursion.name = this.excursionCtrl.value;

        this.excursionService.insertExcursion(excursion).subscribe(result => {
          this.excursions.push(result);
          this.tour.excursionId = result.id;
        })
      }
      this.tourService.insertTour(this.tour).subscribe(result => this.dialogRef.close(result));
    } else {
      alert(this.tour.id);
      this.tourService.updateTour(this.tour).subscribe(result => this.dialogRef.close(result));
    }
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
