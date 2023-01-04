namespace Staff.Application.Exceptions;

internal class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
