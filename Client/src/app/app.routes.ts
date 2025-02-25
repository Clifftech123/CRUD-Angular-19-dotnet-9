import { Routes } from '@angular/router';
import { ProductComponent } from './components/product/product.component';
import { ProductCreateComponent } from './components/product/product-create/product-create.component';
import { ProductDetailComponent } from './components/product/product-detail/product-detail.component';
import { ProductEditComponent } from './components/product/product-edit/product-edit.component';


export const routes: Routes = [
    { path: 'products', component: ProductComponent },
    { path: 'products/create', component: ProductCreateComponent },
    { path: 'products/:id', component: ProductDetailComponent },
    { path: 'products/:id/edit', component: ProductEditComponent },
    { path: '', redirectTo: '/products', pathMatch: 'full' }
  ];