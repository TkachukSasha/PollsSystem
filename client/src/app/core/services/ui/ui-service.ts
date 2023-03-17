import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";

@Injectable({
  providedIn: 'root'
})

export class  UiService{
  constructor(
    private snackBar: MatSnackBar
  ) {
  }

  openSnackBar(message: string, duration: number){
    this.snackBar.open(message, '', {
      duration: duration,
      verticalPosition: 'top',
      horizontalPosition : 'right'
    })
  }
}
