import {Component, Input, OnInit} from '@angular/core';
import {Subject} from "../core/models/subject";
import {Router} from "@angular/router";

@Component({
  selector: 'app-subjects',
  templateUrl: './subjects.component.html',
  styleUrls: ['./subjects.component.css']
})
export class SubjectsComponent implements OnInit {

  @Input() subjects: Subject[];

  constructor(
    private router: Router,
  ) {
  }

  ngOnInit() {
  }

  routeSubject(id: string) {
    this.router.navigate(['/subject', id]);
  }
}
