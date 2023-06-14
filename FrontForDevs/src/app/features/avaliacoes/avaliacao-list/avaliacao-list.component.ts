import { Component, OnInit } from "@angular/core";
import { Avaliacao } from "../../models/avaliacao";
import { Router } from "@angular/router";
import { ToastService } from "../../services/toast.service";
import { AvaliacaoService } from "../../services/avaliacao.service";

@Component({
  selector: 'app-avaliacao-list',
  templateUrl: './avaliacao-list.component.html',
  styleUrls: ['./avaliacao-list.component.scss'],
})
export class AvaliacaoListComponent implements OnInit {
  public avaliacoes: Avaliacao[] = [];
  termoPesquisa: string = '';

  ngOnInit(): void {
    this.obterLista();
  }

  constructor(
    private router: Router,
    private toastService: ToastService,
    private avaliacaoService: AvaliacaoService,
    ){
  }

  criar() {
    this.router.navigate([`/avaliacoes/avaliacao-create`]);
  }

  visualizar(id: string){
    this.router.navigate(['/avaliacoes/avaliacao-view', id]);
  }

  editar(id: string) {
    this.router.navigate([`/avaliacoes/avaliacao-edit/${id}`]);
  }

  remover(id: string) {
    this.avaliacaoService.removerAvaliacao(id)
      .subscribe(response => {
        if(response) {
          this.toastService.showSuccessMessage("Avaliação removido com sucesso", "Avaliação");
          setTimeout(() => {
           this.obterLista();
          }, 1500);
        }
    })
  }

  obterLista(){
    this.avaliacaoService.obterAvaliacoes()
      .subscribe(response => {
       this.avaliacoes = response;
    })
  }

}
