import type { ProductType } from './product-type.enum';
import type { AuditedEntityDto } from '@abp/ng.core';

export interface CreateProductDto {
  name: string;
  quantity: number;
  price: number;
  productType: ProductType;
}

export interface ProductDto extends AuditedEntityDto<number> {
  name: string;
  quantity: number;
  price: number;
  productType: ProductType;
}
