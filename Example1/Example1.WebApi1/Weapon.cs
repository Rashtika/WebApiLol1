using ConsoleApp1;

namespace Example1.WebApi1
{
    public class Weapon : Item
    {
        public int Attack { get; set; }

        public override string getDescription()
        {
            return $"This is a weapon {this.Name}";
        }
    }
}
