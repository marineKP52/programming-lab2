namespace Lab2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Composite educationalInstitution = new Composite("Навчальний заклад");
            
            Composite fam = new Composite("Факультет прикладної математики");
            Composite iasa = new Composite("Інститут прикладного системного аналізу");
            
            Composite sp = new Composite("Кафедра системного програмування");
            Composite ai = new Composite("Кафедра штучного інтелекту");

            educationalInstitution.Add(fam);
            educationalInstitution.Add(iasa);

            iasa.Add(sp);
            iasa.Add(ai);

            fam.Add(new Employee("Anna", 30000m));
            fam.Add(new Employee("Viktor", 25000m));
            sp.Add(new Employee("Sarah", 15000m));
            ai.Add(new Employee("Svetlana", 35000m));
            ai.Add(new Employee("Volodymyr", 30000m));
            ai.Add(new Employee("Vasyl", 32500m));

            educationalInstitution.Display(1);
        }
    }

    public class Employee : StructuralComponent
    {
        private readonly decimal _salary;
        private string Profession { get; }

        public Employee(string name, decimal salary) : base(name)
        {
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

        public override decimal Pay()
        {
            return _salary;
        }

        public override void Display(int depth)
        {
            Console.WriteLine($"{new string('-', depth)} {Name}: виплачено {_salary} грн.");
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

        public override decimal Pay()
        {
            decimal totalSum = 0;

            foreach (StructuralComponent child in _children)
            {
                totalSum += child.Pay();
            }

            return totalSum;
        }

        public override void Display(int depth)
        {
            Console.WriteLine($"{new string('-', depth)} {Name}: виплачено {Pay()} грн.");

            foreach (StructuralComponent child in _children)
            {
                child.Display(depth + 2);
            }
        }
    }

    public abstract class StructuralComponent
    {
        protected string Name { get; }

        protected StructuralComponent(string name)
        {
            Name = name;
        }

        public abstract void Add(StructuralComponent structuralComponent);
        public abstract void Remove(StructuralComponent structuralComponent);
        public abstract decimal Pay();
        public abstract void Display(int depth);
    }

}
