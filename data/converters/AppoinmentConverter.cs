using domain.models.appointment;
using data.models;

namespace data.converters
{
    public class AppoinmentConverter
    {
        public static Appointment toDomain(AppointmentModel model) {
            return new Appointment(
                model.Id,
                model.start,
                model.patient_id, 
                model.doctor_id
            );
        }

        public static AppointmentModel toModel(Appointment app) {
            return new AppointmentModel {
                Id = app.Id,
                start = app.start,
                end = app.end,
                patient_id = app.patient_id,
                doctor_id = app.doctor_id
            };
        }
    }
}