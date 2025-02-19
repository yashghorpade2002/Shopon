import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { HomeComponent } from './Components/home/home.component';
import { DetialsComponent } from './Components/detials/detials.component';
import { PlaceOrderComponent } from './Components/place-order/place-order.component';
import { CartService } from './Services/cart.service';
import { CommonModule } from '@angular/common';
import { Product } from './Models/product.model';
import { SearchService } from './Services/search.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, HomeComponent, DetialsComponent, PlaceOrderComponent, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ShoponWebApp';

  searchKey: string = '';
  cartItemCount: number = 0;
  isListening: boolean = false;
  // searchResults: Product[] = []; // Array to hold search results

  constructor(private cartService: CartService,  private router: Router) {

  }

  ngOnInit(): void {
    // Subscribe to cart item count changes
    this.cartService.getCartItemCount().subscribe(count => {
      this.cartItemCount = count; // Update cart item count dynamically
    });
    
    // Initial count setup (optional)
    this.updateCartItemCount();

    // this.searchResults = [];
  }

  updateCartItemCount(): void {
    this.cartItemCount = this.cartService.getCartItems().length; // Get current count of items in the cart
  }

  // onSearch(searchKey: string): void {
  //   if (searchKey.length > 0) {
  //     this.searchService.searchProducts(searchKey).subscribe(
  //       (data) => {
  //         console.log(data);
  //         this.searchResults = data; // Update search results
  //       },
  //       (error) => {
  //         console.error('Error fetching search results:', error);
  //         this.searchResults = []; // Clear results on error
  //       }
  //     );
  //   } else {
  //     this.searchResults = []; // Clear results if input is empty
  //   }
  // }

  onSearch(): void {
    if (this.searchKey.length > 0) {
      // Navigate to home route with searchKey as a query parameter
      this.router.navigate(['/'], { queryParams: { search: this.searchKey } });
    }
  }

  async startListening(): Promise<void> {
    this.isListening = true; // Disable input
    const stream = await navigator.mediaDevices.getUserMedia({ audio: true });
    const mediaRecorder = new MediaRecorder(stream);
    const audioChunks: Blob[] = [];

    mediaRecorder.ondataavailable = event => {
        audioChunks.push(event.data);
    };

    mediaRecorder.onstop = async () => {
        const audioBlob = new Blob(audioChunks, { type: 'audio/wav' });
        const formData = new FormData();
        formData.append('audio', audioBlob);

        const response = await fetch('http://localhost:8000/recognize', { // Change to your Python server's URL
            method: 'POST',
            body: formData,
        });

        const result = await response.json();
        if (response.ok) {
            this.searchKey = result.text; // Set recognized text to searchKey
            this.onSearch(); // Trigger search with recognized text
        } else {
            console.error(result.error);
        }
        
        this.isListening = false; // Re-enable input
    };

    mediaRecorder.start();
    
    setTimeout(() => {
        mediaRecorder.stop(); // Stop recording after a certain time or based on user action
    }, 5000); // Adjust time as needed
}

  
}
