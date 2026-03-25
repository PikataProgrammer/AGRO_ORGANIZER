namespace AgroOrganizer.Models.ErrorHandling.CustomExceptions;

public class NotFound : Exception
{
    public NotFound(string message) : base(message)
    {
        
    }
}