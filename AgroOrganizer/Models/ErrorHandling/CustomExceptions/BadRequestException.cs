namespace AgroOrganizer.Models.ErrorHandling.CustomExceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    {
        
    }
}