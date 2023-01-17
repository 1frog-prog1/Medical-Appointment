namespace domain.models
{
    public class Doctor
    {
        public int doctor_id;
        public string fio;
        public int specialisation_id;

        public Doctor(string fio, int specialisation_id) {
            this.fio = fio;
            this.specialisation_id = specialisation_id;
        }
    }
}