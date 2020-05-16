import { Component, Injector, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/app-component-base';
import {
  ApiKeysServiceProxy,
  CreateApiKeyDto,
} from '@shared/service-proxies/service-proxies';

@Component({
  templateUrl: 'create-apikey-dialog.component.html',
  styles: [
    `
      mat-form-field {
        width: 100%;
      }
      mat-checkbox {
        padding-bottom: 5px;
      }
    `,
  ],
})
export class CreateApiKeysDialogComponent extends AppComponentBase
  implements OnInit {
  saving = false;
  apikey: CreateApiKeyDto = new CreateApiKeyDto();

  constructor(
    injector: Injector,
    public _apikeyService: ApiKeysServiceProxy,
    private _dialogRef: MatDialogRef<CreateApiKeysDialogComponent>
  ) {
    super(injector);
  }

  public ngOnInit(): void {
    this._apikeyService
      .makeApiKey()
      .subscribe((apiKey) => (this.apikey = apiKey));
  }

  public save(): void {
    this.saving = true;

    this._apikeyService
      .create(this.apikey)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(() => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.close(true);
      });
  }

  public copyInputToClipboard(inputElement: HTMLInputElement) {
    inputElement.select();
    document.execCommand('copy');
    inputElement.setSelectionRange(0, 0);
  }

  public close(result: any): void {
    this._dialogRef.close(result);
  }
}
