import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { StorageService } from "../../../core/services/storage/storage-service";
import { Router } from "@angular/router";

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class NavBarComponent implements OnInit {

  constructor(
    private router: Router,
    private _storage: StorageService
  ) { }

  ngOnInit(): void {
  }

  onLogout() {
    this._storage.clearData('auth');
    this.router.navigateByUrl('');
  }
}
