import type { AuditedEntityDto } from '@abp/ng.core';
import type { ProductType } from './product-type.enum';

export interface ProductDto extends AuditedEntityDto<number> {
  name: string;
  quantity: number;
  price: number;
  productType: ProductType;
}
