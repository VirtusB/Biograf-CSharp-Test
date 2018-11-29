import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'formatStars'
  })
  export class FormatStarsPipe implements PipeTransform {
    format = function (stars) {
        let html = '';
        const countEmpty = 5 - stars;

        for (let i = 0; i < stars; i++) {
            html += '<i class="fa fa-star fa-star-circle mr-1"></i>';
        }

        for (let x = 0; x < countEmpty; x++) {
            html += '<i class="fa fa-star-o fa-star-circle-o mr-1"></i>';
        }
        return html;
  };

    transform(value: number): string {
      return this.format(value);
    }
  }
