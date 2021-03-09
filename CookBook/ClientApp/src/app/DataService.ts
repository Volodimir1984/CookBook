import {Injectable} from "@angular/core";
import {ParentLevel} from "./ListRecipe/ParentLevel";

@Injectable({providedIn: "root"})
export class DataService {
  title: string;
  id: string;
  level: string;
  isCreate: boolean;
  isRecipe: boolean;
  levels: ParentLevel[];
}
