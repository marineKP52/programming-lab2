namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Composite university = new Composite("Igor Sikorsky KPI");
            
            Composite fam = new Composite("Faculty of Applied Mathematics");
            Composite iasa = new Composite("Institute for Applied Systems Analysis");
            
            Composite sp = new Composite("Department of System Programming");
            Composite ai = new Composite("Department of Artificial Intelligence");

            university.Add(fam);
            university.Add(iasa);

            iasa.Add(sp);
            iasa.Add(ai);

            fam.Add(new Employee("Anna", "Math Professor", 5, 30000m));
            fam.Add(new Employee("Viktor", "Math Professor Assistant", 2, 15000m));
            sp.Add(new Employee("Sarah", "Office Coordinator", 3, 25000m));
            ai.Add(new Employee("Svetlana", "Data Science Professor", 15, 35000m));
            ai.Add(new Employee("Volodymyr", "AI Systems Proffessor", 4, 30000m));
            ai.Add(new Employee("Vasyl", "Office Coordinator", 20, 32500m));

            Console.WriteLine("==========Salary information=========");
            university.ShowInfo(0);

            Console.WriteLine();

            Console.WriteLine("==========Salary distribution=========");
            university.TryRecievePayment(170000m);
            Console.WriteLine($"Remainder: {university.Balance} UAH.");*/



        }
    }

    public abstract class StructuralComponent
    {
        protected string Name { get; }
        public decimal Balance { get; protected set; }

        protected StructuralComponent(string name)
        {
            Name = name;
            Balance = 0m;
        }

        public abstract void Add(StructuralComponent structuralComponent);
        public abstract void Remove(StructuralComponent structuralComponent);
        public abstract bool TryRecievePayment(decimal sum);
        public abstract decimal GetRequiredSum();
        public abstract void ShowInfo(int depth);
    }

    public class Employee : StructuralComponent
    {
        private readonly decimal _salary;
        public string Profession { get; }
        public int Expierence { get; }

        public Employee(string name, string profession, int expierence, decimal salary) : base(name)
        {
            Profession = profession;
            Expierence = expierence;
            
            if (salary < 0)
            {
                Console.WriteLine("Не можна встановити від'ємну зарплату.");
                _salary = 0;
            }
            else
            {
                _salary = salary;
            }
        }

        public override void Add(StructuralComponent structuralComponent)
        {
            Console.WriteLine("Неможливо: це співробітник");
        }

        public override void Remove(StructuralComponent structuralComponent)
        {
            Console.WriteLine("Неможливо: це співробітник");
        }

        public override bool TryRecievePayment(decimal sum)
        {
            if (sum < _salary)
            {
                Console.WriteLine($"Employee {Name} did not recieve salary. Incorrect sum.");
                return false;
            }
            
            Balance = sum;

            Console.WriteLine($"Employee {Name}: recieved {sum} UAH.");
            return true;
        }

        public override decimal GetRequiredSum()
        {
            return _salary;
        }

        public override void ShowInfo(int depth)
        {
            Console.WriteLine($"{new string(' ', depth*2)} {Name}, {Profession}, {Expierence} years: {_salary} UAH.");
        }
    }

    public class Composite : StructuralComponent
    {
        private readonly List<StructuralComponent> _children = new List<StructuralComponent>();

        public Composite(string name) : base(name)
        {

        }

        public override void Add(StructuralComponent structuralComponent)
        {
            _children.Add(structuralComponent);
        }

        public override void Remove(StructuralComponent structuralComponent)
        {
            _children.Remove(structuralComponent);
        }

        public override bool TryRecievePayment(decimal sum)
        {
            if (sum < GetRequiredSum())
            {
                Console.WriteLine($"Structural component {Name} did not recieve payment. Not enough money.");
                return false;
            }

            Balance = sum;

            Console.WriteLine($"Structural component {Name}: recieved {sum} UAH.");

            foreach (StructuralComponent child in _children)
            {
                decimal childPayment = child.GetRequiredSum();
                
                if (Balance >= childPayment && child.TryRecievePayment(childPayment))
                {
                    Balance -= childPayment;
                }
            }

            return true;
        }

        public override decimal GetRequiredSum()
        {
            decimal totalSum = 0;

            foreach (StructuralComponent child in _children)
            {
                totalSum += child.GetRequiredSum();
            }

            return totalSum;
        }

        public override void ShowInfo(int depth)
        {
            Console.WriteLine($"{new string(' ', depth*2)} {Name}. Total sum: {GetRequiredSum()} UAH.");

            foreach (StructuralComponent child in _children)
            {
                child.ShowInfo(depth + 2);
            }
        }
    }

    public enum CoffeType
    {
        Espresso,
        Capucchino,
        Latte
    }

    public readonly struct Reciept
    {
        public readonly float Coffee;
        public readonly float Milk;
        public readonly float Water;

        public Reciept(float coffee, float milk, float water)
        {
            Coffee = coffee;
            Milk = milk;
            Water = water;
        }
    }

    public class CoffeMachine
    {
        private float _coffee;
        private float _milk;
        private float _water;

        private const float _MAXCOFFEE = 3000;
        private const float _MAXMILK = 5000;
        private const float _MAXWATER = 5000;

        private bool isTurnedOn;

        private Dictionary<CoffeType, Reciept> _coffeeReciepes = new Dictionary<CoffeType, Reciept>()
        {
            { CoffeType.Espresso, new Reciept(15, 0, 350) },
            { CoffeType.Capucchino, new Reciept(10, 200, 200) },
            { CoffeType.Latte, new Reciept(10, 0, 450) }
        };
            

        public CoffeMachine()
        {
            _coffee = 0;
            _milk = 0;
            _water = 0;
            isTurnedOn = false;
        }

        public void AddWater(float ml)
        {
            if (ml < 0)
            {
                Console.WriteLine("");
                return;
            }

            if (ml + _water > _MAXWATER)
            {
                Console.WriteLine("");
                return;
            }

            _water += ml;
        }
        public void AddMilk(float ml)
        {
            if (ml < 0)
            {
                Console.WriteLine("");
                return;
            }

            if (ml + _milk > _MAXMILK)
            {
                Console.WriteLine("");
                return;
            }

            _milk += ml;
        }

        public void AddCoffee(float gr)
        {
            if (gr < 0)
            {
                Console.WriteLine("");
                return;
            }

            if (gr + _coffee > _MAXCOFFEE)
            {
                Console.WriteLine("");
                return;
            }

            _coffee += gr;
        }

        public void TurnOn()
        {
            isTurnedOn = true;
        }

        public void TurnOff()
        {
            isTurnedOn = false;
        }

        public void Clean()
        {
            Console.WriteLine();
        }


        public void MakeCoffee(CoffeType coffeType)
        {
            Reciept coffeeDrink = _coffeeReciepes[coffeType];

            if (coffeeDrink.Water > _water || coffeeDrink.Milk > _milk || coffeeDrink.Coffee > _coffee)
            {
                Console.WriteLine("");
                return;
            }

            Console.WriteLine($"");
        }
    }

    public class Juicer
    {
        private bool isTurnedOn;

        public Juicer()
        {
            isTurnedOn = false;
        }
        

        public void TurnOn()
        {
            isTurnedOn = true;
        }

        public void TurnOff()
        {
            isTurnedOn = false;
        }

        public void Clean()
        {
            Console.WriteLine("");
        }
    }
}
