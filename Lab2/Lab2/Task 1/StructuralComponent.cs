using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    public abstract class StructuralComponent
    {
        protected string Name { get; }
        public decimal Balance { get; protected set; }

        protected StructuralComponent(string name)
        {
            Name = name;
            Balance = 0m;
        }

        public abstract void Add(StructuralComponent structuralComponent);
        public abstract void Remove(StructuralComponent structuralComponent);
        public abstract bool TryRecievePayment(decimal sum);
        public abstract decimal GetRequiredSum();
        public abstract void ShowInfo(int depth);
    }
}
