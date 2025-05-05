import {
  HttpInterceptorFn,
  HttpRequest,
  HttpEvent,
  HttpErrorResponse,
  HttpHandlerFn,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { UiService } from '../services/ui.service';

export const errorInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>,
  next: HttpHandlerFn,
): Observable<HttpEvent<unknown>> => {

  const ui = inject(UiService);

  return next(req)
  .pipe(
    catchError((err: HttpErrorResponse) => {

      switch (err.status) {

        // 400 for invalid requests (e.g. negative budget)
        case 400:
          ui.showError('Bad Request: one or more fields are invalid.');
          break;

        // 401 for requests without API key
        case 401:
          ui.showError('Unauthorized: API key is missing.');
          break;

        // 403 for requests with invalid API key
        case 403:
          ui.showError('Forbidden: API key is invalid.');
          break;

        // 404 for non-existing ID
        case 404:
          ui.showError('Not Found: the requested resource does not exist.');
          break;

        // For example, 500 for server errors
        default:
          ui.showError('An unexpected error occurred. Please try again later.');
          break;
      }

      return throwError(() => err);
    }),
  );
};
