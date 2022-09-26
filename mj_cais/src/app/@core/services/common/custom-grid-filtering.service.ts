import { Injectable } from "@angular/core";
import { CrudService, FILTER_OPERATION, GridRemoteFilteringService } from "@tl/tl-common";

@Injectable({
    providedIn: 'root',
  })
export class CustomGridRemoteFilteringService<T, CS extends CrudService<T, any>> extends GridRemoteFilteringService<T,CS> {
  
     NULL_VALUE = null;
    protected _getFilterString(operand, fieldName, filterValue) {
        let filterString;
        switch (operand.condition.name) {
          case 'contains': {
            filterString = `${FILTER_OPERATION.CONTAINS}(tolower(${fieldName}), ${filterValue})`;
            break;
          }
          case 'true': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS}
             ${FILTER_OPERATION.TRUE}`;
            break;
          }
          case 'false': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} ${FILTER_OPERATION.FALSE}`;
            break;
          }
          case 'startsWith': {
            filterString = `${FILTER_OPERATION.STARTS_WITH}(${fieldName},${filterValue})`;
            break;
          }
          case 'endsWith': {
            filterString = `${FILTER_OPERATION.ENDS_WITH}(${fieldName},${filterValue})`;
            break;
          }
          case 'today': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} ${new Date().toJSON().substring(0,10)} `;
            break;
          }
          case 'yesterday': {
            let currentDate = new Date();
            currentDate.setDate(currentDate.getDate() - 1);
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} ${currentDate.toJSON().substring(0,10)} `;
            break;
          }
          case 'thisMonth': {
            //0..11
            filterString = `month(${fieldName}) ${FILTER_OPERATION.EQUALS} ${(new Date().getMonth() + 1).toString()} ${FILTER_OPERATION.AND} year(${fieldName}) ${FILTER_OPERATION.EQUALS} ${(new Date().getFullYear()).toString()} `;
            break;
          }
          case 'lastMonth': {
            let month = new Date().getMonth();
            let year = new Date().getFullYear();
            if(month == 0){
              month = 12;
              year = year -1;
            }
    
            filterString = `month(${fieldName}) ${FILTER_OPERATION.EQUALS} ${month} ${FILTER_OPERATION.AND} year(${fieldName}) ${FILTER_OPERATION.EQUALS} ${year} `;
            break;
          }
          case 'nextMonth': {
            let month = new Date().getMonth() + 2;
            let year = new Date().getFullYear();
            if(month > 12){
              month = 1;
              year = year + 1;
            }
    
            filterString = `month(${fieldName}) ${FILTER_OPERATION.EQUALS} ${month} ${FILTER_OPERATION.AND} year(${fieldName}) ${FILTER_OPERATION.EQUALS} ${year} `;
            break;
          }
          case 'thisYear': {
            filterString = `year(${fieldName}) ${FILTER_OPERATION.EQUALS} ${(new Date().getFullYear()).toString()} `;
            break;
          }
          case 'lastYear': {
            filterString = `year(${fieldName}) ${FILTER_OPERATION.EQUALS} ${(new Date().getFullYear() - 1).toString()} `;
            break;
          }
          case 'nextYear': {
            filterString = `year(${fieldName}) ${FILTER_OPERATION.EQUALS} ${(new Date().getFullYear() + 1).toString()} `;
            break;
          }
          case 'equals': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} ${filterValue} `;
            break;
          }
          case 'doesNotEqual': {
            filterString = `${fieldName} ${FILTER_OPERATION.DOES_NOT_EQUAL} ${filterValue} `;
            break;
          }
          case 'doesNotContain': {
            filterString = `${FILTER_OPERATION.DOES_NOT_CONTAIN}(${fieldName},${filterValue})`;
            break;
          }
          case 'greaterThan': {
            filterString = `${fieldName} ${FILTER_OPERATION.GREATER_THAN} ${filterValue} `;
            break;
          }
          case 'after':
          case 'greaterThanOrEqualTo': {
            filterString = `${fieldName} ${FILTER_OPERATION.GREATER_THAN_EQUAL} ${filterValue} `;
            break;
          }
          case 'lessThan': {
            filterString = `${fieldName} ${FILTER_OPERATION.LESS_THAN} ${filterValue} `;
            break;
          }
          case 'before':
          case 'lessThanOrEqualTo': {
            filterString = `${fieldName} ${FILTER_OPERATION.LESS_THAN_EQUAL} ${filterValue} `;
            break;
          }
          case 'empty': {
            filterString = `length(${fieldName}) ${FILTER_OPERATION.EQUALS} 0`;
            break;
          }
          case 'notEmpty': {
            filterString = `length(${fieldName}) ${FILTER_OPERATION.GREATER_THAN} 0`;
            break;
          }
          case 'null': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} ${this.NULL_VALUE}`;
            break;
          }
          case 'notNull': {
            filterString = `${fieldName} ${FILTER_OPERATION.DOES_NOT_EQUAL} ${this.NULL_VALUE}`;
            break;
          }
          case 'Администратор': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} 2`;
            break;
          }
          case 'Нормална': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} 0`;
            break;
          }
          case 'Публична': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} 1`;
            break;
          }
          case 'Чернова': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} 0`;
            break;
          }
          case 'Нов': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} 1`;
            break;
          }
          case 'Отхвърлен': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} 2`;
            break;
          }
          case 'Одобрен': {
            filterString = `${fieldName} ${FILTER_OPERATION.EQUALS} 3`;
            break;
          }
        }
    
        return filterString;
      }
}