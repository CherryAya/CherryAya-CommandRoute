using CherryAya_CommandRoute.Entities;

namespace CherryAya_CommandRoute_Test.Commands
{
    public class OptionsCommand : ICommand
    {
        public string Name { get; set; } = "Options";
        public string? Description { get; set; } = "";
        public ICommandStructure Structure { get; set; } = new OptionsCommandStructure();
    }

    public class OptionsCommandStructure : ICommandStructure
    {
        public string Key { get; set; } = "Options";
        public bool hasValue { get; set; } = false;
        public object? Value { get; set; } = null;
        public List<ICommandStructure>? Options { get; set; } = new List<ICommandStructure>()
        {
            new SubOptionA(),
            new SubOptionB()
        };

        public void Handle()
        {
            return;
        }
    }

    public class SubOptionA : ICommandStructure
    {
        public string Key { get; set; } = "OptionA";
        public bool hasValue { get; set; } = true;
        public object? Value { get; set; } = null;
        public List<ICommandStructure>? Options { get; set; } = null;

        public void Handle()
        {
            Console.WriteLine("OptionA value: " + Value);
        }
    }

    public class SubOptionB: ICommandStructure
    {
        public string Key { get; set; } = "OptionB";
        public bool hasValue { get; set; } = true;
        public object? Value { get; set; } = null;
        public List<ICommandStructure>? Options { get; set; } = new List<ICommandStructure>()
        {
            new SubOptionA()
        };

        public void Handle()
        {
            Console.WriteLine("OptionB value: " + Value);
        }
    }

}
