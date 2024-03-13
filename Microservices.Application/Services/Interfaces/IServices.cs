namespace Microservices.Application.Services.Interfaces;
public interface IServices<R>
{
    Task<R> ExecuteAsync();
}

public interface IServices<R,P>
{
    Task<R> ExecuteAsync(P parameter);
}
