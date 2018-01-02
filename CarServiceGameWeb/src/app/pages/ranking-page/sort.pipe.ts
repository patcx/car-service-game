import { Pipe, PipeTransform } from '@angular/core';
import { SortingValue } from '../../services/ranking.service';

@Pipe({
  name: 'sort'
})
export class SortPipe implements PipeTransform {

  transform(value: SortingValue, args?: any): string {
    let ret;
    switch (value) {
      case SortingValue.CASH:
        return "By Cash Balance";
      case SortingValue.WORKERS:
        return "By Number Of Workers";
      case SortingValue.ORDERS:
        return "By Number Of Completed Orders"
      case SortingValue.EFFICIENCY:
        return "By Efficiency";
      case SortingValue.NAME:
        return "By Name";
    }
  }

}
