using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Data.Entity
{
    [Table("Seance")]
    public  class Seance
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }
        public DateTime? Date { get; set; }
        public string Time { get; set; }
        public bool IsDeleted { get; set; }
    }
}
