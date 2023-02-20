namespace Ethan.FMS.Application.Exceptions;

public class SaveFileException:Exception
{
  
    public SaveFileException(string message)
        : base("Count not save file with error " + message)
    {
        
    }
}