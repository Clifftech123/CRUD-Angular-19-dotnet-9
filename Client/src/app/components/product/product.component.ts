import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service.service';
import { RouterLink, RouterOutlet } from '@angular/router';
import { CommonModule, NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-product',
  imports: [
    CommonModule,
    RouterLink,
    RouterOutlet,
    NgIf,
    NgFor
  ],
  standalone: true,
  templateUrl: './product.component.html'
})

export class ProductComponent implements OnInit {

 products :Product[] = [];
 loading = false
 error='';

 constructor( private productService: ProductService ) { }
  ngOnInit(): void {
    this.loadProducts();
  }


  loadProducts(): void {
    this.loading = true;
    this.productService.getAllProducts().subscribe({
      next: (response) => {
        if (response.success) {
          this.products = response.data;
        } else {
          this.error = response.message;
        }
        this.loading = false;
      },
      error: (err) => {
        this.error = 'Failed to load products';
        console.error(err);
        this.loading = false;
      }
    });
  }


  confirmDelete(id: string): void {
    if (confirm('Are you sure you want to delete this product?')) {
      this.productService.deleteProduct(id).subscribe({
        next: (response) => {
          if (response.success) {
            this.products = this.products.filter(p => p.id !== id);
          } else {
            this.error = response.message;
          }
        },
        error: (err) => {
          this.error = 'Failed to delete product';
          console.error(err);
        }
      });
    }
  }


 
 
}
