import { CommonModule } from "@angular/common";
import { ClienteCreateComponent } from "./cliente-create/cliente-create.component";
import { HttpClientModule } from "@angular/common/http";
import { ClienteListComponent } from "./cliente-list/cliente-list.component";
import { ClienteRoutingModule } from "./cliente-routing.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { NgModule } from "@angular/core";
import { ClienteService } from "../services/cliente.service";
import { ClientesViewComponent } from "./cliente-view/cliente-view.component";

@NgModule({
  declarations: [
    ClienteListComponent,
    ClienteCreateComponent,
    ClientesViewComponent
  ],
  imports:
  [
    CommonModule,
    ClienteRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [
    ClienteService,
  ]
})
export class ClienteModule {}
