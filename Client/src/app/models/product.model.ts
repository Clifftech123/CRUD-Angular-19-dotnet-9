export interface Product {
    id :string;
    name :string
    price :number
    description :string
}

export interface  CreateProductRequest {
    name :string
    price :number
    description :string
}


export interface UpdateProductRequest {
  
    name? :string
    price? :number
    description? :string
}

