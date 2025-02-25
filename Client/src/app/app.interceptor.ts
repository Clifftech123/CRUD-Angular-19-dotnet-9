import { HttpEvent,  HttpHandlerFn,HttpInterceptorFn, HttpRequest} from '@angular/common/http';
import { Observable } from 'rxjs';

export const appInterceptor: HttpInterceptorFn = (
  req: HttpRequest<unknown>, 
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
  const baseUrl = 'https://localhost:7277/api/';
  return next(req.clone({ url: baseUrl + req.url }));
};