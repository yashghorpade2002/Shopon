import { Component, OnInit } from '@angular/core';
import { Product } from '../../Models/product.model';
import { SearchService } from '../../Services/search.service';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-place-order',
  standalone: true,
  imports: [RouterLink, FormsModule, CommonModule],
  templateUrl: './place-order.component.html',
  styleUrl: './place-order.component.css'
})
export class PlaceOrderComponent implements OnInit {
  products: Product[] = []; 
  searchKey: string = '';

  constructor(private searchService : SearchService) {

  }
  
  ngOnInit(): void {
    this.products = [];
  }

  onSearch(searchKey: string): void {
    if (searchKey.length > 0) {
      this.searchService.searchProducts(searchKey).subscribe(
        (data) => {
          console.log(data);
          this.products = data; // Update search results
        },
        (error) => {
          console.error('Error fetching search results:', error);
          this.products = []; // Clear results on error
        }
      );
    } else {
      this.products = []; // Clear results if input is empty
    }
  }

}
