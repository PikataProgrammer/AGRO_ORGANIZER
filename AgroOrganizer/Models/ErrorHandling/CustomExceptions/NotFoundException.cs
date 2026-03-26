namespace AgroOrganizer.Models.ErrorHandling.CustomExceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
        
    }
}