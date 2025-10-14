using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp1
{
    internal class Player
    {
        private readonly string firstName;
        private readonly string lastName;
        private readonly string alias;
        public string name { get; private set; }
        public readonly Spaceship playerspaceShip;
        public Player(string newFirstname, string newLastname, string newAlias, Spaceship newPlayerspaceShip)
        {
            firstName = newFirstname;
            lastName = newLastname;
            alias = newAlias;
            playerspaceShip = newPlayerspaceShip;
            firstName = FormatNames(firstName);
            lastName = FormatNames(lastName);
            name = $"{firstName}{lastName}";
        }

        private static string FormatNames(string name)
        {
            name = $"{char.ToUpper(name[0])}{name.Substring(1).ToLower()}";
            return name;
        }

        public override string ToString()
        {
            return name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Player other)
            {
                return firstName == other.name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
