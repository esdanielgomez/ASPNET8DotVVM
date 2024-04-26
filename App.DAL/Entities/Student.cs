namespace App.DAL.Entities;

public partial class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string About { get; set; } = null!;
    public DateTime EnrollmentDate { get; set; }
}