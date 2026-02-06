namespace Domain.Exceptions;

public class NoResourceFoundException(string message) : DomainException(message);