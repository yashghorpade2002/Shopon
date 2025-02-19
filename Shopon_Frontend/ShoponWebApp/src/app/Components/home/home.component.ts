import { Component, OnInit, Input } from '@angular/core';
import {Product} from '../../Models/product.model';
import {ProductService} from '../../Services/product.service';
import { CommonModule } from '@angular/common';
import { CarouselComponent } from './carousel/carousel.component';
import { ActivatedRoute, RouterLink, RouterOutlet } from '@angular/router';
import { SearchService } from '../../Services/search.service';


@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, CarouselComponent, RouterLink, RouterOutlet],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit{
  searchResults: Product[] = [];
  products: Product[] = [];
  errormessage: string | null = null;

  constructor(
    private productService: ProductService,
    private route: ActivatedRoute,
    private searchService: SearchService
  ) {}

  ngOnInit(): void {
    // Load all products initially
    this.loadProducts();

    // Check for search key in query parameters
    this.route.queryParams.subscribe(params => {
      const searchKey = params['search'];
      if (searchKey) {
        this.onSearch(searchKey); // Trigger search if there's a search key
      }
    });
  }

  loadProducts() {
    this.productService.getAllProducts().subscribe(
      (data) => {
        this.products = data; // Load all products into the products array
      },
      (error) => {
        this.errormessage = 'Error While Fetching Products!';
        console.error(error);
      }
    );
  }

  onSearch(searchKey: string): void {
    if (searchKey.length > 0) {
      this.searchService.searchProducts(searchKey).subscribe(
        (data) => {
          this.searchResults = data; // Update products with search results
          this.products = data; // Display only search results
        },
        (error) => {
          console.error('Error fetching search results:', error);
          this.searchResults = []; // Clear results on error
          this.products = []; // Clear products on error
        }
      );
    } else {
      this.loadProducts(); // If no search key, load all products
    }
  }

}
