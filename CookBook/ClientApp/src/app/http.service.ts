import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";

@Injectable()
export class HttpService{
  constructor(private http: HttpClient) {}

  postData(object: object, uri: string){
    return this.http.post(uri, object);
  }

  getData(uri: string){
    return this.http.get(uri);
  }
}
