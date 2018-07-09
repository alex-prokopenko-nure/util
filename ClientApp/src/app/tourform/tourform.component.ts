import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ClientService } from '../services/client.service';
import { ExcursionService } from '../services/excursion.service';
import { TourService } from '../services/tour.service';
import { HttpClient } from '@angular/common/http';
import { Excursion } from '../viewmodels/excursion';
import { Tour } from '../viewmodels/tour';
import { Client } from '../viewmodels/client';
import { startWith, map } from 'rxjs/operators';
import { Sight } from '../viewmodels/sight';
import { SightService } from '../services/sight.service';
import { ExcursionSightService } from '../services/excursionsight.service';


@Component({
  templateUrl: './tourform.component.html',
  styleUrls: ['./tourform.component.css']
})

export class TourFormComponent {
  form: FormGroup;
  tours: Tour[];
  excursions: Excursion[] = [];
  clients: Client[] = [];
  sights: Sight[] = [];
  tourSights: Sight[] = [];
  sight: Sight = new Sight();
  ids: number[];
  excursionCtrl: FormControl;
  clientCtrl: FormControl;
  sightCtrl: FormControl;
  filteredExcursions: Excursion[];
  filteredClients: Client[];
  filteredSights: Sight[];
  tour: Tour = new Tour();
  today: string;
  hasDate: boolean = true;
  hasExcursion: boolean = true;
  hasClient: boolean = true;
  hasSights: boolean = true;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<TourFormComponent>,
    private clientService: ClientService,
    private excursionService: ExcursionService,
    private tourService: TourService,
    private sightService: SightService,
    private excursionSightService: ExcursionSightService,
    http: HttpClient,
    @Inject(MAT_DIALOG_DATA) private data
  )
  {
    this.sight.name = '';
    this.tourService.getTours().subscribe(result => this.tours = result);
    this.excursionService.getExcursions().subscribe(result => { this.excursions = result; this.filterExcursions(''); });
    this.clientService.getClients().subscribe(result => { this.clients = result; this.filterClients(''); });
    this.sightService.getAllSights().subscribe(result => { this.sights = result; this.filterSights(''); });
    this.excursionCtrl = new FormControl();
    this.excursionCtrl.valueChanges
      .pipe(
        startWith(''),
        map(excursion => excursion ? this.filterExcursions(excursion) : this.excursions.slice())
      ).subscribe(result => {
        this.filteredExcursions = result;
        this.tour.excursionId = this.excursionExists(this.excursionCtrl.value);
        if (this.tour.excursionId) {
          this.sightService.getSights(this.tour.excursionId).subscribe(result => this.tourSights = result);
        } else {
          this.tourSights = [];
        }
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
    this.sightCtrl = new FormControl();
    this.sightCtrl.valueChanges
      .pipe(
        startWith(''),
        map(sight => sight ? this.filterSights(sight) : this.sights.slice())
      ).subscribe(result => {
        this.filteredSights = result;
        this.sight.name = this.sightCtrl.value;
        this.sight.id = this.sightExists(this.sightCtrl.value);
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
    if (this.tour.id != undefined) {
      this.sightService.getSights(this.tour.excursionId).subscribe(result => this.tourSights = result);
    }
  }

  sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
  }

  async addSight() {
    if (this.sight.id == undefined) {
      this.sightService.insertSight(this.sight).subscribe(result => { this.sights.push(result); this.sight = result });
    }
    await this.sleep(300);
    this.excursionSightService.insertSight(this.tour.excursionId, this.sight.id).subscribe(result => {
      let addedSight = new Sight();
      addedSight.id = this.sight.id;
      addedSight.name = this.sight.name;
      this.tourSights.push(addedSight);
      this.sight = new Sight();
      this.sightCtrl.setValue('');
    });
  }

  filterExcursions(name: string) {
    return this.excursions.filter(option =>
      option.name.toLowerCase().indexOf(name.toLowerCase()) === 0);
  }

  filterClients(name: string) {
    return this.clients.filter(option =>
      option.name.toLowerCase().indexOf(name.toLowerCase()) === 0);
  }

  filterSights(name: string) {
    return this.sights.filter(option =>
      option.name.toLowerCase().indexOf(name.toLowerCase()) === 0);
  }

  clientExists(name: string) {
    for (let i = 0; i < this.clients.length; ++i) {
      if (this.clients[i].name == name)
        return this.clients[i].id;
    }
    return undefined;
  }

  excursionExists(name: string) {
    for (let i = 0; i < this.excursions.length; ++i) {
      if (this.excursions[i].name == name)
        return this.excursions[i].id;
    }
    return undefined;
  }

  sightExists(name: string) {
    for (let i = 0; i < this.sights.length; ++i) {
      if (this.sights[i].name == name)
        return this.sights[i].id;
    }
    return undefined;
  }

  chooseExcursion(excursion: Excursion) {
    this.excursionCtrl.setValue(excursion.name);
  }

  chooseClient(client: Client) {
    this.clientCtrl.setValue(client.name);
  }

  chooseSight(sight: Sight) {
    this.sightCtrl.setValue(sight.name);
  }

  async insertExcursionAndClient() {
    if (this.tour.clientId == undefined) {
      var client = new Client();
      client.name = this.clientCtrl.value;

      await this.clientService.insertClient(client).subscribe(result => {
        this.tour.clientId = result.id;
      })
    }
    if (this.tour.excursionId == undefined) {
      var excursion = new Excursion();
      excursion.name = this.excursionCtrl.value;

      await this.excursionService.insertExcursion(excursion).subscribe(result => {
        this.tour.excursionId = result.id;
      });
    }
    return 0;
  }

  async createTour() {
    if (this.tour.date == null) {
      this.hasDate = false;
      return;
    }
    this.hasDate = true;
    if (!this.tour.excursionId && (this.excursionCtrl.value == null || this.excursionCtrl.value == '')) {
      this.hasExcursion = false;
      return;
    }
    this.hasExcursion = true;
    if (!this.tour.clientId && (this.clientCtrl.value == null || this.clientCtrl.value == '')) {
      this.hasClient = false;
      return;
    }
    this.hasClient = true;
    await this.insertExcursionAndClient();
    if (this.tourSights.length == 0) {
      this.hasSights = false;
      return;
    }
    this.hasSights = true;
    await this.sleep(300);
    if (this.tour.id == undefined) {
      localStorage.setItem('mode', 'add');
      this.tourService.insertTour(this.tour).subscribe(result => this.dialogRef.close(result));
    } else {
      localStorage.setItem('mode', 'edit');
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

  deleteSight(sight: Sight) {
    this.excursionSightService.deleteSight(this.tour.excursionId, sight.id).subscribe(result => this.sightService.getSights(this.tour.excursionId).subscribe(result => this.tourSights = result));
  }
}
