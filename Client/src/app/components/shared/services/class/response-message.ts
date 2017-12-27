export class ResponseMessage {
    public Code :string ;
    public ErrorMessage : string;
    public Data : object;

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
    set data(t : object){
        this.Data = t;
    }
}
