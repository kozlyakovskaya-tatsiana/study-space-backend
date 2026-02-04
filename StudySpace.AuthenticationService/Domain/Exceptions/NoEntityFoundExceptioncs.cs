namespace Domain.Exceptions;

public class NoEntityFoundException(string message) : DomainException(message);