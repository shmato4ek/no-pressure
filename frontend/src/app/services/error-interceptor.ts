import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { EMPTY, Observable, catchError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private readonly router: Router) {

    }
    intercept(
        req: HttpRequest<any>,
        next: HttpHandler
      ): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
          catchError((error) => {
            this.router.navigate([`/error/${error.status}`]);
            return EMPTY;
          })
        );
      }
}