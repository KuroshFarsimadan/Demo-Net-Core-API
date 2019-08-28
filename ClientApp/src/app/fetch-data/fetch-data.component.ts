import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-fetch-data',
  templateUrl: './fetch-data.component.html'
})
export class FetchDataComponent {
  public users: Users[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Users[]>(baseUrl + 'api/User/GetUsers').subscribe(result => {
      this.users = result;
    }, error => console.error(error));
  }
}

interface Users {
  dateFormatted: string;
  Id: number;
  HashedId: number;
  Description: string;
}
