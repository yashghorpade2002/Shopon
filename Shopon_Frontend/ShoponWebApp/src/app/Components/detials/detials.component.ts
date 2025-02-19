import { Component, OnInit } from '@angular/core';
import { ProductService} from '../../Services/product.service'
import { ActivatedRoute, RouterLink, Router } from '@angular/router';
import { Product } from '../../Models/product.model';
import { error } from 'console';
import { CommonModule } from '@angular/common';
import { HomeComponent } from '../home/home.component';
import { CartService } from '../../Services/cart.service';

@Component({
  selector: 'app-detials',
  standalone: true,
  imports: [CommonModule, HomeComponent, RouterLink],
  templateUrl: './detials.component.html',
  styleUrl: './detials.component.css'
})
export class DetialsComponent implements OnInit {
  
  product: Product = {
    id: 0,
    name: 'Loading...',
    price: 0,
    availableStatus: false,
    imageUrl: 'assets/images/placeholder.png', 
    company: { companyId: 0, companyName: 'N/A' },
    ratings: null,
    categories: { categoryId: 0, category_Name: 'N/A' }
  };
  errorMessage: string | null = null;

  constructor ( 
    private productService: ProductService,
    private route: ActivatedRoute,
    private cartService: CartService,
    private router: Router,
  ) {}
  

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.productService.getProductById(id).subscribe(
      (data) => {
        // console.log('Product data received:', data);
        this.product = data;
      },
      (error) => {
        this.errorMessage = 'Product not found!';
        console.log('No product found');
        console.error(error);
      }
    );
  }

  addToCart(): void {
    if (this.product) {
      this.cartService.addToCart(this.product); // Add product to cart
      // alert(`${this.product.name} has been added to your cart!`); // Optional: Show a confirmation message
      this.router.navigate(['/view-cart']);
    }
  }

  
}
