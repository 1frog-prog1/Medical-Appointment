using data.models;
using domain.models;

namespace data.converters
{
    public class SheldueConverter
    {
        public static Sheldue toDomain(SheldueModel model) {
            return new Sheldue(
                model.Id,
                model.doctor_id, 
                model.day_start, 
                model.day_end
            );
        }

        public static SheldueModel toModel(Sheldue sheldue) {
            return new SheldueModel {
                Id = sheldue.Id,
                doctor_id = sheldue.doctor_id,
                day_start = sheldue.day_start,
                day_end = sheldue.day_end
            };
        }
    }
}