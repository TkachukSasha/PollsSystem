import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatIconModule } from "@angular/material/icon";
import { FormsModule } from "@angular/forms";
import { SearchBarComponent } from './components/search-bar/search-bar.component';
import { FooterComponent } from './components/footer/footer.component';
import {RouterModule} from "@angular/router";
import { TimeBarComponent } from './components/time-bar/time-bar.component';

@NgModule({
  declarations: [
    NavBarComponent,
    SearchBarComponent,
    FooterComponent,
    TimeBarComponent
  ],
    exports: [
        NavBarComponent,
        SearchBarComponent,
        FooterComponent,
        TimeBarComponent
    ],
    imports: [
        CommonModule,
        MatSnackBarModule,
        MatIconModule,
        FormsModule,
        RouterModule
    ]
})
export class SharedModule { }
