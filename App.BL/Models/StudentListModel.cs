using System.ComponentModel.DataAnnotations;
using DotVVM.AutoUI.Annotations;

namespace App.BL.Models;

public class StudentListModel
{
    [Display(Name = "User ID")]
    public int Id { get; set; }

    [Display(Name = "Name", AutoGenerateField = true)]
    public string FirstName { get; set; } = null!;
    [Display(Name = "Last Name", AutoGenerateField = false)]
    public string LastName { get; set; } = null!;
}