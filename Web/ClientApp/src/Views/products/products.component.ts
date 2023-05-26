import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})  
export class ProductsComponent implements OnInit {

  
  public currentCount = 0;
  constructor() {
    //service.getUsers().subscribe(resp => {
    //  console.log(resp);
    //}, error => {
    //  console.error(error);
    //});
  }

  ngOnInit(): void {
  }
  public incrementCounter() {
    this.currentCount++;
  }
}
