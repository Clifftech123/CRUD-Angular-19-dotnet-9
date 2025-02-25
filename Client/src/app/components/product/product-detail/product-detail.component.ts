import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';

import { Product } from '../../../models/product.model';
import { ProductService } from '../../../services/product.service.service';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './product-detail.component.html',
})
export class ProductDetailComponent implements OnInit {
  product: Product | null = null;
  loading = false;
  error = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    const id = this.route.snapshot.paramMap.get('id');
    
    if (id) {
      this.productService.getProductById(id).subscribe({
        next: (response) => {
          if (response.success) {
            this.product = response.data;
          } else {
            this.error = response.message;
          }
          this.loading = false;
        },
        error: (err) => {
          this.error = 'Failed to load product details';
          console.error(err);
          this.loading = false;
        }
      });
    } else {
      this.error = 'Product ID is missing';
      this.loading = false;
    }
  }

  confirmDelete(): void {
    if (!this.product) return;
    
    if (confirm('Are you sure you want to delete this product?')) {
      this.loading = true;
      this.productService.deleteProduct(this.product.id).subscribe({
        next: (response) => {
          if (response.success) {
            this.router.navigate(['/products']);
          } else {
            this.error = response.message;
            this.loading = false;
          }
        },
        error: (err) => {
          this.error = 'Failed to delete product';
          console.error(err);
          this.loading = false;
        }
      });
    }
  }
}
