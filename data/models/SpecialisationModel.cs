using System.ComponentModel.DataAnnotations;

namespace data.models
{
    public class SpecialisationModel
    {
        [Key]
        public int Id {get; set;}
        public string name {get; set;}
    }
}