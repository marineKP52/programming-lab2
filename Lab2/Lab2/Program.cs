namespace Lab2
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Task 1

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

    public abstract class KitchenDevice
    {
        protected bool IsTurnedOn { get; private set; }

        public void TurnOn()
        {
            IsTurnedOn = true;
            Console.WriteLine($"{GetType().Name} has been turned on.");
        }

        public void TurnOff()
        {
            IsTurnedOn = false;
            Console.WriteLine($"{GetType().Name} has been turned off.");
        }

        public abstract void Clean();
    }

    public enum CoffeeType
    {
        Espresso,
        Cappuccino,
        Latte
    }

    public struct Recipe
    {
        public float Coffee { get; }
        public float Milk { get; }
        public float Water { get; }

        public Recipe(float coffee, float milk, float water)
        {
            Coffee = coffee;
            Milk = milk;
            Water = water;
        }
    }

    public class CoffeeMachine : KitchenDevice
    {
        private float _coffee = 0;
        private float _milk = 0;
        private float _water = 0;

        private const float MAX_COFFEE = 3000;
        private const float MAX_MILK = 5000;
        private const float MAX_WATER = 5000;

        private Dictionary<CoffeeType, Recipe> _recipes = new Dictionary<CoffeeType, Recipe>()
        {
            { CoffeeType.Espresso, new Recipe(15, 0, 35) },
            { CoffeeType.Cappuccino, new Recipe(10, 200, 20) },
            { CoffeeType.Latte, new Recipe(10, 150, 45) }
        };

        public void AddCoffee(float amount)
        {
            if (amount <= 0 || _coffee + amount > MAX_COFFEE)
            {
                Console.WriteLine("Invalid amount of coffee.");
                return;
            }

            _coffee += amount;
            Console.WriteLine("Coffee has been added.");
        }

        public void AddMilk(float amount)
        {
            if (amount <= 0 || _milk + amount > MAX_MILK)
            {
                Console.WriteLine("Invalid amount of milk.");
                return;
            }

            _milk += amount;
            Console.WriteLine("Milk has been added.");
        }

        public void AddWater(float amount)
        {
            if (amount <= 0 || _water + amount > MAX_WATER)
            {
                Console.WriteLine("Invalid amount of water.");
                return;
            }

            _water += amount;
            Console.WriteLine("Water has been added.");
        }

        public void MakeCoffee(CoffeeType type)
        {
            if (!IsTurnedOn)
            {
                Console.WriteLine("Coffee machine is turned off.");
                return;
            }

            if (!_recipes.TryGetValue(type, out Recipe recipe))
            {
                Console.WriteLine("Unknown coffee type.");
                return;
            }

            if (_coffee < recipe.Coffee ||
                _milk < recipe.Milk ||
                _water < recipe.Water)
            {
                Console.WriteLine("Not enough ingredients.");
                return;
            }

            _coffee -= recipe.Coffee;
            _milk -= recipe.Milk;
            _water -= recipe.Water;

            Console.WriteLine($"{type} has been prepared.");
        }

        public override void Clean()
        {
            Console.WriteLine("Coffee machine has been cleaned.");
        }
    }

    public enum JuicerMode
    {
        NotSelected,
        Normal,
        Fast,
        Slow
    }

    public class Juicer : KitchenDevice
    {
        private bool _filterIsClean = true;
        private JuicerMode _mode = JuicerMode.NotSelected; 

        public void SelectMode(JuicerMode mode)
        {
            if (!IsTurnedOn)
            {
                Console.WriteLine("Juicer is turned off.");
                return;
            }

            _mode = mode;
            Console.WriteLine($"Juicer mode selected: {mode}.");
        }

        public void MakeJuice(string fruit)
        {
            if (!IsTurnedOn)
            {
                Console.WriteLine("Juicer is turned off.");
                return;
            }

            if (_mode == JuicerMode.NotSelected)
            {
                Console.WriteLine("Select a juicer mode first.");
                return;
            }

            if (!_filterIsClean)
            {
                Console.WriteLine("Clean the juicer filter first.");
                return;
            }

            Console.WriteLine($"Juice made from {fruit} using {_mode} mode.");
            _filterIsClean = false;
        }

        public void CleanFilter()
        {
            _filterIsClean = true;
        }

        public override void Clean()
        {
            CleanFilter();
            Console.WriteLine("Juicer has been cleaned.");
        }
    }

    public class MeatGrinder : KitchenDevice
    {
        private int _nozzleSize = 5;

        public void ChangeNozzle(int size)
        {
            if (size < 1  || size > 10)
            {
                Console.WriteLine("Invalid nozzle size.");
                return;
            }

            _nozzleSize = size;
            Console.WriteLine($"Nozzle size changed to {_nozzleSize}.");
        }

        public void GrindMeat(float weight)
        {
            if (!IsTurnedOn)
            {
                Console.WriteLine("Meat grinder is turned off.");
                return;
            }

            if (weight <= 0)
            {
                Console.WriteLine("Invalid meat weight.");
                return;
            }

            Console.WriteLine($"{weight} kg of meat has been minced.");
        }

        public override void Clean()
        {
            if (IsTurnedOn)
            {
                Console.WriteLine("Turn off the meat grinder before cleaning.");
                return;
            }

            Console.WriteLine("Manual cleaning required: disassemble and wash the meat grinder.");
        }
    }

    public class DoughMixer : KitchenDevice
    {
        private int _speed = 1;

        public void SetSpeed(int speed)
        {
            if (speed < 1 || speed > 5)
            {
                Console.WriteLine("Speed must be between 1 and 5.");
                return;
            }

            _speed = speed;
            Console.WriteLine($"Mixer speed set to {_speed}.");
        }

        public void MixDough(float flour, float water)
        {
            if (!IsTurnedOn)
            {
                Console.WriteLine("Dough mixer is turned off.");
                return;
            }

            if (flour <= 0 || water <= 0)
            {
                Console.WriteLine("Invalid ingredient amount.");
                return;
            }

            Console.WriteLine(
                $"Dough has been mixed using {flour} kg of flour " +
                $"and {water} liters of water at speed {_speed}.");
        }

        public override void Clean()
        {
            Console.WriteLine("Dough mixer has been cleaned.");
        }
    }

    public class FoodProcessorFacade
    {
        private readonly CoffeeMachine _coffeeMachine;
        private readonly Juicer _juicer;
        private readonly MeatGrinder _meatGrinder;
        private readonly DoughMixer _doughMixer;

        public FoodProcessorFacade()
        {
            _coffeeMachine = new CoffeeMachine();
            _juicer = new Juicer();
            _meatGrinder = new MeatGrinder();
            _doughMixer = new DoughMixer();
        }

        public void TurnOnAll()
        {
            _coffeeMachine.TurnOn();
            _juicer.TurnOn();
            _meatGrinder.TurnOn();
            _doughMixer.TurnOn();
        }

        public void TurnOffAll()
        {
            _coffeeMachine.TurnOff();
            _juicer.TurnOff();
            _meatGrinder.TurnOff();
            _doughMixer.TurnOff();
        }

        public void CleanAll()
        {
            _coffeeMachine.Clean();
            _juicer.Clean();
            _meatGrinder.Clean();
            _doughMixer.Clean();
        }

        public void AddCoffeeIngredients(float coffee, float milk, float water)
        {
            _coffeeMachine.AddCoffee(coffee);
            _coffeeMachine.AddMilk(milk);
            _coffeeMachine.AddWater(water);
        }

        public void MakeCoffee(CoffeeType type)
        {
            _coffeeMachine.MakeCoffee(type);
        }

        public void SelectJuicerMode(JuicerMode mode)
        {
            _juicer.SelectMode(mode);
        }

        public void MakeJuice(string fruit)
        {
            _juicer.MakeJuice(fruit);
        }

        public void ChangeMeatGrinderNozzle(int size)
        {
            _meatGrinder.ChangeNozzle(size);
        }

        public void GrindMeat(float weight)
        {
            _meatGrinder.GrindMeat(weight);
        }

        public void SetMixerSpeed(int speed)
        {
            _doughMixer.SetSpeed(speed);
        }

        public void MixDough(float flour, float water)
        {
            _doughMixer.MixDough(flour, water);
        }
    }
}
