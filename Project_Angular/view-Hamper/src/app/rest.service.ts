import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { map, catchError, tap } from 'rxjs/operators';

const endpoint = 'https://localhost:44310/api/hampers/';
const httpOptions = {
headers: new HttpHeaders({
'Content-Type':  'application/json'
})
};

@Injectable({
  providedIn: 'root'
  
})

export class RestService {

  constructor(private http: HttpClient) {
   
}

   private extractData(res: Response) {
    let body = res;
    return body || { };
  }


  getHampers(): Observable<any> {
    return this.http.get(endpoint).pipe(
      map(this.extractData))
  }

  getHampersByCat(id: number): Observable<any> {
    return this.http.get(endpoint + id).pipe(
      map(this.extractData))
  }

  getHamperByString(q: string): Observable<any> {
    return this.http.get(endpoint + 'search/' + q).pipe(
      map(this.extractData))
  }

  getCats(): Observable<any> {
    return this.http.get(endpoint + 'categories').pipe(
      map(this.extractData))
  }

}
