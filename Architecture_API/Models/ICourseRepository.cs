namespace Architecture_API.Models
{
    public interface ICourseRepository
    {
        // Course
        Task<Course[]> GetAllCourseAsync();
        Task<Course> AddCourseAsync(Course course);
        Task<Course> EditCourseAsync(Course course);
        Task<Course> DeleteCourseAsync(Course course);
        Task<Course> GetCourseIdAsync(int id);

    }
}
