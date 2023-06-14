import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { ClienteListComponent } from "./cliente-list/cliente-list.component";
import { ClienteCreateComponent } from "./cliente-create/cliente-create.component";

const routes: Routes = [
  {
    path: '',
    component: ClienteListComponent,
    data: {
      breadcrumb: 'Clientes',
    },
  },
  { path: 'features/cliente/cliente-create', component: ClienteCreateComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ClienteRoutingModule {}
