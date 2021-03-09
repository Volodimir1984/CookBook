import {Component} from "@angular/core";
import {ActivatedRoute} from "@angular/router";
import {HttpService} from "../http.service";
import {DataService} from "../DataService";
import {Recipe} from "../CreateRecipe/Recipe";
import {ParentLevel} from "../ListRecipe/ParentLevel";

@Component({
  selector: 'app-recipe',
  templateUrl: 'recipe.component.html',
  providers: [HttpService],
})

export class RecipeComponent{

  recipe: Recipe;
  isRecipe: boolean;
  isStart: boolean;
  isParent: boolean;
  levels: ParentLevel[];
  isUpdate = false;

  constructor(private route: ActivatedRoute, private http: HttpService, private data: DataService) {
    this.route.params.subscribe(
      params => {
        let id: string = params['id'];
        this.getRecipe(id);
      });
  }

  getRecipe(id: string): void{
    this.http.getData(`/api/recipe/${id}`).subscribe((i:Recipe) => this.setLevel(i));
  }

  setLevel(recipe: Recipe): void{
    this.isRecipe = true;
    this.data.isRecipe = true;
    this.data.level = recipe.level;
    this.recipe = recipe;
    this.isStart = (recipe.level != '0');
    this.levels = this.data.levels;
    this.isParent = this.data.levels.length > 0;
  }

  showUpdate(flag: boolean){
    this.isUpdate = flag;
  }

  update(recipe: Recipe){
    this.http.postData(recipe, "api/recipe/create").subscribe((i: Recipe) => this.recipe = i);

    this.showUpdate(false);
  }

}
