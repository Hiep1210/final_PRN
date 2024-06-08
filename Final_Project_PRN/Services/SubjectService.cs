using Final_Project_PRN.Models;

namespace Final_Project_PRN.Services
{
    public class SubjectService
    {
        private readonly PRN221ProjectContext _context;
        public SubjectService(PRN221ProjectContext context)
        {
            _context = context;
        }


        public List<Subject> GetSubjects()
        {
            return _context.Subjects.ToList();
        }

        public int GetNumberOfSessionInSubject(string id)
        {
            var subject = _context.Subjects.FirstOrDefault(x => x.SubjectId == id);
            if (subject == null)
            {
                return -1;
            }
            return (int)subject.NumberOfSessions;
        }

        public List<string> GetSubjectIdList()
        {
            return GetSubjects().Select(x => x.SubjectId).ToList();
        }
    }
}
