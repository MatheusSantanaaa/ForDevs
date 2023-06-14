import { take } from 'rxjs';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { distinctUntilChanged, Observable, Subject, takeUntil } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  public formGroup: FormGroup;
  public user: any;


  error = '';


  alerts$: Observable<any>;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private authService: AuthService,
    private router: Router,
  ) {}

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      email: [null, [Validators.required, Validators.email]],
      senha: [null, [Validators.required]]
    });
  }



  onSubmit() {
    const formValues = this.formGroup.getRawValue();

    if (this.formGroup.invalid) {
      return;
    }

    this.authService.login(formValues).subscribe(
      (res: any) => {
        localStorage.setItem('currentToken', JSON.stringify(res.model.accessToken))
        this.router.navigate(['']);
        setTimeout(() => {
          this.user = this.authService.getCurrentUser();
        }, 1500);
      },
      e => console.log('Error: ' + e)
    );
  }

  handleCreateUser(): void {
    this.router.navigate(['/auth/register']);
  }


  mostrarSenha() {
    const tipo = document.getElementById('senha');
    if (tipo?.getAttribute('type') == 'password') {
      tipo?.setAttribute('type', 'text');
    } else {
      tipo?.setAttribute('type', 'password');
    }
  }

  get f() {
    return this.formGroup['controls'];
  }
}
