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
  ProductsServiceProxy,
  ProductDto
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'edit-product-dialog.component.html'
})
export class EditProductDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  product: ProductDto = new ProductDto();
  id: number;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    public _productService: ProductsServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._productService.get(this.id).subscribe((result: ProductDto) => {
      this.product = result;
    });
  }

  save(): void {
    this.saving = true;

    this._productService
      .update(this.product)
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
