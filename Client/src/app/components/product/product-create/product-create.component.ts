import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { CreateProductRequest } from '../../../models/product.model';
import { ProductService } from '../../../services/product.service.service';

@Component({
  selector: 'app-product-create',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './product-create.component.html',
})
export class ProductCreateComponent {
  productForm: FormGroup;
  loading = false;
  error = '';
  
  constructor(
    private fb: FormBuilder, 
    private productService: ProductService,
    private router: Router
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      price: ['', [Validators.required, Validators.min(0)]],
      description: ['', [Validators.required, Validators.maxLength(500)]]
    });
  }

  onSubmit(): void {
    if (this.productForm.invalid) {
      this.productForm.markAllAsTouched();
      return;
    }
    const productData = this.productForm.value as CreateProductRequest;
    this.productService.createProduct(productData)


    this.productService.createProduct(productData).subscribe({
      next: (response) => {
        if (response.success) {
          this.router.navigate(['/products']);
        } else {
          this.error = response.message;
          this.loading = false;
        }
      },
      error: (err) => {
        this.error = 'Failed to create product';
        console.error(err);
        this.loading = false;
      }
    });
  }}
