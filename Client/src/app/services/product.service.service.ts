import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { CreateProductRequest, Product } from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private http: HttpClient) { }


 // Get All Products

 getAllProducts() : Observable<ApiResponse<Product[]>>{
 
  return this.http.get<ApiResponse<Product[]>>('products');
 }

// Get a product by id

 getProductById(id: string) : Observable<ApiResponse<Product>>{
  return this.http.get<ApiResponse<Product>>(`products/${id}`);
 }


 // Create a new product

 createProduct(product: CreateProductRequest) : Observable<ApiResponse<Product>>{

  return this.http.post<ApiResponse<Product>>('products', product);
 }


 // Update a product
 updateProduct(id: string, product: Product) : Observable<ApiResponse<Product>>{
  return this.http.put<ApiResponse<Product>>(`products/${id}`, product);
 }


 // Delete a product
deleteProduct(id: string) : Observable<ApiResponse<Product>>{
  return this.http.delete<ApiResponse<Product>>(`products/${id}`);
}


  }

