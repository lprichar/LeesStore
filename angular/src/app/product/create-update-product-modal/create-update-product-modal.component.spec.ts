import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateUpdateProductModalComponent } from './create-update-product-modal.component';

describe('CreateUpdateProductModalComponent', () => {
  let component: CreateUpdateProductModalComponent;
  let fixture: ComponentFixture<CreateUpdateProductModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateUpdateProductModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateUpdateProductModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
