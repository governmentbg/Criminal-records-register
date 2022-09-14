import { Component, Injector, OnInit } from '@angular/core';
import { RemoteGridWithStatePersistance } from '../../../@core/directives/remote-grid-with-state-persistance.directive';
import { UserExternalGridService } from './_data/user-external-grid.service';
import { UserExternalGridModel } from './_models/user-external.grid.model';

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
constructor(
  service: UserExternalGridService,
  injector: Injector
) {
  super("users-external", service, injector);
}

  ngOnInit(): void {
    super.ngOnInit();
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
