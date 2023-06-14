import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";

@Component({
  selector: 'app-avaliacao-view',
  templateUrl: './avaliacao-view.component.html'
})
export class AvaliacaoViewComponent implements OnInit {
  @Input() _id: string;

  ngOnInit(): void {
  }

  constructor(  private route: ActivatedRoute){
    this._id = this.route.snapshot.params['id'];
  }

}
