import { Component, OnInit } from '@angular/core';
import { ApiService } from 'app/shared/services/api.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  
  constructor(private apiService: ApiService) {
  }

  ngOnInit() {
    // this.apiService.getCurrentAccount().subscribe();
  }

  getMe() {
    this.apiService.getCurrentAccount().subscribe();
  }
}
