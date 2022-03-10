using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aveneo.WebApi.Model.DB
{
    [Index(nameof(Key), IsUnique = true)]
    public partial class ExchangeRate : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [StringLength(100)]
        [Required]
        public String Key { get; set; }
        public String Data { get; set; }

    }
}
