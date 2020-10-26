import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProductDto, ProductsService, productTypeOptions } from '@proxy/products';

@Component({
  selector: 'app-create-update-product-modal',
  templateUrl: './create-update-product-modal.component.html',
  styleUrls: ['./create-update-product-modal.component.scss'],
})
export class CreateUpdateProductModalComponent implements OnInit {
  public isModalOpen: boolean;
  public form: FormGroup;
  public productTypes = productTypeOptions;

  @Output()
  public productChanged: EventEmitter<any> = new EventEmitter<any>();

  constructor(private formBuilder: FormBuilder, private productService: ProductsService) {}

  public open(product?: ProductDto) {
    const currentProduct = product || {} as ProductDto;
    this.isModalOpen = true;
    this.form = this.formBuilder.group({
      name: [currentProduct.name, Validators.required],
      productType: [currentProduct.productType, Validators.required],
      quantity: [currentProduct.quantity, Validators.required],
      price: [currentProduct.price, Validators.required],
    });
  }

  public ngOnInit(): void {}

  public save() {
    this.productService.create(this.form.value).subscribe(() => {
      this.isModalOpen = false;
      this.form.reset();
      this.productChanged.emit();
    });
  }
}
