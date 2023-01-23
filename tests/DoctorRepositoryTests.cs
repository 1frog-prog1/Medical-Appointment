using Xunit;

using domain.models;
using domain.models.user;
using data;
using data.repository;
using domain.models.user.model;

namespace tests
{
    public class DoctorRepositoryTests
    {
        private readonly ApplicationContextFactory dbFatory;
        private readonly ApplicationContext db;
        private readonly DoctorRepository doc_rep;

        public DoctorRepositoryTests() {
            dbFatory = new ApplicationContextFactory();
            db = dbFatory.CreateDbContext();
            doc_rep = new DoctorRepository(db);
        }

        [Fact]
        public void DoctorRepositoryIsExist() {
            Doctor doc = new Doctor("Dominic Schultz", 2);
            doc_rep.create(doc);

            var doc_list = doc_rep.findDoctorListBySpecialisation(2);

            Assert.True(doc_rep.isExist(doc_list[0].Id));

            doc_rep.delete(doc_list[0].Id);
            // в бд ничего не осталось
        }


        [Fact]
        public void DoctorRepositoryFindBySpec() {
            Doctor doc = new Doctor("Dominic Schultz", 2);

            doc_rep.create(doc);

            var doc_list = doc_rep.findDoctorListBySpecialisation(2);

            Assert.True(doc_list[0].fio == doc.fio && 
                        doc_list[0].specialisation_id == doc.specialisation_id);

            doc_rep.delete(doc_list[0].Id);
            // в бд ничего не осталось
        }

        [Fact]
        public void DoctorRepositoryGetAll() {

            Doctor doc1 = new Doctor("Dominic Schultz", 2);
            Doctor doc2 = new Doctor("Bobbie Parisian", 1);
            doc_rep.create(doc1);
            doc_rep.create(doc2);
            
            var doc_list = doc_rep.getAll();

            Assert.True(doc_list[0].fio == doc1.fio && 
                        doc_list[0].specialisation_id == doc1.specialisation_id);
            Assert.True(doc_list[1].fio == doc2.fio && 
                        doc_list[1].specialisation_id == doc2.specialisation_id);

            doc_rep.delete(doc_list[0].Id);
            doc_rep.delete(doc_list[1].Id);
            // в бд ничего не осталось
        }

        [Fact]
        public void DoctorRepositoryFindDoctorById() {

            Doctor doc = new Doctor("Dominic Schultz", 2);
            doc_rep.create(doc);
            var doc_id = doc_rep.getAll()[0].Id;

            Doctor copy_doc = doc_rep.findDoctorByID(doc_id);

            Assert.True(doc.fio == copy_doc.fio && 
                        doc.specialisation_id == copy_doc.specialisation_id);


            doc_rep.delete(doc_id);
            // в бд ничего не осталось
        }

    }
}