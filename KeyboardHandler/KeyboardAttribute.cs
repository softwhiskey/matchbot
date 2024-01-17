[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class KeyboardAttribute : Attribute
{
    public string Keyboard { get; set; }

    public KeyboardAttribute(string keyboard)
    {
        Keyboard = keyboard;
    }
}