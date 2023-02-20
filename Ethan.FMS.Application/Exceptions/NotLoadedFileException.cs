namespace Ethan.FMS.Application.Exceptions;

public class NotLoadedFileException:Exception
{
    public NotLoadedFileException(long id)
        : base($"File  {id} could not be loaded.")
    {
        
    }
}