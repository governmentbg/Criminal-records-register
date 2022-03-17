import {
  AfterViewChecked,
  ChangeDetectorRef,
  Directive,
  ElementRef,
  EventEmitter,
  Injector,
} from "@angular/core";
import { Location } from "@angular/common";
import { changei18n } from "@infragistics/igniteui-angular";
import {
  ABaseModel,
  CrudService,
  EActions,
  FormComponent,
} from "@tl/tl-common";
import { IgxResourceStringsBG } from "igniteui-angular-i18n";
import { Observable, of } from "rxjs";
import { CustomToastrService } from "../services/common/custom-toastr.service";
import { FormGroup } from "@angular/forms";
import { BaseResolverData } from "../models/common/base-resolver.data";
import { InputTypeConstants } from "../constants/input-type.constants";

@Directive()
export abstract class CrudForm<
    T extends ABaseModel,
    TForm extends { group: FormGroup },
    TResolverData extends BaseResolverData<T>,
    CS extends CrudService<T, string>
  >
  extends FormComponent<T, CS>
  implements AfterViewChecked
{
  constructor(service: CS, injector: Injector) {
    super(service, injector);
    this.setRouteParameters();

    this.formFinishedLoading.subscribe(() => {
      if (this.isForPreview) {
        this.fullForm.group.disable({ onlySelf: false });
      }
    });
  }

  public InputTypeConstants = InputTypeConstants;

  public readonly CREATE_ACTION = "create";
  public readonly EDIT_ACTION = "edit";

  public overrideDefaultBehaviour: boolean = false;
  public formFinishedLoading: EventEmitter<null> = new EventEmitter();
  public backUrl = "";

  public isForPreview: boolean;
  public fullForm: TForm;
  public dbData: TResolverData;
  public displayTitle: string;

  protected navigateTimeout = 500;
  protected successMessage = "Успешно запазени данни!";
  protected dangerMessage = "Грешка при запазване на данните: ";
  protected validationMessage = "Грешка при валидациите!";

  ngAfterViewChecked(): void {
    // This provides fix for:
    // Expression has changed after it was checked.
    // Previous value: 'ng-valid: true'. Current value: 'ng-valid: false'
    let changeDetector =
      this.injector.get<ChangeDetectorRef>(ChangeDetectorRef);
    changeDetector.detectChanges();
  }

  get toastr() {
    return this.injector.get<CustomToastrService>(CustomToastrService);
  }

  protected setDisplayTitle(objectName: string) {
    let prefix = "";
    if (this.isForPreview) {
      prefix = "Преглед на";
    } else if (this.currentAction == EActions.CREATE) {
      prefix = "Добавяне на";
    } else if (this.currentAction == EActions.EDIT) {
      prefix = "Редактиране на";
    }

    this.displayTitle = prefix + " " + objectName;
  }

  public globalCancelFunction = () => {
    this.router.navigateByUrl(this.backUrl);
  };

  protected validateAndSave(form: any) {
    console.log(form.group);
    if (!form.group.valid) {
      form.group.markAllAsTouched();
      this.toastr.showToast("danger", "Грешка при валидациите!");

      this.scrollToValidationError();
    } else {
      this.formObject = form.group.value;
      this.saveAndNavigate();
    }
  }

  private saveAndNavigate() {
    let model = this.formObject;
    let submitAction: Observable<T>;
    if (this.isEdit()) {
      submitAction = this.service.update(this.formObject.id, model);
    } else {
      submitAction = this.service.save(model);
    }

    submitAction.subscribe({
      next: (data) => {
        this.toastr.showToast("success", this.successMessage);

        setTimeout(() => {
          this.onSubmitSuccess(data);
        }, this.navigateTimeout);
      },
      error: (errorResponse) => {
        let title = this.dangerMessage;
        let errorText = errorResponse.status + " " + errorResponse.statusText;

        if (errorResponse.error && errorResponse.error.customMessage) {
          title = errorResponse.error.customMessage;
          errorText = "";
        }

        this.toastr.showBodyToast("danger", title, errorText);

        // if has server side validation errors add them to the form control
        Object.keys(errorResponse.error.errors).forEach((prop) => {
          var propName = prop[0].toLocaleLowerCase() + prop.slice(1);
          const formControl = this.fullForm.group.get(propName);
          if (formControl) {
            // activate the error message
            formControl.setErrors({
              serverErrors: errorResponse.error.errors[prop],
            });
          }
        });
      },
    });
  }

  protected onSubmitSuccess(data: any) {
    let currentUrl = this.router.url.toLocaleLowerCase();
    let index = currentUrl.indexOf(this.CREATE_ACTION);
    let editUrl = currentUrl.substr(0, index) + this.EDIT_ACTION;
    if (data?.id) {
      let newUrl = editUrl + "/" + data.id;
      this.router.navigateByUrl(newUrl);
    } else {
      // Important, if we modify some navigation properties they must refresh
      this.reloadCurrentRoute();
    }
  }

  protected scrollToValidationError() {
    setTimeout(() => {
      let el = this.injector.get<ElementRef>(ElementRef);

      var validationSpan = el.nativeElement.querySelector(
        "cais-validation-message:not([type=hidden]) div span"
      );

      if (validationSpan) {
        var container = validationSpan.closest("div.ng-invalid");
        if (container) {
          container.scrollIntoView({ behavior: "smooth" });
        }
      }
    }, 300);
  }

  protected reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl("/", { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  protected redirectLocationBack() {
    let location = this.injector.get<Location>(Location);
    location.back();
  }

  // Overriding tl-common ngOnInit, because it uses subscribe
  ngOnInit() {
    changei18n(IgxResourceStringsBG);

    if (this.currentAction == EActions.CREATE) {
      this.prepareForm();
    } else {
      this.readObjectDetails();
    }

    this.ngOnInitImplementation();
  }

  readObjectDetails() {
    if (this.overrideDefaultBehaviour) {
      return;
    }

    this.service.find(this.objectId).subscribe(
      (data) => {
        this.formObject = data;
        this.onBaseServiceLoaded();
        this.prepareForm();
        this.afterObjectReady();
      },
      (error) => {
        this.onBaseServiceError();
      }
    );
  }

  ngOnInitImplementation() {}

  public isEdit() {
    return this.currentAction == EActions.EDIT;
  }

  prepareForEdit(object: T): void {
    if (this.overrideDefaultBehaviour) {
      return;
    } else {
      super.prepareForEdit(object);
    }
  }

  prepareForCreate(): void {
    if (this.overrideDefaultBehaviour) {
      return;
    } else {
      super.prepareForCreate();
    }
  }

  private setRouteParameters(): void {
    this.isForPreview = this.activatedRoute.snapshot.data["preview"];

    this.name = this.activatedRoute.snapshot.data["name"];
    this.icon = this.activatedRoute.snapshot.data["icon"];

    this.objectId = this.activatedRoute.snapshot.paramMap.get("ID");
    if (!this.objectId) {
      this.currentAction = EActions.CREATE;
    } else {
      const editParam = this.activatedRoute.snapshot.data["edit"];
      if (editParam === true) {
        this.currentAction = EActions.EDIT;
      }
    }

    this.dbData = this.activatedRoute.snapshot.data["dbData"];
  }
}
