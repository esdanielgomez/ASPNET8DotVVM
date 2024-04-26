using System;
using System.Threading.Tasks;
using App.BL.Models;
using App.BL.Services;
namespace App.PL.ViewModels.CRUD;

public class DetailViewModel : MasterPageViewModel
{
    private readonly StudentService _studentService;

    public DetailViewModel(StudentService studentService)
    {
        _studentService = studentService;
    }

    public StudentDetailModel Student { get; set; }

    public override async Task PreRender()
    {
        var id = Convert.ToInt32(Context.Parameters["Id"]);
        Student = await _studentService.GetStudentByIdAsync(id);
        await base.PreRender();
    }
    public async Task DeleteStudent()
    {
        await _studentService.DeleteStudentAsync(Student.Id);
        Context.RedirectToRoutePermanent("Default", replaceInHistory: true);
    }
}