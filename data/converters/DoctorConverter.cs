using domain.models;
using data.models;

namespace data.converters
{
    public class DoctorConverter
    {
        public static Doctor? toDomain(DoctorModel model) {
            return new Doctor (
                model.Id,
                model.fio,
                model.specialisation_id
            );
        }

    public static DoctorModel toModel(Doctor doctor) {
        return new DoctorModel (
            doctor.Id,
            doctor.fio,
            doctor.specialisation_id
        );
    }
    }
}