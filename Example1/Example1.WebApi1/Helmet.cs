using ConsoleApp1;

namespace Example1.WebApi1
{
    public class Helmet : Item
    {
        private int Armor {  get; set; }

        public override string getDescription()
        {
            return $"This is a helmet {this.Name}";
        }
    }
}
