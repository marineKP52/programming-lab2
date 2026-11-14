using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
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
}
