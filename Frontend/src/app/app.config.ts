import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient, withInterceptors } from '@angular/common/http';
import { errorInterceptor } from './shared/interceptors/error.interceptor';

/**
 * Configuration object for the Angular application.
 * 
 * This configuration defines the providers used throughout the application,
 * including zone change detection, routing, and HTTP client interceptors.
 * 
 * @constant
 * @type {ApplicationConfig}
 * 
 * @property {Array} providers - An array of providers used in the application.
 * 
 * - `provideZoneChangeDetection`: Configures Angular's zone change detection with event coalescing enabled.
 *   Event coalescing improves performance by batching multiple events into a single change detection cycle.
 * 
 * - `provideRouter`: Sets up the application's routing configuration using the `routes` object.
 * 
 * - `provideHttpClient`: Configures the HTTP client with interceptors. In this case, the `errorInterceptor`
 *   is used to handle HTTP errors globally across the application.
 */
export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }), 
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([ errorInterceptor ])
    ),
  ]
};
