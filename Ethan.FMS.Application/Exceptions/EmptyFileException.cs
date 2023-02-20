namespace Ethan.FMS.Application.Exceptions;

public class EmptyFileException:Exception
{
    public EmptyFileException()
        : base("File cannot be empty")
    {
        
    }
}