import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, catchError, map, throwError } from "rxjs";
import { IServiceModelResponse } from "src/app/interfaces/response";
import { environment } from "src/enviroments/environment";
import { parseModel } from "src/app/shared/constants/util";
import { Injectable } from "@angular/core";
import { Avaliacao } from "../models/avaliacao";

@Injectable({
  providedIn: 'root'
})
export class  AvaliacaoService {
  public headers: HttpHeaders;

  constructor(private httpClient: HttpClient) {
    const token = localStorage.getItem('currentToken');

     this.headers = new HttpHeaders({
      'Authorization': `Bearer ${token?.replace(/"/g, '')}`
    });
  }

  registrarAvaliacao(formData: any): Observable<IServiceModelResponse<Avaliacao>> {
    const options = { headers: this.headers };
    return this.httpClient.post(`${environment.apiUrl}Avaliacao/registrar`, formData, options).pipe(
      map((response: any): IServiceModelResponse<Avaliacao> => {
        return {
          ...response,
          model: parseModel(response.model),
        };
      })
    );
  }

  atualizarAvaliacao(formData: any): Observable<IServiceModelResponse<Avaliacao>> {
    const options = { headers: this.headers };
    return this.httpClient.put(`${environment.apiUrl}Avaliacao/atualizar`, formData, options).pipe(
      map((response: any): IServiceModelResponse<Avaliacao> => {
        return {
          ...response,
          model: parseModel(response.model),
        };
      })
    );
  }

  removerAvaliacao(id: string): Observable<any> {
    const options = { headers: this.headers };
    return this.httpClient.delete(`${environment.apiUrl}Avaliacao/remover/${id}`, options);
  }

  obterAvaliacoes(): Observable<any> {
   const options = { headers: this.headers };

    return this.httpClient
    .get(`${environment.apiUrl}Avaliacao/obter-listagem`, options)
    .pipe(
      map((response: any): IServiceModelResponse<Avaliacao[]> => response.model),
      catchError(error => {
        return throwError(() => error);
      })
    );
  }

  obterPorId(id: string): Observable<Avaliacao> {
    const options = { headers: this.headers };
    return this.httpClient
    .get(`${environment.apiUrl}Avaliacao/obter/${id}`, options)
    .pipe(
      map((response: any): Avaliacao => response.model),
      catchError(error => {
        return throwError(() => error);
      })
    );
  }
}
