using Xunit;
using System;

using data.repository;
using data.converters;
using data.models;
using data;
using domain.models.appointment;

namespace tests
{
    public class AppointmentRepositoryTests
    {
        private readonly ApplicationContextFactory dbFatory;
        private readonly ApplicationContext db;
        private readonly AppointmentRepository rep;

        public AppointmentRepositoryTests() {
            this.dbFatory = new ApplicationContextFactory();
            this.db = dbFatory.CreateDbContext();
            rep = new AppointmentRepository(db);
        }

        [Fact]
        public void AppRepositoryCreateDelete() {
            Appointment app = new Appointment(
                new DateTime(1, 1, 1, 15, 20, 0), 
                1, 1);

            rep.create(app);

            app = rep.getByInfo(app);
            Assert.True(rep.isExist(app.Id));
            
            rep.delete(app.Id);
            Assert.False(rep.isExist(app.Id));
        }

        [Fact]
        public void isDoctorsFreeAtTime() {

            DateTime start = new DateTime(1, 1, 1, 15, 20, 0);
            Appointment app = new Appointment(start, 1, 1);
            rep.create(app);

            Assert.False(rep.isDoctorFreeAtTime(start, 1));

            app = rep.getByInfo(app);
            rep.delete(app.Id);
            
        }

    }
}