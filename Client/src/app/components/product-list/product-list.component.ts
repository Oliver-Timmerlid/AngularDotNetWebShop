import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/product';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService.getAll().subscribe({
      next: (data) => {
        this.products = data;
      },
      error: (err) => {
        console.error('Failed to load products', err);
      },
    });
  }

  getAverageRating(product: Product): string {
    if (!product.ratings || product.ratings.length === 0) {
      return 'No ratings';
    }
    const avg =
      product.ratings.reduce((a, b) => a + b, 0) / product.ratings.length;
    return avg.toFixed(2);
  }
}
