using System;
using System.IO;

namespace LogParser
{
    public class Validation
    {
       public bool ValidateFile(string filePath)
        {
            if(string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException("file path is null or empty");
            }
            string ext = Path.GetExtension(filePath);
            if(ext != ".csv")
            {
                throw new ArgumentException("file is not in csv format");
            }
            if( new FileInfo(filePath).Length == 0 )
            {
                throw new ArgumentException("file is empty");
            }
            return true;
        }
    }
}
