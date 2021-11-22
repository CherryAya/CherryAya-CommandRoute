using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CherryAya_CommandRoute.Entities;

namespace CherryAya_CommandRoute_Test.Commands
{
    public class TestCommand : ICommand
    {
        public string Name { get; set; } = "Test";
        public string? Description { get; set; } = "";
        public ICommandStructure Structure { get; set; } = new TestCommandStructure();
    }

    public class TestCommandStructure : ICommandStructure
    {
        public string Key { get; set; } = "test";
        public bool hasValue { get; set; } = true;
        public object? Value { get; set; } = null;
        public List<ICommandStructure>? Options { get; set; } = null;

        public void Handle()
        {
            Console.WriteLine(Value.ToString());
        }
    }
}
