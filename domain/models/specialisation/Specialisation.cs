namespace domain.models.specialisation
{
    public class Specialisation
    {
        public int Id;
        public string name;

        public Specialisation(int Id, string name) {
            this.Id = Id;
            this.name = name;
        }

        public Specialisation(string name) {
            this.name = name;
        }

    }
}