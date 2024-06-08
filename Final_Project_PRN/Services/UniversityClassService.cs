using Final_Project_PRN.Models;

namespace Final_Project_PRN.Services
{
    public class UniversityClassService
    {
        private readonly PRN221ProjectContext _context;

        public UniversityClassService(PRN221ProjectContext context)
        {
            _context = context;
        }

        public void AddClass(UniversityClass universityClass)
        {
            _context.UniversityClasses.Add(universityClass);
            _context.SaveChanges();
        }

        public List<UniversityClass> GetAllUniversityClasses()
        {
            return _context.UniversityClasses.ToList();
        }

        public List<string> GetClassIdLists()
        {
           return GetAllUniversityClasses().Select(x => x.ClassId).ToList();

        }
    }
}
