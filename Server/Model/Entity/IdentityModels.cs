using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace QuanLyNongTrai.Model.Entity {
    public class ApplicationUser : IdentityUser<Guid>{
        //foreign key id
        public Guid PersonalId {get;set;}
        //relationship
        public Personal Personal {get;set;}
    }

    public class ApplicationRole : IdentityRole<Guid> {
        
    }

}