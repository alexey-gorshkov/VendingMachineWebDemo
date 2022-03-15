import { Component, OnInit, Input } from '@angular/core';
import { Purse } from 'src/app/models/purse';

@Component({
  selector: 'app-vm-purse',
  templateUrl: './vm-purse.component.html',
  styleUrls: ['./vm-purse.component.css']
})
export class VMPurseComponent implements OnInit {

  @Input() purseVM?: Purse;

  constructor() { }

  ngOnInit() {
  }

}
