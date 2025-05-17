﻿using Microsoft.AspNetCore.Identity;
using Project.ENTITIES.Enums;
using Project.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser : IdentityUser<int>, IEntity
    {
        public Guid ActivationCode { get; set; } //Her kullanıcım icin sadece ona özgü unique bir kod olusturmak isterim ki onun Email'ine o kodu gönderebileyim...Böylece ben onun Email'ine gidip oradaki linke tıkladıgını o kodu bana geri ulastırdıgında anlarım...

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DataStatus Status { get; set; }

        //Relational Properties
        public virtual AppUserProfile AppUserProfile { get; set; }
        public virtual ICollection<Order> Orders { get; set; }



    }
}
