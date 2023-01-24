using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace data.models
{
    public class SheldueModel
    {
        [Key]
        public int Id {get; set;}
        [ForeignKey("Doctors")]
        public int doctor_id {get; set;}
        public DateTime day_start {get; set;}
        public DateTime day_end {get; set;}
    }
}