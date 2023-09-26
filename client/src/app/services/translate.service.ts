import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environments } from 'src/environments/environement';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TranslateService {

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };


  constructor(private http: HttpClient) { }

  public getTranslatedData(pathLanguage: string): Observable<any>{
    return this.http.post<any>(`${environments.apiUrl}/Translation/GetData`, pathLanguage, this.httpOptions)

  }
}

