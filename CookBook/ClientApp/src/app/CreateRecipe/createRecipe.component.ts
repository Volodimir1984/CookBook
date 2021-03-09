import {Component} from "@angular/core";
import {Router} from "@angular/router";
import {HttpService} from "../http.service";
import {DataService} from "../DataService";
import {Recipe} from "./Recipe";

@Component({
  selector: 'app-create',
  templateUrl: 'createRecipe.component.html',
  providers: [HttpService]
})

export class CreateRecipeComponent{
  recipe: Recipe = new Recipe();

  constructor(private httpService: HttpService, private dataService: DataService, private router: Router) {}

  submit(recipe: Recipe): void {
    if (this.dataService.isRecipe == undefined || this.dataService.isRecipe == false)
      recipe.level = '0';
    else
      recipe.level = this.dataService.level;

    this.httpService.postData(recipe, "api/recipe/create").subscribe((i: Recipe) => this.showResult(i));
  }

  showResult(recipe: Recipe): void{
    if (recipe.title != null){
      this.dataService.title = recipe.title;
      this.dataService.id = recipe.id
      this.dataService.isCreate = true;
    }
    else{
      this.dataService.isCreate = false;
      this.dataService.title = this.recipe.title;
    }

    this.router.navigate(['resultCreate']);
  }
}
