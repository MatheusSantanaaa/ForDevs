import { Component, Input, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { ToastService } from "../../services/toast.service";
import { Cliente } from "../../models/cliente";
import { ClienteService } from "../../services/cliente.service";
import { take } from "rxjs";
import { NgbActiveModal, NgbModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-cliente-create',
  templateUrl: './cliente-create.component.html',
  styleUrls: ['./cliente-create.component.scss'],
})
export class ClienteCreateComponent implements OnInit {
  @Input() id: string;
  @Input() view: boolean;
  public formGroup: FormGroup;
  public parcelasForms = [];
  public campoObrigatorio: string = "Campo Obrigatório"

  constructor(
    private formBuilder: FormBuilder,
    public activeModal: NgbActiveModal,
    private clienteService: ClienteService,
    private toastService: ToastService){
      this.view = false;
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      nomeDoCliente: ['', Validators.required],
      nomeContato: ['', Validators.required],
      cnpj: ['', Validators.required],
    });

    if(this.id){
      this.getCliente(this.id);
    }
  }

  salvar(){
    this.formGroup.markAllAsTouched();

    if (this.formGroup.invalid) return;

    const formValues = this.formGroup.getRawValue();
    const values = {
      ...formValues,
    };

    if(this.id){
      this.atualizarCliente(values);
    }else{
      this.registrarCliente(values);
    }
  }

  registrarCliente(divida: Cliente){
    this.clienteService.registrarCliente(divida)
    .pipe(take(1))
    .subscribe(resp =>{
      if(resp.success){
        this.toastService.showSuccessMessage("Cliente Cadastrado com sucesso", "Cliente");
        this.activeModal.close();
      }
    });
  }

  atualizarCliente(divida: Cliente){
    divida.id = this.id;
    this.clienteService.atualizarCliente(divida)
    .pipe(take(1))
    .subscribe(resp =>{
      if(resp.success){
        this.toastService.showSuccessMessage("Cliente Atualizado com sucesso", "Cliente");
        this.voltar();
      }
    });
  }

  getCliente(id: string): Promise<any> {
    return new Promise((resolve, reject) => {
      this.clienteService.obterPorId(id)
      .pipe(take(1))
      .subscribe(response => {
        console.log(response)
        this.formGroup.patchValue(response);
        this.formGroup.patchValue({
          id: id
        });
        resolve(response);
      }, error => {
        reject(error);
      });
    });
  }

  voltar(): void {
    this.activeModal.dismiss();
  }
}
