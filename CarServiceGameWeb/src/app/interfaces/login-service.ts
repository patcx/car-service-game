import { Observable } from "rxjs";

export interface ILoginService {

    login(name, password): Observable<any>;
    createAccount(name, password): Observable<any>;
}