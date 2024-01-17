[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class CommandAttribute : Attribute
{
    public string Name { get; }

    public CommandAttribute(string command)
    {
        Name = command;
    }
}