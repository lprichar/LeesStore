import { ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ProductDto, ProductsService } from '@proxy/products';
import { CreateUpdateProductModalComponent } from './create-update-product-modal/create-update-product-modal.component';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss'],
  providers: [ListService],
})
export class ProductComponent implements OnInit {
  public product: PagedResultDto<ProductDto> = { items: [], totalCount: 0 } as PagedResultDto<
    ProductDto
  >;
  public isModalOpen = false;

  constructor(
    public readonly listService: ListService,
    private productService: ProductsService,
    private confirmationService: ConfirmationService
  ) {}

  @ViewChild('createUpdateProductModal', { static: false })
  protected createUpdateProductModal: CreateUpdateProductModalComponent;

  public ngOnInit(): void {
    const productStreamCreator = query => this.productService.getList(query);

    this.listService.hookToQuery(productStreamCreator).subscribe(response => {
      this.product = response;
    });
  }

  public create() {
    this.createUpdateProductModal.open();
  }

  public edit(product: ProductDto) {
    this.createUpdateProductModal.open(product);
  }

  public delete(product: ProductDto) {
    this.confirmationService
      .warn('::AreYouSureToDelete', '::AreYouSure', { messageLocalizationParams: [product.name] })
      .subscribe(status => {
        if (status === Confirmation.Status.confirm) {
          this.productService.delete(product.id).subscribe(this.listService.get.bind(this));
        }
      });
  }

  public incrementQuantity(product: ProductDto) {
    this.productService.incrementQuantityByProductId(product.id).subscribe(this.listService.get.bind(this));
  }

  public onProductChanged(): void {
    this.listService.get();
  }
}
