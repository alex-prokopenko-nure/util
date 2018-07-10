import { Component, OnInit, Inject, HostListener } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ClientService } from '../services/client.service';
import { ExcursionService } from '../services/excursion.service';
import { TourService } from '../services/tour.service';
import { HttpClient } from '@angular/common/http';
import { Excursion } from '../viewmodels/excursion';
import { Tour } from '../viewmodels/tour';
import { Client } from '../viewmodels/client';
import { startWith, map, catchError } from 'rxjs/operators';
import { Sight } from '../viewmodels/sight';
import { SightService } from '../services/sight.service';
import { ExcursionSightService } from '../services/excursionsight.service';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.css']
})
export class DialogComponent implements OnInit {

  constructor(
    private dialogRef: MatDialogRef<DialogComponent>
  ) { }

  ngOnInit() {
  }

  delete() {
    this.dialogRef.close(true);
  }
}
