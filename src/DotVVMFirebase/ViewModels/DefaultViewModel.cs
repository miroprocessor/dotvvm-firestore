using DotVVM.Framework.ViewModel;
using DotVVMFirebase.Models;
using DotVVMFirebase.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotVVMFirebase.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {
        private readonly StudentService studentService;

        public DefaultViewModel(StudentService studentService)
        {
            this.studentService = studentService;
        }

        [Bind(Direction.ServerToClient)]
        public List<StudentListModel> Students { get; set; }

        public override async Task PreRender()
        {
            Students = await studentService.GetAllStudentsAsync();
            await base.PreRender();
        }
    }
}
