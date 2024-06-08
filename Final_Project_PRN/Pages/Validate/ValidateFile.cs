using Microsoft.DotNet.Scaffolding.Shared;
using static Final_Project_PRN.Pages.Common.EnumPrototype;
namespace Final_Project_PRN.Pages.Validate
{
    public class ValidateFile
    {
        private static IFormFile filea;
        public static string validate(IFormFile file)
        {
            filea = file;
            if (ValidateFileLength())
            {
                return "file length is sus";
            }
            if(ValidateFileType())
            {
                return "file type is sus";
            }
            return "Import OK";
        }
        private static bool ValidateFileLength()
        {
            if(filea == null || filea.Length == 0)
            {
                return true;
            }
            return false;
        }
        private static bool ValidateFileType()
        {
            string fileex = Path.GetExtension(filea.FileName);
            if(fileex == null)
            {
                return true;   
            }
            if(fileex != ".json" && fileex != ".xml")
            {
                return true;
            }
            return false;
        }
    }
    
}
