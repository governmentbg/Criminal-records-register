import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root',
  })
export class UserAuthorityService {
    private _csAuthorityId: string;

    get csAuthorityId(): string {
        return this._csAuthorityId;
    }

    set csAuthorityId(newCsAuthorityId: string) {
        this._csAuthorityId = newCsAuthorityId;
    }
}