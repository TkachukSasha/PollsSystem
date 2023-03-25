import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { StorageService } from "../services/storage/storage-service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private _storage: StorageService
  ) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    // @ts-ignore
    const data = JSON.parse(this._storage.getData('auth'));

    // @ts-ignore
    const access_token = data?.accessToken;

    console.log(`data from guard: ${access_token}`);

    if(!access_token){
      this.router.navigateByUrl("");
      return false;
    }

    return true;
  }

}
