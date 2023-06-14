import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-clientes-view',
  templateUrl: './cliente-view.component.html',
  styleUrls: ['./cliente-view.component.scss'],
})
export class ClientesViewComponent implements OnInit {
  @Input() _id: string;

  ngOnInit(): void {
  }

  constructor(  private route: ActivatedRoute){
    this._id = this.route.snapshot.params['id'];
  }

}
