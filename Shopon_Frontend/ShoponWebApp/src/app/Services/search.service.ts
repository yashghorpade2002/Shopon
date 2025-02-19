import { Injectable } from '@angular/core';
import { Product } from '../Models/product.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SearchService {
  private apiUrl = 'http://localhost:5128/api/ProductAsyn/SearchProducts'; // Replace <port> with your actual port

  constructor(private http: HttpClient) {}

  searchProducts(key: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}?key=${key}`);
  }
}
