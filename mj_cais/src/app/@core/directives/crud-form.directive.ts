import {
  AfterViewChecked,
  ChangeDetectorRef,
  Directive,
  ElementRef,
  EventEmitter,
  Injector,
  ViewChild,
} from "@angular/core";
import { Location } from "@angular/common";
import {
  changei18n,
  ColumnPinningPosition,
  IPinningConfig,
} from "@infragistics/igniteui-angular";
import {
  ABaseModel,
  CrudService,
  EActions,
  FormComponent,
} from "@tl/tl-common";
import { IgxResourceStringsBG } from "igniteui-angular-i18n";
import { Observable } from "rxjs";
import { CustomToastrService } from "../services/common/custom-toastr.service";
import { FormGroup } from "@angular/forms";
import { BaseResolverData } from "../models/common/base-resolver.data";
import { InputTypeConstants } from "../constants/input-type.constants";
import {
  NbDialogService,
  NbTabComponent,
  NbTabsetComponent,
} from "@nebular/theme";
import * as fileSaver from "file-saver";
import { ErrorDialogComponent } from "../components/dialogs/error-dialog/error-dialog.component";

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

  public pinningConfig: IPinningConfig = { columns: ColumnPinningPosition.End };

  public readonly CREATE_ACTION = "create";
  public readonly EDIT_ACTION = "edit";

  public overrideDefaultBehaviour: boolean = false;
  public formFinishedLoading: EventEmitter<null> = new EventEmitter();
  public backUrl = "";

  public isForPreview: boolean;
  public fullForm: TForm;
  public dbData: TResolverData;
  public displayTitle: string;
  public isLoadingForm: boolean = false;

  protected navigateTimeout = 500;
  protected successMessage = "Успешно запазени данни!";
  protected dangerMessage = "Грешка при запазване на данните: ";
  protected validationMessage = "Грешка при валидациите!";

  @ViewChild("nbtabset") tabset: NbTabsetComponent;

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
    let location = this.injector.get<Location>(Location);
    let navigationState = location.getState() as any;
    // if in history has only current url
    if (navigationState?.navigationId < 2) {
      this.router.navigateByUrl("/");
    } else {
      location.back();
    }

    // let routerService = this.injector.get<RouterExtService>(RouterExtService);
    // let previousUrl = routerService.getPreviousUrl();
    // if (previousUrl && previousUrl != "/") {
    //   // When coming from different view
    //   this.redirectLocationBack();
    // } else {
    //   this.router.navigateByUrl(this.backUrl);
    // }
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

  protected saveAndNavigate() {
    let model = this.formObject;
    let submitAction: Observable<T>;
    if (this.isEdit()) {
      submitAction = this.service.update(this.formObject.id, model);
    } else {
      submitAction = this.service.save(model);
    }

    // prevent submit twice
    if (this.isLoadingForm === true) {
      return;
    }

    this.isLoadingForm = true
    submitAction.subscribe({
      next: (data) => {
        this.toastr.showToast("success", this.successMessage);
        setTimeout(() => {
          this.onSubmitSuccess(data);
        }, this.navigateTimeout);
      },
      error: (errorResponse) => {
        this.isLoadingForm = false;
        this.errorHandler(errorResponse);
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

      // search for server validation error
      if (validationSpan == null) {
        validationSpan = el.nativeElement.querySelector(
          "mat-error:not([type=hidden]) div.status-danger"
        );
      }

      if (validationSpan) {
        // select tab containing controls with validation errors
        let tab = validationSpan.closest("nb-tab");

        if (tab && this.tabset && this.tabset.tabs) {
          var tabParent = tab.parentElement;
          var index = Array.prototype.indexOf.call(tabParent.children, tab) - 1;
          var tabs = (this.tabset.tabs as any)._results;
          var hasElement = tabs && tabs.length > index;
          if (hasElement) {
            var element = tabs[index] as NbTabComponent;
            this.tabset.selectTab(element);
          }
        }

        var container = validationSpan.closest("div.ng-invalid");
        if (container) {
          container.scrollIntoView({ behavior: "smooth" });
        }
      }
    }, 300);
  }

  protected reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router
      .navigateByUrl("/pages/empty", { skipLocationChange: true })
      .then(() => {
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

  protected errorHandler(errorResponse): void {
    if (errorResponse.status == "401") {
      //this.router.navigateByUrl("pages");
      window.location.reload();
      return;
    }

    let title = this.dangerMessage;
    let errorText = errorResponse.status + " " + errorResponse.statusText;

    if (errorResponse.error && errorResponse.error.customMessage) {
      title = errorResponse.error.customMessage;
      errorText = "";
    }

    this.isLoadingForm = false;
    this.toastr.showBodyToast("danger", title, errorText);
    let errorInFormUpdated = false;

    // if has server side validation errors add them to the form control
    if (errorResponse.error.errors) {
      Object.keys(errorResponse.error.errors).forEach((prop) => {
        var propName = prop[0].toLocaleLowerCase() + prop.slice(1);
        const formControl = this.fullForm.group.get(propName);
        if (formControl) {
          // activate the error message
          errorInFormUpdated = true;
          formControl.setErrors({
            serverErrors: errorResponse.error.errors[prop],
          });
        }
      });
    }

    if (!errorInFormUpdated) {
      let dialogService = this.injector.get<NbDialogService>(NbDialogService);
      let message: string[] = [];

      // if has server side validation errors nut is in transactions
      if (errorResponse.error.errors) {
        message.push(errorResponse.title);
        (
          Object.keys(
            errorResponse.error.errors
          ) as (keyof typeof errorResponse.error.errors)[]
        ).forEach((key, index) => {
          // 👇️ name Tom 0, country Chile 1
          message.push(key.toString() + ":" + errorResponse.error.errors[key]);
          //console.log(key, element[key], index);
        });
      }

      if (errorResponse.error.error) {
        message.push(errorResponse.error.error);
      }

      let dialogRef = dialogService.open(ErrorDialogComponent, {
        context: {
          header: errorResponse.message,
          messages: message,
        },
        closeOnBackdropClick: false,
      });
    }

    this.scrollToValidationError();
  }

  protected downloadFile(response) {
    let blob = new Blob([response.body]);
    window.URL.createObjectURL(blob);

    let header = response.headers.get("Content-Disposition");
    let filenameRegex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;

    let fileName = "download";

    var matches = filenameRegex.exec(header);
    if (matches != null && matches[1]) {
      fileName = matches[1].replace(/['"]/g, "");
    }

    fileSaver.saveAs(blob, fileName);
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
