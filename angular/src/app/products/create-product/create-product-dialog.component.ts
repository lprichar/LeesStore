import {
  Component,
  Injector,
  OnInit,
  Output,
  EventEmitter
} from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import {
  ProductDto,
  ProductsServiceProxy
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-product-dialog.component.html'
})
export class CreateProductDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  product: ProductDto = new ProductDto();

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _productService: ProductsServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
  }

  save(): void {
    this.saving = true;

    this._productService
      .create(this.product)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      });
  }
}
