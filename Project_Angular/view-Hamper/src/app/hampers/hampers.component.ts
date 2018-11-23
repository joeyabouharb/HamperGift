import { Component, OnInit } from '@angular/core';
import { RestService } from '../rest.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-hampers',
  templateUrl: './hampers.component.html',
  styleUrls: ['./hampers.component.css']


})
export class HampersComponent implements OnInit {

  hampers:any [] ;
  categories:any [];
  hampersQ:any [];
  id = 0;
  q = '';
  constructor(public rest:RestService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit() {
    this.getHampers();
    this.getCats();
   
  }

  LoadHampers(){
    this.getHamperByCatId();
  }

  QueryLoad(){
    this.getHamperByQuery();
  }
  getHampers(){
    this.rest.getHampers().subscribe((data: []) => {
     

      this.hampers = data;
    });
  }

  getCats(){
    this.rest.getCats().subscribe((data: []) => {
       
      this.categories = data 
    })

  }
  getHamperByCatId(){
    if(this.id === 0){
      this.getHampers();
      return;
    }
    this.rest.getHampersByCat(this.id).subscribe((data: []) => {
      this.hampers = data;

    })
  }

  getHamperByQuery(){
    if(this.q === ''){
      this.getHampers();
      return;
    }
  this.rest.getHamperByString(this.q).subscribe((data: []) => {
  this.hampers = data;


  })


  }
  }