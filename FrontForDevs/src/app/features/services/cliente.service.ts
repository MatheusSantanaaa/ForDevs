import { HttpClient, HttpHeaders, HttpParams } from "@angular/common/http";
import { Observable, catchError, map, throwError } from "rxjs";
import { IServiceModelResponse } from "src/app/interfaces/response";
import { environment } from "src/enviroments/environment";
import { Cliente } from "../models/cliente";
import { parseModel } from "src/app/shared/constants/util";
import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root'
})
export class ClienteService {
  public headers: HttpHeaders;

  constructor(private httpClient: HttpClient) {
    const token = localStorage.getItem('currentToken');

     this.headers = new HttpHeaders({
      'Authorization': `Bearer ${token?.replace(/"/g, '')}`
    });
  }

  registrarCliente(formData: any): Observable<IServiceModelResponse<Cliente>> {
    const options = { headers: this.headers };
    return this.httpClient.post(`${environment.apiUrl}Cliente/registrar`, formData, options).pipe(
      map((response: any): IServiceModelResponse<Cliente> => {
        return {
          ...response,
          model: parseModel(response.model),
        };
      })
    );
  }

  atualizarCliente(formData: any): Observable<IServiceModelResponse<Cliente>> {
    const options = { headers: this.headers };
    return this.httpClient.put(`${environment.apiUrl}Cliente/atualizar`, formData, options).pipe(
      map((response: any): IServiceModelResponse<Cliente> => {
        return {
          ...response,
          model: parseModel(response.model),
        };
      })
    );
  }

  removerCliente(id: string): Observable<any> {
    const options = { headers: this.headers };
    return this.httpClient.delete(`${environment.apiUrl}Cliente/remover/${id}`, options);
  }

  obterClientes(): Observable<any> {
    const options = { headers: this.headers };
    return this.httpClient
    .get(`${environment.apiUrl}Cliente/obter-listagem`,options)
    .pipe(
      map((response: any): IServiceModelResponse<Cliente[]> => response.model),
      catchError(error => {
        return throwError(() => error);
      })
    );
  }

  obterPorId(id: string): Observable<Cliente> {
    const options = { headers: this.headers };
    return this.httpClient
    .get(`${environment.apiUrl}Cliente/obter/${id}`, options)
    .pipe(
      map((response: any): Cliente => response.model),
      catchError(error => {
        return throwError(() => error);
      })
    );
  }

  obterPorNome(nome: string): Observable<any> {
    const params = new HttpParams().set('nome', nome);
    return this.httpClient
    .get(`${environment.apiUrl}Cliente/obter-pelo-nome`, {params})
    .pipe(
      map((response: any): IServiceModelResponse<Cliente[]> => response.model),
      catchError(error => {
        return throwError(() => error);
      })
    );
  }
}
