using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
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
