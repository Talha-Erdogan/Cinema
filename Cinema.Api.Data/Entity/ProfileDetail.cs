using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Api.Data.Entity
{
    [Table("ProfileDetail")]
    public class ProfileDetail
    {
        public int Id { get; set; }
        public int AuthId { get; set; }
        public int ProfileId { get; set; }
    }
}
