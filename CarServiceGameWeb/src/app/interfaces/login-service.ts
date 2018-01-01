export interface ILoginService {
    
    login(name, password): void;
    createAccount(name, password): void;
}