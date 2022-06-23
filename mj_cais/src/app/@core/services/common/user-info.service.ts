import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root',
  })
export class UserInfoService {
    private _csAuthorityId: string;
    private _userId: string;

    get csAuthorityId(): string {
        return this._csAuthorityId;
    }

    get userId(): string {
        return this._userId;
    }

    set csAuthorityId(newCsAuthorityId: string) {
        this._csAuthorityId = newCsAuthorityId;
    }
    set userId(newUserId: string) {
        this._userId = newUserId;
    }
}