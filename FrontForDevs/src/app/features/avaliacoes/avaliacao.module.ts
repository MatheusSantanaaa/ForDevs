import { NgModule } from "@angular/core";
import { AvaliacaoListComponent } from "./avaliacao-list/avaliacao-list.component";
import { AvaliacaoCreateComponent } from "./avaliacao-create/avaliacao-create.component";
import { CommonModule } from "@angular/common";
import { AvaliacaoRoutingModule } from "./avaliacao-routing.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpClientModule } from "@angular/common/http";
import { AvaliacaoEditComponent } from "./avaliacao-edit/avaliacao-edit.component";
import { AvaliacaoViewComponent } from "./avaliacao-view/avaliacao-view.component";

@NgModule({
  declarations: [
    AvaliacaoListComponent,
    AvaliacaoCreateComponent,
    AvaliacaoEditComponent,
    AvaliacaoViewComponent
  ],
  imports:
  [
    CommonModule,
    AvaliacaoRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [
  ]
})
export class AvaliacaoModule {}
