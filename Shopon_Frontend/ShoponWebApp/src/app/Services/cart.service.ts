import { Injectable } from '@angular/core';
import { Product } from '../Models/product.model';
import { CartItem } from '../Models/Cart-Item.model';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private cartItems: CartItem[] = [];
  private cartItemCountSubject = new BehaviorSubject<number>(0); // Initialize BehaviorSubject

  constructor() {
    const storedCart = this.getStoredCart();
    if (storedCart) {
      this.cartItems = storedCart;
      this.cartItemCountSubject.next(this.cartItems.length); // Emit initial count
    }
  }

  addToCart(product: Product): void {
    const existingItem = this.cartItems.find(item => item.id === product.id);
    
    if (existingItem) {
      if (existingItem.quantity < 5) { // Check max quantity
        existingItem.quantity++;
      } else {
        alert('Maximum product can be 5 only!'); // Snackbar can be implemented later
      }
    } else {
      this.cartItems.push({ ...product, quantity: 1 }); // Add new item with quantity 1
    }

    this.saveCart();
    this.cartItemCountSubject.next(this.cartItems.length); // Emit updated count
  }

  removeFromCart(productId: number): void {
    this.cartItems = this.cartItems.filter(item => item.id !== productId);
    this.saveCart();
    this.cartItemCountSubject.next(this.cartItems.length); // Emit updated count
  }

  getCartItems(): CartItem[] {
    return this.cartItems;
  }

  clearCart(): void {
    this.cartItems = [];
    this.saveCart();
    this.cartItemCountSubject.next(0); // Emit updated count
  }

  getCartItemCount() {
    return this.cartItemCountSubject.asObservable(); // Return observable for subscribers
  }

  saveCart(): void {
    if (this.isBrowser()) {
      sessionStorage.setItem('cart', JSON.stringify(this.cartItems));
    }
  }

  private getStoredCart(): CartItem[] | null {
    if (this.isBrowser()) {
      const storedCart = sessionStorage.getItem('cart');
      return storedCart ? JSON.parse(storedCart) : null;
    }
    return null; // Return null if not in a browser environment
  }

  private isBrowser(): boolean {
    return typeof window !== 'undefined'; // Check if running in a browser
  }
}

