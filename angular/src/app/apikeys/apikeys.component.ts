import { Component, Injector } from '@angular/core';
import { MatDialog } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
    PagedListingComponentBase, PagedRequestDto,
} from 'shared/paged-listing-component-base';
import {
    ApiKeysServiceProxy,
    ApiKeyDto,
    ApiKeyDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { CreateApiKeysDialogComponent } from './create-apikey/create-apikey-dialog.component';

@Component({
    templateUrl: './apikeys.component.html',
    animations: [appModuleAnimation()],
    styles: [
        `
          mat-form-field {
            padding: 10px;
          }
        `
      ]
})
export class ApiKeysComponent extends PagedListingComponentBase<ApiKeyDto> {
    apikeys: ApiKeyDto[] = [];
    keyword = '';
    isActive: boolean | null;

    constructor(
        injector: Injector,
        private _apikeyService: ApiKeysServiceProxy,
        private _dialog: MatDialog
    ) {
        super(injector);
    }

    list(
        request: PagedRequestDto,
        pageNumber: number,
        finishedCallback: Function
    ): void {

        this._apikeyService
            .getAll('', request.skipCount, request.maxResultCount)
            .pipe(
                finalize(() => {
                    finishedCallback();
                })
            )
            .subscribe((result: ApiKeyDtoPagedResultDto) => {
                this.apikeys = result.items;
                this.showPaging(result, pageNumber);
            });
    }

    delete(apikey: ApiKeyDto): void {
        abp.message.confirm(
            this.l('ApiKeyDeleteWarningMessage', apikey.apiKey),
            undefined,
            (result: boolean) => {
                if (result) {
                    this._apikeyService
                        .delete(apikey.id)
                        .pipe(
                            finalize(() => {
                                abp.notify.success(this.l('SuccessfullyDeleted'));
                                this.refresh();
                            })
                        )
                        .subscribe(() => { });
                }
            }
        );
    }

    createApiKey(): void {
        this.showCreateOrEditApiKeyDialog();
    }

    editApiKey(apikey: ApiKeyDto): void {
        this.showCreateOrEditApiKeyDialog(apikey.id);
    }

    showCreateOrEditApiKeyDialog(id?: number): void {
        const dialog = this._dialog.open(CreateApiKeysDialogComponent);

        dialog.afterClosed().subscribe(result => {
            if (result) {
                this.refresh();
            }
        });
    }
}
