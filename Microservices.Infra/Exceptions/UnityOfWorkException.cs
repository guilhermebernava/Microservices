namespace Microservices.Infra.Exceptions;
public class UnityOfWorkException : Exception
{
    public UnityOfWorkException(string message) : base(message)
    {

    }
}
