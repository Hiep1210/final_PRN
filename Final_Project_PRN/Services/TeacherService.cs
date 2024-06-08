using Final_Project_PRN.Models;

namespace Final_Project_PRN.Services
{
    public class TeacherServices
    {
        private readonly PRN221ProjectContext _context;
        public TeacherServices(PRN221ProjectContext context)
        {
            _context = context;
        }


        public List<Teacher> GetTeachers()
        {
            return _context.Teachers.ToList();
        }

        public List<string> GetAllTeacherId()
        {
            return GetTeachers().Select(x => x.TeacherId).ToList();
        }
    }
}
