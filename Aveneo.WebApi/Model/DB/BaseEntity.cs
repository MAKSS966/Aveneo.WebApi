using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Model.DB
{
    public partial class BaseEntity
    { 
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
