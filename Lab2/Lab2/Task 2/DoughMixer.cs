using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
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
}
