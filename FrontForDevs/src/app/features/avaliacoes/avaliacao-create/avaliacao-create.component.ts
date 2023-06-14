import { Component, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { Router } from "@angular/router";
import { ToastService } from "../../services/toast.service";
import { AvaliacaoService } from "../../services/avaliacao.service";
import { ClienteService } from "../../services/cliente.service";
import { Item } from "../../models/item.model";
import { Avaliacao } from "../../models/avaliacao";
import { take } from "rxjs";

@Component({
  selector: 'app-avaliacao-create',
  templateUrl: './avaliacao-create.component.html',
  styleUrls: ['./avaliacao-create.component.scss'],
})
export class AvaliacaoCreateComponent implements OnInit {
  public formGroup: FormGroup;
  public campoObrigatorio: string = "Campo Obrigatório";

  public avaliacaoClienteFormGroup: FormGroup;
  public avaliacaoClienteForms: FormGroup[] = [];

  public clientes: Array<Item>;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastService: ToastService,
    private avaliacaoService: AvaliacaoService,
    private clienteService: ClienteService
    ){
      this.clientes = new Array<Item>;
  }

  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      dataDeReferencia: ['', Validators.required],
    });

    this.avaliacaoClienteFormGroup = this.formBuilder.group({
      clienteId: ['', Validators.required],
      nota: ['', Validators.required],
      motivoNota: ['', Validators.required],
    })

    this.preecherDropDown();
  }

  addAvaliacaoCliente() {
    const novoFormGroup = this.formBuilder.group({
      clienteId: ['', Validators.required],
      nota: ['', Validators.required],
      motivoNota: ['', Validators.required],
    });

    this.avaliacaoClienteForms.push(novoFormGroup);
  }

  preecherDropDown(){
    this.clienteService.obterClientes()
      .subscribe((response : any[]) => {
        response.map(cliente => {
          this.clientes.push({ value: cliente.id, label: cliente.nomeContato });
        });
    })
  }

  removeAvaliacaoCliente(): void {
    this.avaliacaoClienteForms.pop();
  }

  salvar() {
    this.formGroup.markAllAsTouched();

    this.avaliacaoClienteForms.map(formGroup => formGroup.markAllAsTouched());

    const isInvalid = this.avaliacaoClienteForms.some(formGroup => formGroup.invalid);

    if (this.formGroup.invalid) return;

    if (isInvalid) {
      this.toastService.showWarningMessage("Formulário de Clientes Inválido", "Avaliacao");
      return;
    };

    if (this.avaliacaoClienteForms.length == 0) {
      this.toastService.showWarningMessage("É obrigatório que uma avaliação, tenha ao menos um Cliente", "Cliente");
      return;
    }

    const formValues = this.formGroup.getRawValue();

    const avaliacoesClientes = this.avaliacaoClienteForms.map(formGroup => formGroup.getRawValue());

    const values = {
      ...formValues,
      avaliacaoClientes: avaliacoesClientes
    };

    this.registrarAvaliacao(values);
  }

  registrarAvaliacao(avaliacao: Avaliacao){
    this.avaliacaoService.registrarAvaliacao(avaliacao)
    .pipe(take(1))
    .subscribe(resp =>{
      if(resp.success){
        this.toastService.showSuccessMessage("Avaliação Cadastrada com sucesso", "Avaliação");

        setTimeout(() => {
          this.router.navigate(['/avaliacoes']);
        }, 1500);
      }
    });
  }

}
