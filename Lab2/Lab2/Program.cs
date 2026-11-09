namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Composite university = new Composite("КПІ ім. Ігоря Сікорського");
            
            Composite fam = new Composite("Факультет прикладної математики");
            Composite iasa = new Composite("Інститут прикладного системного аналізу");
            
            Composite sp = new Composite("Кафедра системного програмування");
            Composite ai = new Composite("Кафедра штучного інтелекту");

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

            
        }
    }

    public abstract class StructuralComponent
    {
        protected string Name { get; }
        protected decimal Balance { get; set; }

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
            _salary = salary;
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
                Console.WriteLine($"Employee {Name} did not recieve salary. Not enough money.");
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
            Console.WriteLine($"{new string('\t', depth)} {Name}, {Profession}, {Expierence} years: {_salary} UAH.");
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
                Console.WriteLine($"Structural component {Name} did not recieve money. Not enough money.");
                return false;
            }

            Balance = sum;

            Console.WriteLine($"Structural component {Name}: recieved {sum} UAH.");

            foreach (StructuralComponent child in _children)
            {
                decimal childPayment = child.GetRequiredSum();
                
                if (child.TryRecievePayment(childPayment) && Balance >= childPayment)
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
            Console.WriteLine($"{new string('\t', depth)} {Name}. Total sum: {GetRequiredSum()}.");

            foreach (StructuralComponent child in _children)
            {
                child.ShowInfo(depth + 2);
            }
        }
    }
}
