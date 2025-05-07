import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

/**
 * BaseHttpService is a reusable service that provides basic HTTP methods
 * (GET, POST, PUT, DELETE) for interacting with an API. It uses Angular's
 * HttpClient and environment variables for configuration.
 */
@Injectable({
  providedIn: 'root',
})
export class BaseHttpService {

  /**
   * Injects Angular's HttpClient for making HTTP requests.
   */
  private http: HttpClient = inject(HttpClient);

  /**
   * Default HTTP options used for all requests, including headers for
   * content type, CORS, and API key authentication.
   */
  private readonly httpOption = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      'Access-Control-Allow-Origin': '*',
      'Access-Control-Allow-Methods': 'GET,POST,PUT,DELETE',
      [environment.API_KEY_HEADER]: environment.API_KEY,
    }),
  };

  /**
   * Base URL for the API, retrieved from environment variables.
   */
  private apiUrl: string = environment.API_URL;

  /**
   * Sends a GET request to the specified API path.
   * @param path - The endpoint path to append to the base API URL.
   * @returns An Observable of the response body, typed as T.
   */
  protected get<T>(path: string): Observable<T> {
    return this.http.get<T>(`${this.apiUrl}${path}`, this.httpOption);
  }

  /**
   * Sends a POST request to the specified API path with a request body.
   * @param path - The endpoint path to append to the base API URL.
   * @param body - The request payload to send in the body of the POST request.
   * @returns An Observable of the response body, typed as T.
   */
  protected post<T>(path: string, body: any): Observable<T> {
    return this.http.post<T>(`${this.apiUrl}${path}`, body, this.httpOption);
  }

  /**
   * Sends a PUT request to the specified API path with a request body.
   * @param path - The endpoint path to append to the base API URL.
   * @param body - The request payload to send in the body of the PUT request.
   * @returns An Observable of the response body, typed as T.
   */
  protected put<T>(path: string, body: any): Observable<T> {
    return this.http.put<T>(`${this.apiUrl}${path}`, body, this.httpOption);
  }

  /**
   * Sends a DELETE request to the specified API path.
   * @param path - The endpoint path to append to the base API URL.
   * @returns An Observable of the response body, typed as T.
   */
  protected delete<T>(path: string): Observable<T> {
    return this.http.delete<T>(`${this.apiUrl}${path}`, this.httpOption);
  }
}