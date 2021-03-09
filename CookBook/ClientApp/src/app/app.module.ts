import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import  {HomeComponent} from "./Home/home.component";
import {MenuComponent} from "./Menu/menu.component";
import {CreateRecipeComponent} from "./CreateRecipe/createRecipe.component";
import {ListRecipeComponent} from "./ListRecipe/list-recipe.component";
import {ResultCreateComponent} from "./ResultCreate/resultCreate.component";
import {RecipeComponent} from "./Recipe/recipe.component";
import {CartRecipeComponent} from "./CartRecipe/cartRecipe.component";
import {BreadCrumbsComponent} from "./BreadCrumbs/breadCrumbs.component";

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    CreateRecipeComponent,
    ListRecipeComponent,
    ResultCreateComponent,
    RecipeComponent,
    CartRecipeComponent,
    BreadCrumbsComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent},
      {path: 'create', component: CreateRecipeComponent},
      {path: 'api/recipe/:id', component: RecipeComponent},
      {path: 'api/recipe/list/:level', component: ListRecipeComponent},
      {path: 'resultCreate', component: ResultCreateComponent},
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
