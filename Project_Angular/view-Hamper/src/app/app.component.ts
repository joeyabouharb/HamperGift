import { Component, OnInit } from '@angular/core';
import { RestService } from './rest.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'view-Hamper';
}
