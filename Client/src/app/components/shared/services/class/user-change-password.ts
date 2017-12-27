export class UserChangePassword {
        public UserName:string;
        public OldPassword:string;
        public Password:string;
        public RePassword:string;
        get username(){
                return this.UserName;
        }
        set username(u : string){
                this.UserName = u;
        }
        get oldpassword(){
                return this.OldPassword;
        }
        set oldpassword(o : string){
                this.OldPassword = o;
        }
        get password(){
                return this.Password;
        }
        set password(p : string){
                this.Password = p;
        }
        get repassword(){
                return this.RePassword;
        }
        set repassword(r : string){
                this.RePassword = r;
        }
}
