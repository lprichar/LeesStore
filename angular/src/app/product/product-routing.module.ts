import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProductComponent } from './product.component';
import { AuthGuard, PermissionGuard } from '@abp/ng.core';

const routes: Routes = [
  {
    path: '',
    component: ProductComponent,
    canActivate: [ AuthGuard, PermissionGuard ],
    data: {
      requiredPolicy: 'LeesStore.ViewEditProducts'
    }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProductRoutingModule { }
