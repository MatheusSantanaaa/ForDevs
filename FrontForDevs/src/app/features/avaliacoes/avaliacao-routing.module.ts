import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { AvaliacaoListComponent } from "./avaliacao-list/avaliacao-list.component";
import { AvaliacaoCreateComponent } from "./avaliacao-create/avaliacao-create.component";
import { AvaliacaoEditComponent } from "./avaliacao-edit/avaliacao-edit.component";
import { AvaliacaoViewComponent } from "./avaliacao-view/avaliacao-view.component";

const routes: Routes = [
  {
    path: '',
    component: AvaliacaoListComponent,
    data: {
      breadcrumb: 'Avaliações',
    },
  },
  { path: 'avaliacoes', component: AvaliacaoListComponent },
  { path: 'avaliacao-create', component: AvaliacaoCreateComponent },
  { path: 'avaliacao-edit/:id', component: AvaliacaoEditComponent },
  { path: 'avaliacao-view/:id', component: AvaliacaoViewComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class AvaliacaoRoutingModule {}
