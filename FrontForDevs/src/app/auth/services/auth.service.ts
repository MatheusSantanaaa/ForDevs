import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, switchMap, tap, throwError } from 'rxjs';
import { IServiceModelResponse } from 'src/app/interfaces/response';
import { parseJwt, parseModel } from 'src/app/shared/constants/util';
import { environment } from 'src/enviroments/environment';
import { Usuario } from '../login/models/usuario';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private authUser: any;

  private token: string;

  private user: any;

  constructor(
    private httpClient: HttpClient,
  ) {
    const tokenString = localStorage.getItem('currentToken');

    if (tokenString !== null && tokenString !== 'undefined') {
      this.token = JSON.parse(tokenString);
      this.authUser = parseJwt(this.token);
    }

    if (this.token) {
      this.authUser = parseJwt(this.token);
    }
  }

  public getCurrentUser(): any {
    return this.user;
  }

  public getCurrentAuthUser(): any {
    return this.authUser;
  }

  public getCurrentToken(): string {
    return this.token;
  }

  public isLoggedIn(): boolean {
    return this.authUser ? true : false;
  }

  public login<IUser>(auth: string): Observable<any> {
    return this.httpClient
      .post<any>(`${environment.apiUrl}Autenticacao/entrar`, auth)
      .pipe(
        tap((response: any) => (this.token = response.model, this.authUser = response.model.userToken)),
        catchError(error => {
          return throwError(() => error);
        })
      );
  }

  public getToken<IUser>(): Observable<any> {
    return this.httpClient
      .post<any>(`${environment.apiUrl}Authentication/GetToken`, null)
      .pipe(
        tap(response => (this.token = response.model)),
        tap(response => localStorage.setItem('currentToken', JSON.stringify(response.model))),
        tap(() => {
          this.authUser = parseJwt(this.token);
          this.getUser(this.authUser.UserId);
        }),

        catchError(error => {
          return throwError(() => error);
        })
      );
  }

  private getUser(userId: string): void {
    this.httpClient
      .get(`${environment.apiUrl}User/get/unique/${userId}`)
      .pipe(map((response: any): any => response.model))
      .subscribe(user => {
        this.user = user;
      });
  }

  register(formData: any): Observable<IServiceModelResponse<Usuario>> {
    return this.httpClient.post(`${environment.apiUrl}Autenticacao/nova-conta`, formData).pipe(
      map((response: any): IServiceModelResponse<Usuario> => {
        return {
          ...response,
        };
      })
    );
  }

  resetPassword(email: string) {
    return;
  }

  logout() {
    this.authUser = null;
    this.token = '';
    localStorage.removeItem('currentToken');

    return;
  }


  registerUser(user: any): Observable<IServiceModelResponse<any>> {
    return this.httpClient
      .post(`${environment.apiUrl}User/register`, user)
      .pipe(map((response: any): IServiceModelResponse<any> => response));
  }

}
