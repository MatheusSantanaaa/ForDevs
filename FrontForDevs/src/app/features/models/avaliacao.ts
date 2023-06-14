import { AvaliacaoCliente } from "./avaliacaoCliente";

export class Avaliacao{
  id: string;
  dataDeReferencia: Date;
  totalDeParticipantes: number;
  quantidadeDePromotores: number;
  quantidadeDeNeutros: number;
  quantidadeDeDetratores: number;
  resultadoGeral: number;
  avaliacaoClientes: AvaliacaoCliente[];
}
