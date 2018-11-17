import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'formatStars'
  })
  export class FormatStarsPipe implements PipeTransform {
    format = function (stars) {
        let html = '';
        const countEmpty = 5 - stars;

        for (let i = 0; i < stars; i++) {
            html += '<i class="fa fa-star"></i>';
        }

        for (let x = 0; x < countEmpty; x++) {
            html += '<i class="fa fa-star-o"></i>';
        }
        return html;
  };

    transform(value: number): string {
      return this.format(value);
    }
  }
