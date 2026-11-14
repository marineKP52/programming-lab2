namespace Lab2
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Task 1

            Composite university = new Composite("Igor Sikorsky KPI");
            
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
            Console.WriteLine($"Remainder: {university.Balance} UAH.");
            Console.WriteLine();
            Console.WriteLine();


            // Task 2

            FoodProcessorFacade foodProcessor = new FoodProcessorFacade();

            Console.WriteLine("========== Adding ingredients ==========");
            foodProcessor.AddCoffeeIngredients(1000, 2000, 3000);

            Console.WriteLine("\n========== Turning on devices ==========");
            foodProcessor.TurnOnAll();

            Console.WriteLine("\n========== Coffee machine ==========");
            foodProcessor.MakeCoffee(CoffeeType.Cappuccino);
            foodProcessor.MakeCoffee(CoffeeType.Espresso);

            Console.WriteLine("\n========== Juicer ==========");
            foodProcessor.SelectJuicerMode(JuicerMode.Normal);
            foodProcessor.MakeJuice("Apple");

            foodProcessor.MakeJuice("Orange");

            Console.WriteLine("\n========== Meat grinder ==========");
            foodProcessor.ChangeMeatGrinderNozzle(3);
            foodProcessor.GrindMeat(2);

            Console.WriteLine("\n========== Dough mixer ==========");
            foodProcessor.SetMixerSpeed(3);
            foodProcessor.MixDough(1, 0.5f);

            Console.WriteLine("\n========== Turning off devices ==========");
            foodProcessor.TurnOffAll();

            Console.WriteLine("\n========== Cleaning devices ==========");
            foodProcessor.CleanAll();
        }
    }
}
