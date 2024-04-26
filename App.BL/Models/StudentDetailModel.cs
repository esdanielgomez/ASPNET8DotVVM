using DotVVM.AutoUI.Annotations;
using System.ComponentModel.DataAnnotations;

namespace App.BL.Models;

public class StudentDetailModel
{
    [Display(AutoGenerateField = false)]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "First name is required!")]
    public string FirstName { get; set; } = null!;

    [Required(ErrorMessage = "Last name is required!")]
    public string LastName { get; set; } = null!;
    
    [Visible(ViewNames = "Create")]
    public DateTime EnrollmentDate { get; set; }
    
    [DataType(DataType.MultilineText)]
    public string About { get; set; } = null!;
}