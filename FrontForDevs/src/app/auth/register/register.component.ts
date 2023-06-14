import { Component, OnInit } from '@angular/core';
import { FormGroup, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';

import { ActivatedRoute, Router } from '@angular/router';
import { take } from 'rxjs';
import { Location } from '@angular/common';
import { ToastService } from 'src/app/features/services/toast.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  public formGroup: FormGroup;

  public campoObrigatorio: string = "Campo Obrigatório";

  public submitted = false;

  constructor(
    private formBuilder: UntypedFormBuilder,
    private route: ActivatedRoute,
    private toastService: ToastService,
    private authService: AuthService,
    private router: Router,
    private location: Location,
  ) {}

  ngOnInit() {

    this.formGroup = this.formBuilder.group({
      email: [null, [Validators.required, Validators.email]],
      senha: [null, Validators.required],
      confirmarSenha: [null, Validators.required],
    });
  }

  onSubmit() {
    this.submitted = true;
    const formValues = this.formGroup.value;

    if (this.formGroup.invalid) {
      return;
    }

    if(formValues.senha != formValues.confirmarSenha){
      this.toastService.showErrorMessage('As senhas devem ser iguais!', 'Login');
      return;
    }

    this.saveUser();
  }

  backToLogin(): void {
    this.router.navigate(['/auth/login']);
  }

  handleGoBack(): void {
    this.location.back();
  }

  saveUser(): void {
    const user = this.formGroup.value;

    this.authService.register(user)
    .pipe(take(1))
    .subscribe(resp =>{
      if(resp.success){
        this.toastService.showSuccessMessage("Usúario Cadastrado com Sucesso", "Usuário");
        this.router.navigate(['/login']);
      }
    });
  }
}
