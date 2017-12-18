import { Component, OnInit, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-test-client',
  templateUrl: './test-client.component.html',
  styleUrls: ['./test-client.component.css']
})

export class TestClientComponent implements OnInit {
  data : String[];

  constructor(private http : HttpClient) { }

  ngOnInit() {
      this.getData().subscribe(result => this.data = result);
  }

  getData() : Observable<String[]> {
    return this.http.get<String[]>('http://localhost:5000/api/values');
  }

}
