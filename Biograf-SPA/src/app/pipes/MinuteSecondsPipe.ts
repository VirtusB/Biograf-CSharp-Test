import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'minuteSeconds'
  })
  export class MinuteSecondsPipe implements PipeTransform {
    calc = function (time) {
      if (time < 60) {
          return (time) + 'm';
      } else if (time % 60 === 0) {
          return (time - time % 60) / 60 + 'h';
      } else {
          return ((time - time % 60) / 60 + 'h' + ' ' + time % 60 + 'm');
      }
  };

    transform(value: number): string {
      return this.calc(value);
    }
  }
