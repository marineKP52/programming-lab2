using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
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
}
