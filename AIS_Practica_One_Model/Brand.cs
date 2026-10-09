using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("brands")]
    public class Brand : IDomainObject<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int ID { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("country")]
        public string Country { get; set; } = string.Empty;

    }
}
