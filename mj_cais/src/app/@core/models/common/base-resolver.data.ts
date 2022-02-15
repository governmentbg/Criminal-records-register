import { Observable, ObservableInput } from "rxjs";

export class BaseResolverData<T>
  implements Record<string, ObservableInput<any>>
{
  [x: string]: ObservableInput<any>;
  public element: Observable<T>;
}
