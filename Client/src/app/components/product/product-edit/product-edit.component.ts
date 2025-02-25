import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Product, UpdateProductRequest } from '../../../models/product.model';
import { ProductService } from '../../../services/product.service.service';

@Component({
  selector: 'app-product-edit',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './product-edit.component.html',
})
export class ProductEditComponent implements OnInit {
  productForm: FormGroup;
  loading = false;
  loadingProduct = false;
  error = '';
  productId = '';
  
  constructor(
    private fb: FormBuilder, 
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService
  ) {
    this.productForm = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(100)]],
      price: ['', [Validators.required, Validators.min(0)]],
      description: ['', [Validators.required, Validators.maxLength(500)]]
    });
  }

  ngOnInit(): void {
    this.loadingProduct = true;
    const id = this.route.snapshot.paramMap.get('id');
    
    if (id) {
      this.productId = id;
      this.productService.getProductById(id).subscribe({
        next: (response) => {
          if (response.success) {
            this.productForm.patchValue({
              name: response.data.name,
              price: response.data.price,
              description: response.data.description
            });
          } else {
            this.error = response.message;
          }
          this.loadingProduct = false;
        },
        error: (err) => {
          this.error = 'Failed to load product details';
          console.error(err);
          this.loadingProduct = false;
        }
      });
    } else {
      this.error = 'Product ID is missing';
      this.loadingProduct = false;
    }
  }

  onSubmit(): void {
    if (this.productForm.invalid) {
      this.productForm.markAllAsTouched();
      return;
    }

    const productData: UpdateProductRequest = this.productForm.value;
    this.loading = true;

    
    const productWithId: Product = {
      id: this.productId,
      ...productData as Required<UpdateProductRequest>
    };
    
    this.productService.updateProduct(this.productId,productWithId).subscribe({
      next: (response) => {
        if (response.success) {
          this.router.navigate(['/products', this.productId]);
        } else {
          this.error = response.message;
          this.loading = false;
        }
      },
      error: (err) => {
        this.error = 'Failed to update product';
        console.error(err);
        this.loading = false;
      }
    });
  }
}
