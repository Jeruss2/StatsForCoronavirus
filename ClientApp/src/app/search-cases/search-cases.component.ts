import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-search-cases',
  templateUrl: './search-cases.component.html',
  styleUrls: ['./search-cases.component.css']
})
export class SearchCasesComponent implements OnInit {
  constructor(private http: HttpClient) { }

  public cases:Cases[];

  ngOnInit() {
    this.SearchCases();
  }

  public SearchCases(): boolean {
    console.log("Hey is this working?");


    function formatDate(unformattedDate) {
      var newDateParts = unformattedDate.split('T')[0].split('-');

      var newDate = newDateParts[1] + '-' + newDateParts[2] + '-' + newDateParts[0];

      var newTime = unformattedDate.split('T')[1].split(':')[0] + ':' + unformattedDate.split('T')[1].split(':')[1];

      return newDate + " " + newTime;
    }

  
    this.http.get<Cases[]>("api/Cases").subscribe(result => {
      result.forEach(x => x.dateAdded = formatDate(x.dateAdded));
      this.cases = result;
      console.log(result);
      console.log();
    });

    //this.http.post("https://localhost:4200/api/Listings", this.listing).subscribe(result => {console.log(result)});

    return true;
  }


  private formatDate(date: string) {


  }

}

export class Cases {
  totalCases: string;
  totalDeaths: string;
  totalRecoveries: string;
  dateAdded: string;
}
