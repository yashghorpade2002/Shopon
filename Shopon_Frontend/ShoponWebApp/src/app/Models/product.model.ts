// product.model.ts

export interface Company {
    companyId: number;
    companyName: string;
  }
  
  export interface Category {
    categoryId: number;
    category_Name: string;
  }
  
  export interface Product {
    id: number; // Assuming you have an ID field in your API response
    name: string;
    price: number;
    availableStatus: boolean;
    imageUrl: string;
    company?: Company;     // Nested company object
    ratings?: number | null; // Optional field for ratings
    categories?: Category; // Nested category object
  }
  