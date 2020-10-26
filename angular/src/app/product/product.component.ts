import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductDto, ProductsService } from '@proxy/products';
import { CreateUpdateProductModalComponent } from './create-update-product-modal/create-update-product-modal.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
  providers: [ListService],
})
export class ProductComponent implements OnInit {
  public product = { items: [], totalCount: 0 } as PagedResultDto<ProductDto>;
  public isModalOpen = false;

  constructor(public readonly listService: ListService, private productService: ProductsService) {}

  @ViewChild('createUpdateProductModal', { static: false })
  protected createUpdateProductModal: CreateUpdateProductModalComponent;

  public ngOnInit(): void {
    const productStreamCreator = query => this.productService.getList(query);

    this.listService.hookToQuery(productStreamCreator).subscribe(response => {
      this.product = response;
    });
  }

  public createProduct() {
    this.createUpdateProductModal.open();
  }

  public editProduct(product: ProductDto) {
    this.createUpdateProductModal.open(product);
  }

  public onProductChanged(): void {
    this.listService.get();
  }
}
