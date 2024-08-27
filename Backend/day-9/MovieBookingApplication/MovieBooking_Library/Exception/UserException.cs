using System;

public class UserException : Exception
{
    public UserException(string message)
        : base(message)
    {
    }
}
public class UserValidationException : Exception
{
    public UserValidationException(string message) : base(message) { }
}