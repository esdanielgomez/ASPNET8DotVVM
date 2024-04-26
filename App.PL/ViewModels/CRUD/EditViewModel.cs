using System.Threading.Tasks;
using App.BL.Models;
using App.BL.Services;
namespace App.PL.ViewModels.CRUD;

public class EditViewModel : MasterPageViewModel
{
    private readonly StudentService _studentService;

    public StudentDetailModel Student { get; set; }

    public EditViewModel(StudentService studentService)
    {
        _studentService = studentService;
    }

    public override async Task PreRender()
    {
        if (int.TryParse(Context.Parameters?["Id"]!.ToString(), out var id))
        {
            Student = await _studentService.GetStudentByIdAsync(id);
        }
        await base.PreRender();
    }


    public async Task EditStudent()
    {
        await _studentService.UpdateStudentAsync(Student);
        Context.RedirectToRoute("CRUD_Detail", new { id = Student.Id });
    }
}