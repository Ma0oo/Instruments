namespace Plugins.Customizable.Attributs
{
    public class TEnum : TField
    {
        public string Name { get; }

        public TEnum(string name) => Name = name;
    }
}