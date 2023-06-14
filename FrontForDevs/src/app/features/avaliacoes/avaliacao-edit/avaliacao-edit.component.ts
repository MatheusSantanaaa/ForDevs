import { Component, Input, OnInit } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastService } from "../../services/toast.service";
import { AvaliacaoService } from "../../services/avaliacao.service";
import { ClienteService } from "../../services/cliente.service";
import { Item } from "../../models/item.model";
import { Avaliacao } from "../../models/avaliacao";
import { take } from "rxjs";
import { Cliente } from "../../models/cliente";

@Component({
  selector: 'app-avaliacao-edit',
  templateUrl: './avaliacao-edit.component.html',
  styleUrls: ['./avaliacao-edit.component.scss'],
})
export class AvaliacaoEditComponent implements OnInit {
  @Input() avaliacaoId: string;
  private _id: string;
  public formGroup: FormGroup;
  public campoObrigatorio: string = "Campo Obrigatório";

  public avaliacaoClienteFormGroup: FormGroup;
  public avaliacaoClienteForms: FormGroup[] = [];

  public clientes: Array<Item>;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private toastService: ToastService,
    private avaliacaoService: AvaliacaoService,
    private clienteService: ClienteService
    ){
      this._id = this.route.snapshot.params['id'];
      this.clientes = new Array<Item>;
  }

  async ngOnInit(): Promise<void> {
    this.formGroup = this.formBuilder.group({
      dataDeReferencia: ['', Validators.required],
      id: ['']
    });

    this.avaliacaoClienteFormGroup = this.formBuilder.group({
      clienteId: ['', Validators.required],
      nota: ['', Validators.required],
      motivoNota: ['', Validators.required],
    })

    let id = this.avaliacaoId ? this.avaliacaoId : this._id;
    this.getAvaliacoes(id);
    this.preecherDropDown();
  }

  getAvaliacoes(id: string) : Promise<any>{
    return new Promise((resolve, reject) => {
      this.avaliacaoService.obterPorId(id)
      .pipe(take(1))
      .subscribe(response => {
        console.log(response)
        this.formGroup.patchValue(response);
        this.formGroup.patchValue({
          id: id,
        });
        this.createClientesFormGroup(response);
        resolve(response);
      }, error => {
        reject(error);
      });
    });
  }

  createClientesFormGroup(avaliacao: Avaliacao): void {
    for (const cliente of avaliacao.avaliacaoClientes) {
      const parcelaFormGroup: FormGroup = this.formBuilder.group({
        clienteId: [cliente.clienteId, Validators.required],
        nota: [cliente.nota, Validators.required],
        motivoNota: [cliente.motivoNota, Validators.required],
      });
      this.avaliacaoClienteForms.push(parcelaFormGroup);
    }
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

    this.atualizarAvaliacao(values);
  }

  atualizarAvaliacao(avaliacao: Avaliacao){
    this.avaliacaoService.atualizarAvaliacao(avaliacao)
    .pipe(take(1))
    .subscribe(resp =>{
      if(resp.success){
        this.toastService.showSuccessMessage("Avaliação Atualizada com sucesso", "Avaliação");

        setTimeout(() => {
          this.router.navigate(['/avaliacoes']);
        }, 1500);
      }
    });
  }

  voltar(): void {
    window.history.back();
  }

  get viewButton(): boolean{
    if(this.avaliacaoId){
      return false;
    }
    else{
      return true;
    }
   }
}
