namespace AgroOrganizer.Models.ErrorHandling.CustomExceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException(string message) : base(message)
    {
        
    }
}