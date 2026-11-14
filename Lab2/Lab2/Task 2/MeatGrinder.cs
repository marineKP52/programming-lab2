using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    public class MeatGrinder : KitchenDevice
    {
        private int _nozzleSize = 5;

        public void ChangeNozzle(int size)
        {
            if (size < 1 || size > 10)
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
}
