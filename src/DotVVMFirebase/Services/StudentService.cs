using DotVVMFirebase.DAL.Entities;
using DotVVMFirebase.Models;
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotVVMFirebase.Services
{
    public class StudentService
    {
        private const string Project = "dotvvm";
        private const string Collection = "students";

        public async Task<List<StudentListModel>> GetAllStudentsAsync()
        {
            var studentsList = new List<Student>();
            FirestoreDb db = FirestoreDb.Create(Project);
            Query allStudentsQuery = db.Collection(Collection);

            QuerySnapshot allStudentsQuerySnapshot = await allStudentsQuery.GetSnapshotAsync();
            foreach (DocumentSnapshot documentSnapshot in allStudentsQuerySnapshot.Documents)
            {
                studentsList.Add(documentSnapshot.ConvertTo<Student>());
            }

            return studentsList.Select(
                s => new StudentListModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName
                }
            ).ToList();
        }

        public async Task<StudentDetailModel> GetStudentByIdAsync(int studentId)
        {
            FirestoreDb db = FirestoreDb.Create(Project);
            Query docRef = db.Collection(Collection).WhereEqualTo("Id", studentId).Limit(1);
            QuerySnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Count > 0)
            {
                Student student = snapshot.ElementAt(0).ConvertTo<Student>();
                return new StudentDetailModel()
                {
                    About = student.About,
                    EnrollmentDate = student.EnrollmentDate.ToDateTime(),
                    FirstName = student.FirstName,
                    Id = student.Id,
                    LastName = student.LastName
                };
            }
            else
            {
                return null;
            }
        }

        public async Task UpdateStudentAsync(StudentDetailModel student)
        {
            FirestoreDb db = FirestoreDb.Create(Project);
            Query docRef = db.Collection(Collection).WhereEqualTo("Id", student.Id).Limit(1);
            QuerySnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Count > 0)
            {
                DocumentReference studentRef = db.Collection(Collection).Document(snapshot.ElementAt(0).Id);
                Dictionary<string, object> updates = new Dictionary<string, object>
                {
                    { nameof(student.About), student.About},
                    { nameof(student.EnrollmentDate), Timestamp.FromDateTime(student.EnrollmentDate.ToUniversalTime())},
                    { nameof(student.FirstName), student.FirstName},
                    { nameof(student.LastName), student.LastName}
                };
                await studentRef.UpdateAsync(updates);
            }
        }
       
        public async Task InsertStudentAsync(StudentDetailModel student)
        {
            var entity = new Student()
            {
                Id = new Random().Next(1, int.MaxValue),
                FirstName = student.FirstName,
                LastName = student.LastName,
                About = student.About,
                EnrollmentDate = Timestamp.FromDateTime(student.EnrollmentDate.ToUniversalTime())
            };

            FirestoreDb db = FirestoreDb.Create(Project);
            var Id = Guid.NewGuid().ToString();
            DocumentReference docRef = db.Collection(Collection).Document(Id);
            await docRef.SetAsync(entity);
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            FirestoreDb db = FirestoreDb.Create(Project);
            Query docRef = db.Collection(Collection).WhereEqualTo("Id", studentId).Limit(1);
            QuerySnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Count > 0)
            {
                DocumentReference studentRef = db.Collection(Collection).Document(snapshot.ElementAt(0).Id);
                await studentRef.DeleteAsync();
            }
        }
    }
}
