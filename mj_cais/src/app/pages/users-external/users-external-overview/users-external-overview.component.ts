import { ChangeDetectorRef, Component, Injector, OnInit } from '@angular/core';
import { ConnectedPositioningStrategy, FilteringExpressionsTree, FilteringLogic, HorizontalAlignment, IgxBooleanFilteringOperand, IgxStringFilteringOperand, NoOpScrollStrategy, VerticalAlignment } from '@infragistics/igniteui-angular';
import { RemoteGridWithStatePersistance } from '../../../@core/directives/remote-grid-with-state-persistance.directive';
import { DateFormatService } from '../../../@core/services/common/date-format.service';
import { UserExternalGridService } from './_data/user-external-grid.service';
import { UserExternalGridModel } from './_models/user-external.grid.model';

enum FilterTypes {
  reportsAll,
  reportsForRegistration,

  eCertificatesAll,
  eCertificatesForRegistration,
  eCertificatesDenied
}

@Component({
  selector: 'cais-users-external-overview',
  templateUrl: './users-external-overview.component.html',
  styleUrls: ['./users-external-overview.component.scss']
})
export class UsersExternalOverviewComponent  extends RemoteGridWithStatePersistance<
UserExternalGridModel,
UserExternalGridService
> {
  public now = new Date();
  public filterTypes = FilterTypes;
  
  public overlaySettings = {
    positionStrategy: new ConnectedPositioningStrategy({
      horizontalDirection: HorizontalAlignment.Left,
      horizontalStartPoint: HorizontalAlignment.Right,
      verticalStartPoint: VerticalAlignment.Bottom,
    }),
    scrollStrategy: new NoOpScrollStrategy(),
  };
constructor(
  service: UserExternalGridService,
  injector: Injector,
  public dateFormatService: DateFormatService
) {
  super("users-external", service, injector);
}

  ngOnInit(): void {
    super.ngOnInit();
  }

  filterUsers(filterType: FilterTypes): void{
    this.grid.clearFilter();

    const gridFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And);

    switch(filterType){
      case FilterTypes.eCertificatesAll:{        
        const egnFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'egn');
        const egnExpression = {
            condition: IgxStringFilteringOperand.instance().condition('notNull'),
            fieldName: 'egn',
            ignoreCase: true,
            searchVal: null
        };
        egnFilteringExpressionsTree.filteringOperands.push(egnExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(egnFilteringExpressionsTree);


        const deniedFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'denied');
        const deniedExpression = {
            condition: IgxStringFilteringOperand.instance().condition('null'),
            fieldName: 'denied',
            ignoreCase: true,
            searchVal: null
        };
        deniedFilteringExpressionsTree.filteringOperands.push(deniedExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(deniedFilteringExpressionsTree);
        
        this.grid.filteringExpressionsTree = gridFilteringExpressionsTree;
        break;
      }
      case FilterTypes.eCertificatesDenied:{
        
        const egnFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'egn');
        const egnExpression = {
            condition: IgxStringFilteringOperand.instance().condition('notNull'),
            fieldName: 'egn',
            ignoreCase: true,
            searchVal: null
        };
        egnFilteringExpressionsTree.filteringOperands.push(egnExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(egnFilteringExpressionsTree);


        const deniedFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'denied');
        const deniedExpression = {
            condition: IgxBooleanFilteringOperand.instance().condition('true'),
            fieldName: 'denied',
            ignoreCase: true,
            searchVal: null
        };
        deniedFilteringExpressionsTree.filteringOperands.push(deniedExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(deniedFilteringExpressionsTree);
        
        this.grid.filteringExpressionsTree = gridFilteringExpressionsTree;

        break;
      }
      case FilterTypes.eCertificatesForRegistration:{
        
        const egnFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'egn');
        const egnExpression = {
            condition: IgxStringFilteringOperand.instance().condition('notNull'),
            fieldName: 'egn',
            ignoreCase: true,
            searchVal: null
        };
        egnFilteringExpressionsTree.filteringOperands.push(egnExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(egnFilteringExpressionsTree);

        const activeFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'active');
        const activeExpression = {
            condition: IgxBooleanFilteringOperand.instance().condition('false'),
            fieldName: 'active',
            ignoreCase: true,
            searchVal: null
        };
        activeFilteringExpressionsTree.filteringOperands.push(activeExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(activeFilteringExpressionsTree);


        const deniedFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'denied');
        const deniedExpression = {
            condition: IgxStringFilteringOperand.instance().condition('null'),
            fieldName: 'denied',
            ignoreCase: true,
            searchVal: null
        };
        deniedFilteringExpressionsTree.filteringOperands.push(deniedExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(deniedFilteringExpressionsTree);
        
        this.grid.filteringExpressionsTree = gridFilteringExpressionsTree;

        break;
      }
      case FilterTypes.reportsAll:{

        const userNameFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'userName');
        const userNameExpression = {
            condition: IgxStringFilteringOperand.instance().condition('notNull'),
            fieldName: 'userName',
            ignoreCase: true,
            searchVal: null
        };
        userNameFilteringExpressionsTree.filteringOperands.push(userNameExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(userNameFilteringExpressionsTree);
        
        this.grid.filteringExpressionsTree = gridFilteringExpressionsTree;
        break;
      }
      case FilterTypes.reportsForRegistration:{        
        const activeFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'active');
        const activeExpression = {
            condition: IgxBooleanFilteringOperand.instance().condition('false'),
            fieldName: 'active',
            ignoreCase: true,
            searchVal: null
        };
        activeFilteringExpressionsTree.filteringOperands.push(activeExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(activeFilteringExpressionsTree);

        const userNameFilteringExpressionsTree = new FilteringExpressionsTree(FilteringLogic.And, 'userName');
        const userNameExpression = {
            condition: IgxStringFilteringOperand.instance().condition('notNull'),
            fieldName: 'userName',
            ignoreCase: true,
            searchVal: null
        };
        userNameFilteringExpressionsTree.filteringOperands.push(userNameExpression);        
        gridFilteringExpressionsTree.filteringOperands.push(userNameFilteringExpressionsTree);
        
        this.grid.filteringExpressionsTree = gridFilteringExpressionsTree;
        break;
      }
    }
  }

  unlock(id: string){
    this.service.unlock(id).subscribe({
      next: 
        data => {
          this.toastr.showToast("success", "Успешно отключен потребител");
          this.sortingDone({});
        },
      error: 
        error => 
          this.toastr.showBodyToast("danger", "Error", error)
    });
  }
}
