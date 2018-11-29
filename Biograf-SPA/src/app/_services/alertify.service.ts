import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {

constructor() { }

confirm(title: string, message: string, okCallback: () => any, errorCallback?: () => any) {
  alertify.confirm(title, message, function(e) {
    if (e) {
      okCallback();
    } else {}
  }, function(t) {
    if (t) {
      if (errorCallback && typeof errorCallback === 'function') {
        errorCallback();
      }
    } else {}
  }).set('labels', {ok: 'Ja', cancel: 'Annuller'}).set({transition: 'fade'});
}

success(message: string) {
  alertify.success(message);
}

error(message: string) {
  alertify.error(message);
}

warning(message: string) {
  alertify.warning(message);
}

message(message: string) {
  alertify.message(message);
}

}
