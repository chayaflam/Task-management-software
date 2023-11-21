

namespace DO;
/// <summary>
/// Id not exist exception
/// </summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}
/// <summary>
/// Id already exists exception
/// </summary>
[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}

/// <summary>
/// Invalid selection exception
/// </summary>
[Serializable]
public class DalInvalidSelectionException : Exception
{
    public DalInvalidSelectionException(string? message) : base(message) { }
}


/// <summary>
/// XML file load create exception
/// </summary>
[Serializable]
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}




