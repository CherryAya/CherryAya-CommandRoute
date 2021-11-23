using CherryAya_CommandRoute.Entities;

namespace CherryAya_CommandRoute_Test.Commands
{
    public class HelloCommand : ICommand
    {
        public string Name { get; set; } = "Hello";
        public string? Description { get; set; } = "Hello World";
        public ICommandStructure Structure { get; set; } = new HelloCommandStructure(); 
    }

    public class HelloCommandStructure : ICommandStructure
    {
        public string Key { get; set; } = "Hello";
        public bool hasValue { get; set; } = false;
        public object? Value { get; set; } = null;
        public List<ICommandStructure>? Options { get; set; } = null;

        public void Handle()
        {
            Console.WriteLine("Hello world!");
        }
    }
}
