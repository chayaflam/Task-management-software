

namespace BO;

/// <summary>
/// Id already exists exception
/// </summary>
[Serializable] 
    public class BlAlreadyExistsException : Exception
    {
        public BlAlreadyExistsException(string? message) : base(message) { }
    }

