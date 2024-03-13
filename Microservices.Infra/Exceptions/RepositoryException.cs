namespace Microservices.Infra.Exceptions;
public class RepositoryException : Exception
{
    private string Repository { get; set; }
    public RepositoryException(string message, string repository) : base(message)
    {
        Repository = repository;
    }
    
}
