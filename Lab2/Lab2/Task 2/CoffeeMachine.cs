using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
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
}
