
namespace BO;

[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string message, Exception innerException)
                : base(message, innerException) { }
}


[Serializable]
public class BlInvalidValuesException : Exception
{
    public BlInvalidValuesException(string? message) : base(message) { }
}

[Serializable]
public class BlAlreadyExistsException: Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string message, Exception innerException)
                : base(message, innerException) { }
}


[Serializable]
public class BlNotErasableException : Exception
{
    public BlNotErasableException(string? message) : base(message) { }
}