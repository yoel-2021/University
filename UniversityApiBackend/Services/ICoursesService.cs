using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICoursesService
    {
        IEnumerable<Course> GetCousesWithAConcretCategory();
        IEnumerable<Course> GetCoursesNoThemes();
        IEnumerable<Course> GetThemesOfAConcretCourse();
        IEnumerable<Course> GetStudentsOfAConcretCourse();
        IEnumerable<Course> GetCoursesOfAStudent();
    }
}
