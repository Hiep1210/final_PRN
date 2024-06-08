using Final_Project_PRN.Models;

namespace Final_Project_PRN.Pages.Common
{
    public class DisplayCell
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string Subject {  get; set; }
        public string Teacher { get; set; }

        public DisplayCell() { }
        public DisplayCell(int id,string @class, string subject, string teacher)
        {
            Class = @class;
            Subject = subject;
            Teacher = teacher;
            Id = id;
        }
    }
}
