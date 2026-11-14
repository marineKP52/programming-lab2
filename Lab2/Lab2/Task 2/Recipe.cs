using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
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
}
