namespace MyApp.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, int key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}