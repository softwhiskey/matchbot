[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CallbackAttribute : Attribute
{
    public string Name { get; }

    public CallbackAttribute(string name)
    {
        Name = name;
    }
}