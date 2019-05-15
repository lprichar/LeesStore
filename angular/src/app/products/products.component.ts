import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase, PagedRequestDto,
} from 'shared/paged-listing-component-base';
import {
    ProductsServiceProxy,
    ProductDto,
    PagedResultDtoOfProductDto
} from '@shared/service-proxies/service-proxies';
import { CreateProductDialogComponent } from './create-product/create-product-dialog.component';
import { EditProductDialogComponent } from './edit-product/edit-product-dialog.component';

@Component({
    templateUrl: './products.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
      ]
})
export class ProductsComponent extends PagedListingComponentBase<ProductDto> {
    products: ProductDto[] = [];
    keyword = '';
    isActive: boolean | null;

    constructor(
        injector: Injector,
        private _productService: ProductsServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    list(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {

        this._productService
            .getAll(null, request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: PagedResultDtoOfProductDto) => {
                this.products = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(product: ProductDto): void {
        abp.message.confirm(
            this.l('ProductDeleteWarningMessage', product.name),
            (result: boolean) => {
                if (result) {
                    this._productService
                        .delete(product.id)
                        .pipe(
                            finalize(() => {
                                abp.notify.success(this.l('SuccessfullyDeleted'));
                                this.refresh();
                            })
                        )
                        .subscribe(() => { });
                }
            }
        );
    }

    createProduct(): void {
        this.showCreateOrEditProductDialog();
    }

    editProduct(product: ProductDto): void {
        this.showCreateOrEditProductDialog(product.id);
    }

    showCreateOrEditProductDialog(id?: number): void {
        let createOrEditProductDialog;
        if (id === undefined || id <= 0) {
            createOrEditProductDialog = this._dialog.open(CreateProductDialogComponent);
        } else {
            createOrEditProductDialog = this._dialog.open(EditProductDialogComponent, {
                data: id
            });
        }

        createOrEditProductDialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
