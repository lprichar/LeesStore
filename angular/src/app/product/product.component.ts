import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { ProductDto, ProductsService } from '@proxy/products';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
  providers: [ListService],
})
export class ProductComponent implements OnInit {
  product = { items: [], totalCount: 0 } as PagedResultDto<ProductDto>;

  constructor(public readonly list: ListService, private productService: ProductsService) {}

  ngOnInit(): void {
    const productStreamCreator = query => this.productService.getList(query);

    this.list.hookToQuery(productStreamCreator).subscribe(response => {
      this.product = response;
    });
  }
}
