import { Component, Injector, OnInit } from '@angular/core';
import { RemoteGridWithStatePersistance } from '../../../@core/directives/remote-grid-with-state-persistance.directive';
import { UserCitizenGridService } from './_data/user-citizen-grid.service';
import { UserCitizenGridModel } from './_models/user-citizen.grid.model';

@Component({
  selector: 'cais-users-citizen-overview',
  templateUrl: './users-citizen-overview.component.html',
  styleUrls: ['./users-citizen-overview.component.scss']
})
export class UsersCitizenOverviewComponent extends RemoteGridWithStatePersistance<
UserCitizenGridModel,
UserCitizenGridService
> {
  
  constructor(
    service: UserCitizenGridService,
    injector: Injector
    ) {
      super("users-citizen", service, injector);
  }

  ngOnInit(): void {
    super.ngOnInit();
  }
  navigateToPerson(egn: string){
    this.service.findByEgn(egn).subscribe(
      {
        next: res =>{
          this.router.navigateByUrl(`/pages/people/preview/${res}`);
        },
        error: error => {
          if (error.status == "404"){
            this.toastr.showBodyToast("danger", "Грешка", "Не е намерено лице!")
          } else if(error.status == "400") {
            this.toastr.showBodyToast("danger", "Грешка", "Намерено е повече от едно лице!")
          }
        }
      }
    );
  }
}
