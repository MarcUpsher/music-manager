import { Pipe, PipeTransform } from '@angular/core';

@Pipe({name: 'duration'})
export class DurationPipe implements PipeTransform {
  transform(value: number): string {
    const minutes = Math.floor(value / 60);
    return minutes.toString() + ':' + (value - minutes * 60).toString().padStart(2, '0');
  }
}
