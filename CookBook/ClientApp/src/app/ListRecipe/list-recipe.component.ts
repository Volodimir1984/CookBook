import {Component} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {HttpService} from "../http.service";
import {DataService} from "../DataService";
import {Info} from "./Info";


@Component({
  selector: 'app-listRecipe',
  templateUrl: 'list-recipe.component.html',
  styleUrls: ['list-recipe.component.css'],
  providers: [HttpService],
})

export class ListRecipeComponent{

  info: Info;
  level: string;
  isStart: boolean
  isSort: boolean;

  constructor(private route: ActivatedRoute, private http: HttpService, private data: DataService) {
    this.route.params.subscribe(
      params => {
        this.level = params['level'];
        this.data.isRecipe = true;
        this.getRecipes(this.level);
      });
  }

  setData(info: Info){
    this.info = info;
    this.data.levels = info.parents;
    this.isStart = info.parents.length > 0;
  }

  getRecipes(level: string): void{
      this.http.getData(`/api/recipe/list/${level}`).subscribe((i:Info) => this.setData(i));
  }

  sort(): void{
    this.isSort = this.isSort != true;

    this.http.getData(`/api/recipe/list/${this.level}?isSort=${this.isSort}`).subscribe((i:Info) => this.setData(i));
  }
}
