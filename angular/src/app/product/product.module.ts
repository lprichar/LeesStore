import { NgModule } from '@angular/core';
import { ProductRoutingModule } from './product-routing.module';
import { ProductComponent } from './product.component';
import { SharedModule } from '../shared/shared.module';
import { CreateUpdateProductModalComponent } from './create-update-product-modal/create-update-product-modal.component';

@NgModule({
  declarations: [ProductComponent, CreateUpdateProductModalComponent],
  imports: [
    SharedModule,
    ProductRoutingModule
  ]
})
export class ProductModule { }
