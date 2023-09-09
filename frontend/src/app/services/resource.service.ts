import { Injectable } from '@angular/core';
import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
  HttpParams,
  HttpResponse,
} from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export abstract class ResourceService<T> {
    private readonly APIUrl = environment.apiUrl + this.getResourceUrl();
    public headers = new HttpHeaders();

    constructor(protected httpClient: HttpClient) {}
    
    abstract getResourceUrl(): string;

    add<TRequest, TResponse>(
        resource: TRequest
      ): Observable<HttpResponse<TResponse>> {
        return this.httpClient
          .post<TResponse>(`${this.APIUrl}`, resource, { observe: 'response' })
          .pipe(catchError(this.handleError));
      }

    update<TRequest, TResponse>(
      resource: TRequest
    ): Observable<HttpResponse<TResponse>> {
      return this.httpClient
        .put<TResponse>(`${this.APIUrl}`, resource, {observe: 'response' })
        .pipe(catchError(this.handleError));
    }

    get(id: string | number): Observable<HttpResponse<T>> {
      return this.httpClient
        .get<T>(`${this.APIUrl}/${id}`, { observe: 'response' })
        .pipe(catchError(this.handleError));
    }  

    private handleError(error: HttpErrorResponse) {
        return throwError(() => error);
    }

    public getFullRequest<TRequest>(
      url: string,
      httpParams?: HttpParams
    ): Observable<HttpResponse<TRequest>> {
      return this.httpClient.get<TRequest>(`${environment.apiUrl}/${url}`, {
        observe: 'response',
        headers: this.getHeaders(),
        params: httpParams,
      });
    }

    delete(id: string | number): Observable<HttpResponse<T>> {
      return this.httpClient
        .delete<T>(`${this.APIUrl}/${id}`, { observe: 'response' })
        .pipe(catchError(this.handleError));
    }
    
    patch<TRequest>(request:TRequest): Observable<HttpResponse<TRequest>> {
      return this.httpClient.patch<TRequest>(`${environment.apiUrl}/activity/state`, request, {
        observe: 'response',
        headers: this.getHeaders(),
      });
    }

    private getHeaders() {
      return this.headers;
    }
}