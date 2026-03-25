namespace AgroOrganizer.Models.ErrorHandling.CustomExceptions;

public class BadRequest : Exception
{
    public BadRequest(string message) : base(message)
    {
        
    }
}