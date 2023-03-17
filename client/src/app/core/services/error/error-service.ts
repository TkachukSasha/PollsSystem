import {Injectable} from "@angular/core";
import { UiService } from "../ui/ui-service";

@Injectable({
  providedIn: 'root'
})
export class ErrorService{
  constructor(
    private _uiService: UiService
  )   {
  }

  displayError(errorMessage: string){
    this._uiService.openSnackBar(errorMessage, 200);
  }
}
