using App.BL.Models;
using App.DAL;
using App.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.BL.Services;

public class StudentService
{
    private readonly StudentDbContext _studentDbContext;

    public StudentService(StudentDbContext studentDbContext)
    {
        _studentDbContext = studentDbContext;
    }

    public async Task<List<StudentListModel>> GetAllStudentsAsync()
    {

        return await _studentDbContext.Students.Select(
            s => new StudentListModel
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName
            }
        ).ToListAsync();
    }


    public async Task<StudentDetailModel> GetStudentByIdAsync(int studentId)
    {
        return await _studentDbContext.Students.Select(
                s => new StudentDetailModel
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    About = s.About,
                    EnrollmentDate = s.EnrollmentDate
                })
            .FirstOrDefaultAsync(s => s.Id == studentId) ?? new ();
    }

    public async Task UpdateStudentAsync(StudentDetailModel student)
    {
        var entity = await _studentDbContext.Students.FirstOrDefaultAsync(s => s.Id == student.Id);

        entity.FirstName = student.FirstName;
        entity.LastName = student.LastName;
        entity.About = student.About;
        entity.EnrollmentDate = student.EnrollmentDate;

        await _studentDbContext.SaveChangesAsync();
    }

    public async Task InsertStudentAsync(StudentDetailModel student)
    {
        var entity = new Student()
        {
            FirstName = student.FirstName,
            LastName = student.LastName,
            About = student.About,
            EnrollmentDate = student.EnrollmentDate
        };

        _studentDbContext.Students.Add(entity);
        await _studentDbContext.SaveChangesAsync();
    }

    public async Task DeleteStudentAsync(int studentId)
    {
        var entity = new Student()
        {
            Id = studentId
        };
        _studentDbContext.Students.Attach(entity);
        _studentDbContext.Students.Remove(entity);
        await _studentDbContext.SaveChangesAsync();
    }


}