import {Component, OnInit}  from "@angular/core";
import {DataService} from "../DataService";

@Component({
  selector: 'app-resultCreate',
  templateUrl: 'createResult.component.html',
})
export class ResultCreateComponent implements OnInit{

  isCreate: boolean;
  title: string;
  id: string;

  constructor(private service: DataService) {}

  ngOnInit() {
    this.title = this.service.title;
    this.id = this.service.id;
    this.isCreate = this.service.isCreate;
  }
}
