using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Data.Entity
{
    [Table("UserToken")]
    public class UserToken
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }

        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(150)]
        public string Token { get; set; }
       
        public DateTime ValidBeginDate { get; set; }
        public DateTime ValidEndDate { get; set; }
        public bool IsValid { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public int ProfileId { get; set; }
    }
}
