using System.ComponentModel.DataAnnotations;


namespace data.models
{
    public class DoctorModel
    {
        [Key]
        public int Id {get; set;}
        public string fio {get; set;}
        public int specialisation_id {get; set;}

        public DoctorModel(int Id, string fio, int specialisation_id) {
            this.Id = Id;
            this.fio = fio;
            this.specialisation_id = specialisation_id;
        }

    }
}