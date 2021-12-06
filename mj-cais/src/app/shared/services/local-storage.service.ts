import { Injectable } from "@angular/core";
import { NGXLogger } from "ngx-logger";

@Injectable({
  providedIn: 'root',
})
export class LocalStorageService {
  constructor(
    private logger: NGXLogger
  ) {}

  public write(key: string, value: any): void {
    this.logger.debug(`Writing key: ${key} with value ${value} to local stroage`);
    localStorage.setItem(key, JSON.stringify(value));
  }

  public read(key: string): any {
    this.logger.debug(`Reading key: ${key} from local stroage`);
    const data = localStorage.getItem(key);
    if (data != null) {
      const value = JSON.parse(data);
      this.logger.debug(`Value: ${value} red local stroage`);
      return value;
    }
    this.logger.debug(`No key: ${key} found in local stroage`);
    return;
  }

  public remove(key: string) {
    this.logger.debug(`Removing key: ${key} from local stroage`);
    localStorage.removeItem(key);
  }
}
