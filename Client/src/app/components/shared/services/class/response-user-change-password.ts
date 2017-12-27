import { UserChangePassword } from "./index";

export class ResponseUserChangePassword {
    private Code :string ;
    private ErrorMessage : string;
    private Data : UserChangePassword;

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
    set data(t : UserChangePassword){
        this.Data = t;
    }
}
