[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class StateAttribute : Attribute
{
    public string Key { get; set; }

    public StateAttribute(string key)
    {
        Key = key;
    }
}
