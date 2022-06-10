import { Pipe, PipeTransform } from '@angular/core';
import { BrandMap } from './services/brand-map';

@Pipe({
  name: 'brandCode'
})
export class BrandCodePipe implements PipeTransform {
  transform(id: string, map: BrandMap): string {
    return map[id] ?? id;
  }
}
