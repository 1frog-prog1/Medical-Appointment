namespace domain.models
{
    public class Sheldue
    {
        public int Id;
        public int doctor_id;
        public DateTime day_start;
        public DateTime day_end;

        public Sheldue(int Id, int doctor_id, DateTime day_start, DateTime day_end) {
            this.Id = Id;
            this.doctor_id = doctor_id;
            this.day_start = day_start;
            this.day_end = day_end;
        }

    }
}