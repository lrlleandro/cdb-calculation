import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { SimulatorData } from '../interfaces/simulator-data';

@Injectable({
  providedIn: 'root'
})

export class ApiService {
  constructor(private http: HttpClient) { }

  simulatePerformance(request: SimulatorData) {
    return this.http.post(`https://localhost:7176/api/cdb`, request)
      .pipe(
        catchError(error => {
          return throwError(error);
        })
      );;
  }
} 
