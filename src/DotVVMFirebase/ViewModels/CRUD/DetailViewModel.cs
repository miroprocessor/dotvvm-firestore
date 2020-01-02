using DotVVMFirebase.Models;
using DotVVMFirebase.Services;
using System;
using System.Threading.Tasks;


namespace DotVVMFirebase.ViewModels.CRUD
{
    public class DetailViewModel : MasterPageViewModel
    {
        private readonly StudentService studentService;

        public DetailViewModel(StudentService studentService)
        {
            this.studentService = studentService;
        }

        public StudentDetailModel Student { get; set; }

        public override async Task PreRender()
        {
            int id = Convert.ToInt32(Context.Parameters["Id"]);
            Student = await studentService.GetStudentByIdAsync(id);
            await base.PreRender();
        }
        public async Task DeleteStudent()
        {
            await studentService.DeleteStudentAsync(Student.Id);
            Context.RedirectToRoutePermanent("Default", replaceInHistory: true);
        }
    }
}
