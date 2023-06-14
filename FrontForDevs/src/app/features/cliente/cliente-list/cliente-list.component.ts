import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ToastService } from "../../services/toast.service";
import { Cliente } from "../../models/cliente";
import { ClienteService } from "../../services/cliente.service";
import { NgbModal, NgbModalOptions } from "@ng-bootstrap/ng-bootstrap";
import { AvaliacaoCreateComponent } from "../../avaliacoes/avaliacao-create/avaliacao-create.component";
import { ClienteCreateComponent } from "../cliente-create/cliente-create.component";
import { ClientesViewComponent } from "../cliente-view/cliente-view.component";

@Component({
  selector: 'app-clientes-list',
  templateUrl: './cliente-list.component.html',
  styleUrls: ['./cliente-list.component.scss'],
})
export class ClienteListComponent implements OnInit {
  public clientes: Cliente[] = [];
  termoPesquisa: string = '';

  ngOnInit(): void {
    this.obterLista();
  }

  constructor(
    private router: Router,
    private modalService: NgbModal,
    private toastService: ToastService,
    private clienteService: ClienteService){
  }

  criar() {
    const modalRef = this.modalService.open(ClienteCreateComponent, { backdrop: 'static' });

    modalRef.result.then((resultado) => {
      this.obterLista();
    }).catch((motivo) => {
      this.obterLista();
    });
  }

  visualizar(id: string){
    const modalRef = this.modalService.open(ClientesViewComponent, { backdrop: 'static' });
    modalRef.componentInstance._id = id;
  }

  editar(id: string) {
    const modalRef = this.modalService.open(ClienteCreateComponent, { backdrop: 'static' });
    modalRef.componentInstance.id = id;

    modalRef.result.then((resultado) => {
        this.obterLista();
    }).catch((motivo) => {
      this.obterLista();
    });
  }

  remover(id: string) {
    this.clienteService.removerCliente(id)
      .subscribe(response => {
        if(response) {
          this.toastService.showSuccessMessage("Cliente removido com sucesso", "Cliente");
          setTimeout(() => {
           this.obterLista();
          }, 1500);
        }
    })
  }

  obterLista(){
    this.clienteService.obterClientes()
      .subscribe(response => {
       this.clientes = response;
    })
  }

  pesquisarPorNome(item: string){
    console.log(item)
    this.clienteService.obterPorNome(item)
      .subscribe(response => {
       this.clientes = response;
    })
  }

}
