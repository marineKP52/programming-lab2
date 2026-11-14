using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
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

        public override bool TryRecievePayment(decimal sum)
        {
            if (sum < GetRequiredSum())
            {
                Console.WriteLine($"Structural component {Name} did not recieve payment. Not enough money.");
                return false;
            }

            Balance = sum;

            Console.WriteLine($"Structural component {Name}: recieved {sum} UAH.");

            foreach (StructuralComponent child in _children)
            {
                decimal childPayment = child.GetRequiredSum();

                if (Balance >= childPayment && child.TryRecievePayment(childPayment))
                {
                    Balance -= childPayment;
                }
            }

            return true;
        }

        public override decimal GetRequiredSum()
        {
            decimal totalSum = 0;

            foreach (StructuralComponent child in _children)
            {
                totalSum += child.GetRequiredSum();
            }

            return totalSum;
        }

        public override void ShowInfo(int depth)
        {
            Console.WriteLine($"{new string(' ', depth * 2)} {Name}. Total sum: {GetRequiredSum()} UAH.");

            foreach (StructuralComponent child in _children)
            {
                child.ShowInfo(depth + 2);
            }
        }
    }
}
