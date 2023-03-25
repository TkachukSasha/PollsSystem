import { Pipe, PipeTransform } from '@angular/core';
import { IPoll } from "../models/poll-model";

@Pipe({
  name: 'filterPolls'
})
export class FilterPollsPipe implements PipeTransform {
  transform(entities: IPoll[], searchValue: string): IPoll[] {
    return entities.filter(e => e.title.toLowerCase().includes(searchValue.toLowerCase()));
  }
}
