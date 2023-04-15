import { Pipe, PipeTransform } from '@angular/core';
import { IPoll } from "../models/poll-model";

@Pipe({
  name: 'filterPolls'
})
export class FilterPollsPipe implements PipeTransform {
  transform(entities: IPoll[], searchValue: string): IPoll[] {
    if (!entities || !searchValue) {
      return entities;
    }

    return entities.filter(e => e.title.toLowerCase().includes(searchValue.toLowerCase()));
  }
}
