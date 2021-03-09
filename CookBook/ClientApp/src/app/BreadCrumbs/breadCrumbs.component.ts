import {Component, Input} from "@angular/core";
import {ParentLevel} from "../ListRecipe/ParentLevel";

@Component({
  selector: 'app-breadcrumb',
  templateUrl: 'breadCrumbs.component.html',
})

export  class BreadCrumbsComponent{
  @Input() levels: ParentLevel[];
  @Input() isStart: boolean;

}
