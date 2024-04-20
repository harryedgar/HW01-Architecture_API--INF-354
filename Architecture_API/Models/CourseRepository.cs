using Microsoft.EntityFrameworkCore;
using System;

namespace Architecture_API.Models
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _appDbContext;

        public CourseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Course[]> GetAllCourseAsync()
        {
            IQueryable<Course> query = _appDbContext.Courses;
            return await query.ToArrayAsync();
        }

        public async Task<Course> AddCourseAsync(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            _appDbContext.Courses.Add(course);
            await _appDbContext.SaveChangesAsync();
            return course;
        }

        public async Task<Course> EditCourseAsync(Course course)
        {
            _appDbContext.Courses.Update(course);
            await _appDbContext.SaveChangesAsync();
            return course;  
        }

        public async Task<Course> DeleteCourseAsync(Course course)
        {
            _appDbContext.Courses.Remove(course);
            await _appDbContext.SaveChangesAsync();
            return course;
        }

        public async Task<Course> GetCourseIdAsync(int id)
        {
            return await _appDbContext.Courses.FindAsync(id);
        }
    }
}
