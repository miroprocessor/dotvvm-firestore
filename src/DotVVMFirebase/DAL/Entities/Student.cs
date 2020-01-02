using Google.Cloud.Firestore;

namespace DotVVMFirebase.DAL.Entities
{
    [FirestoreData]
    public class Student
    {
        [FirestoreProperty]
        public int Id { get; set; }

        [FirestoreProperty]
        public string FirstName { get; set; }

        [FirestoreProperty]
        public string LastName { get; set; }

        [FirestoreProperty]
        public string About { get; set; }

        [FirestoreProperty]
        public Timestamp EnrollmentDate { get; set; }
    }
}
