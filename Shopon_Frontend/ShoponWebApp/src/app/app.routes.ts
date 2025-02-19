import {RouterModule, Routes } from '@angular/router';

// Import the components you created
import { HomeComponent } from './Components/home/home.component';
import { DetialsComponent } from './Components/detials/detials.component';
import { PlaceOrderComponent } from './Components/place-order/place-order.component';
import { ViewCartDetialsComponent } from './Components/view-cart-detials/view-cart-detials.component';
import { NgModule } from '@angular/core';

export const routes: Routes = [
    { path: '', component: HomeComponent }, // Default route (Home page)
    { path: 'details/:id', component: DetialsComponent }, // Route for the Details page
    { path: 'place-order', component: PlaceOrderComponent }, // Route for the Place Order page
    { path: 'view-cart', component: ViewCartDetialsComponent }, // Route for the View Cart page
    { path: '**', component: HomeComponent} //Take to home componenet if you mis route any source
    // You can add more routes here if needed
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })

  export class AppRoutingModule { }
