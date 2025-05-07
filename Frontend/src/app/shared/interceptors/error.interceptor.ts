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

/**
 * Interceptor function to handle HTTP errors and provide user-friendly error messages.
 * 
 * This function intercepts HTTP requests and processes any errors that occur during the request lifecycle.
 * It uses the `UiService` to display appropriate error messages to the user based on the HTTP status code.
 * 
 * @param req - The HTTP request being intercepted.
 * @param next - The next handler in the HTTP pipeline.
 * @returns An observable of the HTTP event, or an error observable if an error occurs.
 * 
 * ### Error Handling:
 * - **400 (Bad Request):** Indicates that one or more fields in the request are invalid.
 * - **401 (Unauthorized):** Indicates that the API key is missing.
 * - **403 (Forbidden):** Indicates that the API key is invalid.
 * - **404 (Not Found):** Indicates that the requested resource does not exist.
 * - **Default:** Handles any other unexpected errors with a generic error message.
 * 
 * ### Example Usage:
 * This interceptor can be registered in the Angular HTTP interceptor chain to automatically handle errors
 * for all outgoing HTTP requests.
 * 
 * ### Dependencies:
 * - `UiService`: Used to display error messages to the user.
 * - `HttpInterceptorFn`: Angular's functional HTTP interceptor type.
 * - `HttpRequest`, `HttpHandlerFn`, `HttpEvent`: Angular's HTTP request/response types.
 * - `catchError`: RxJS operator to handle errors in the observable stream.
 * - `throwError`: RxJS function to propagate errors downstream.
 */
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
