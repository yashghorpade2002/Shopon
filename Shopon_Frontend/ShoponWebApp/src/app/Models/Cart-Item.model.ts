import { Product } from "./product.model";

export interface CartItem extends Product {
    quantity: number; // Add a quantity property
}