using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using App.BL.Models;
using App.BL.Services;
using DotVVM.Framework.ViewModel;
namespace App.PL.ViewModels;

public class DefaultViewModel : MasterPageViewModel
{
    private readonly StudentService _studentService;

    public DefaultViewModel(StudentService studentService)
    {
        _studentService = studentService;
    }

    [Bind(Direction.ServerToClient)]
    public List<StudentListModel> Students { get; set; }

    public override async Task PreRender()
    {
        Students = await _studentService.GetAllStudentsAsync();
        await base.PreRender();
    }
}