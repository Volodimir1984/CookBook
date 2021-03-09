import {Component, Input} from "@angular/core";
import {Recipe} from "../CreateRecipe/Recipe";

@Component({
  selector: 'app-cartRecipe',
  templateUrl: 'cartRecipe.component.html',
  styleUrls: ['cartRecipe.component.css'],
})

export class CartRecipeComponent{
  @Input() recipe: Recipe;

  visibleDescription: boolean = true;

  isVisible(): void{
    this.visibleDescription = !this.visibleDescription;
  }
}
