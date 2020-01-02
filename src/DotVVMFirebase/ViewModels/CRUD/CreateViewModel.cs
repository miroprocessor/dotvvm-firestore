using DotVVMFirebase.Models;
using DotVVMFirebase.Services;
using System;
using System.Threading.Tasks;

namespace DotVVMFirebase.ViewModels.CRUD
{
    public class CreateViewModel : MasterPageViewModel
    {
        private readonly StudentService studentService;

        public StudentDetailModel Student { get; set; } = new StudentDetailModel { EnrollmentDate = DateTime.UtcNow.Date };

        public CreateViewModel(StudentService studentService)
        {
            this.studentService = studentService;
        }


        public async Task AddStudent()
        {
            await studentService.InsertStudentAsync(Student);
            Context.RedirectToRoute("Default");
        }
    }
}
