using CherryAya_CommandRoute.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CherryAya_CommandRoute_Test.Commands
{
    public class RandomCommand : ICommand
    {
        public string Name { get; set; } = "Random";
        public string? Description { get; set; } = "";
        public ICommandStructure Structure { get; set; } = new RandomCommandStructure();
    }

    public class RandomCommandStructure : ICommandStructure
    {
        public string Key { get; set; } = "random";
        public bool hasValue { get; set; } = true;
        public object? Value { get; set; } = null;
        public List<ICommandStructure>? Options { get; set; } = null;

        public void Handle()
        {
            Random random = new Random();
            Console.WriteLine(random.Next(0, Convert.ToInt32(Value)));
        }
    }
}
