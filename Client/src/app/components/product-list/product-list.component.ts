import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/product';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent implements OnInit {
  products: Product[] = [
    {
      id: 1,
      title: 'Sample Product 1',
      description: 'Description for product 1',
      price: 19.99,
      imageUrl: 'https://via.placeholder.com/150',
      ratings: [4, 5, 3],
    },
    {
      id: 2,
      title: 'Sample Product 2',
      description: 'Description for product 2',
      price: 29.99,
      imageUrl: 'https://via.placeholder.com/150',
      ratings: [5, 4],
    },
    // Add more products as needed
  ];

  constructor() {}

  ngOnInit(): void {}

  getAverageRating(product: Product): string {
    if (!product.ratings || product.ratings.length === 0) {
      return 'No ratings';
    }
    const avg =
      product.ratings.reduce((a, b) => a + b, 0) / product.ratings.length;
    return avg.toFixed(2);
  }
}
