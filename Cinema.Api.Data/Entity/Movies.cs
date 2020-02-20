using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Data.Entity
{
   [Table("Movies")]
    public class Movies
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        public int SeanceId { get; set; }

        [Required]
        public int SalonId { get; set; }

        [StringLength(150)]
        public string Name { get; set; }
        [Required]
        public int TypeId { get; set; }

        [StringLength(150)]
        public string Director { get; set; }

        [StringLength(250)]
        public string Banner { get; set; }
        public bool IsDeleted { get; set; }

    }
}
