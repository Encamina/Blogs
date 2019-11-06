﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreOData.Database
{
    public partial class BusinessEntity
    {
        public BusinessEntity()
        {
            BusinessEntityAddress = new HashSet<BusinessEntityAddress>();
            BusinessEntityContact = new HashSet<BusinessEntityContact>();
        }

        [Key]
        public int BusinessEntityId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Person Person { get; set; }
        public virtual Store Store { get; set; }
        public virtual Vendor Vendor { get; set; }
        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddress { get; set; }
        public virtual ICollection<BusinessEntityContact> BusinessEntityContact { get; set; }
    }
}
