namespace Ethan.FMS.Application.Exceptions;

public class FileNotFoundException:Exception
{
    public FileNotFoundException()
        : base("File not found.")
    {
        
    }
}