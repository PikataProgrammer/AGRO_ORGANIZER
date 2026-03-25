namespace AgroOrganizer.Models.ErrorHandling.CustomExceptions;

public class Conflict : Exception
{
    public Conflict(string message) : base(message)
    {
        
    }
}