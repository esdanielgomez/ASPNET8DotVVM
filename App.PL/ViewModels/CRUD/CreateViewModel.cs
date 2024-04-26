using System;
using System.Threading.Tasks;
using App.BL.Models;
using App.BL.Services;

namespace App.PL.ViewModels.CRUD;

public class CreateViewModel : MasterPageViewModel
{
    private readonly StudentService _studentService;

    public StudentDetailModel Student { get; set; } = new() { EnrollmentDate = DateTime.UtcNow.Date };

    public CreateViewModel(StudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task AddStudent()
    {
        await _studentService.InsertStudentAsync(Student);
        Context.RedirectToRoute("Default");
    }
}