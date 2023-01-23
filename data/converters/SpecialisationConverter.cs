using data.models;
using domain.models.specialisation;

namespace data.converters
{
    public class SpecialisationConverter
    {
        public static Specialisation toDomain(SpecialisationModel model) {
            return new Specialisation(model.Id, model.name);
        }

        public static SpecialisationModel toModel(Specialisation spec) {
            return new SpecialisationModel {
                Id = spec.Id,
                name = spec.name
            };
        }
    }
}