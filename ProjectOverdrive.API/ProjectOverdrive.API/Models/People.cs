using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectOverdrive.API.Models
{
    [Table("people")]
    public class People
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [Column("document")]
        [Required]
        [StringLength(14)]
        public string Document { get; set; }

        [Column("number_contact")]
        [StringLength(20)]
        public string NumberContact { get; set; }

        [Column("username")]
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column("status")]
        public bool Status { get; set; }
    }
}
