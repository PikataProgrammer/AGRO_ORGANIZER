namespace AgroOrganizer.Models.ErrorHandling.CustomExceptions;

public class Unauthorized : Exception
{
    public Unauthorized(string message) : base(message)
    {
        
    }
}