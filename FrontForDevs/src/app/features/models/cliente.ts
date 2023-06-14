import { AvaliacaoCliente } from "./avaliacaoCliente";

export class Cliente{
  id: string;
  nomeDoCliente: string;
  nomeContato: string;
  cnpj: string;
  categoria: number;
  dataDeCriacao: Date;
  avaliacaoCliente: AvaliacaoCliente[]
}
