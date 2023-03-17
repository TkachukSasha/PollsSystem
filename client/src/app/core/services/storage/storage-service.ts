import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  storeData(key: string, value: any) {
    localStorage.setItem(key, value);
  }

  getData(key: string){
    localStorage.getItem(key)
  }

  clearData(key: string){
    localStorage.removeItem(key)
  }
}
