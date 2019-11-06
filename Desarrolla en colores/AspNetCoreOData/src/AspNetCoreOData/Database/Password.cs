﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreOData.Database
{
    public partial class Password
    {
        [Key]
        public int BusinessEntityId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Person BusinessEntity { get; set; }
    }
}
