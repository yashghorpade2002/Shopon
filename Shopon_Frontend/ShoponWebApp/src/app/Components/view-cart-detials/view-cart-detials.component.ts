import { Component, OnInit } from '@angular/core';
import { CartService } from '../../Services/cart.service';
import { Product } from '../../Models/product.model';
import { CommonModule } from '@angular/common';
import { HomeComponent } from '../home/home.component';
import { RouterLink } from '@angular/router';
import { CartItem } from '../../Models/Cart-Item.model';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-view-cart-detials',
  standalone: true,
  imports: [CommonModule, HomeComponent, RouterLink, FormsModule],
  templateUrl: './view-cart-detials.component.html',
  styleUrl: './view-cart-detials.component.css'
})
export class ViewCartDetialsComponent implements OnInit {
  cartItems: CartItem[] = [];

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.cartItems = this.cartService.getCartItems();
  }

  removeFromCart(productId: number): void {
    this.cartService.removeFromCart(productId);
    this.cartItems = this.cartService.getCartItems(); // Update local state
  }

  clearCart(): void {
    this.cartService.clearCart();
    this.cartItems = []; // Clear local state
  }

  updateQuantity(item: CartItem, newQuantity: number): void {
    if (newQuantity >= 1 && newQuantity <= 5) {
      item.quantity = newQuantity;
      this.cartService.saveCart(); // Save updated cart to session storage
    } else if (newQuantity > 5) {
      alert('Maximum product can be 5 only!'); // Show alert for exceeding max quantity
    }
  }

  calculateItemTotal(item: CartItem): number {
    return item.price * item.quantity; // Calculate total for individual item
  }

  calculateCartTotal(): number {
    return this.cartItems.reduce((total, item) => total + this.calculateItemTotal(item), 0); // Calculate total for all items
  }
}