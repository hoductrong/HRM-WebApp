import { UserLogin } from "./index";

export class ResponseUserLogin {
    private Code :string ;
    private ErrorMessage : string;
    private Data : UserLogin;

    get code(){
        return this.Code;
    }
    set code(c : string){
        this.Code = c;
    }
    get errormessage(){
        return this.ErrorMessage;
    }
    set errormessage(c : string){
        this.ErrorMessage = c;
    }
    get data(){
        return this.Data;
    }
    set data(t : UserLogin){
        this.Data = t;
    }
}
