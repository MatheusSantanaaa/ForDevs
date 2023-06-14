import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { FeatureComponent } from "./feature.component";

const routes: Routes = [
  {
    path: '',
    component: FeatureComponent,
    children: [
      {
        path: 'clientes',
        loadChildren: () => import('./cliente/cliente.module').then(m => m.ClienteModule),
      },
      {
        path: 'avaliacoes',
        loadChildren: () => import('./avaliacoes/avaliacao.module').then(m => m.AvaliacaoModule),
      }
    ]
  },
]

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class FeatureRoutingModule {}
