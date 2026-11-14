using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    public class Employee : StructuralComponent
    {
        private readonly decimal _salary;
        public string Profession { get; }
        public int Expierence { get; }

        public Employee(string name, string profession, int expierence, decimal salary) : base(name)
        {
            Profession = profession;
            Expierence = expierence;

            if (salary < 0)
            {
                Console.WriteLine("Не можна встановити від'ємну зарплату.");
                _salary = 0;
            }
            else
            {
                _salary = salary;
            }
        }

        public override void Add(StructuralComponent structuralComponent)
        {
            Console.WriteLine("Неможливо: це співробітник");
        }

        public override void Remove(StructuralComponent structuralComponent)
        {
            Console.WriteLine("Неможливо: це співробітник");
        }

        public override bool TryRecievePayment(decimal sum)
        {
            if (sum < _salary)
            {
                Console.WriteLine($"Employee {Name} did not recieve salary. Incorrect sum.");
                return false;
            }

            Balance = sum;

            Console.WriteLine($"Employee {Name}: recieved {sum} UAH.");
            return true;
        }

        public override decimal GetRequiredSum()
        {
            return _salary;
        }

        public override void ShowInfo(int depth)
        {
            Console.WriteLine($"{new string(' ', depth * 2)} {Name}, {Profession}, {Expierence} years: {_salary} UAH.");
        }
    }
}
