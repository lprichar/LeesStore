import { mapEnumToOptions } from '@abp/ng.core';

export enum ProductType {
  Other = 0,
  Siren = 1,
  Mug = 2,
}

export const productTypeOptions = mapEnumToOptions(ProductType);
