using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp1
{
    internal class Player : IPlayer
    {
        private readonly string firstName;
        private readonly string lastName;
        public string Name { get; set;}
        public string Alias { get; set;}
        public Spaceship BattleShip { get; set; }
        public Player(string newFirstname, string newLastname, string newAlias, Spaceship newPlayerspaceShip)
        {
            firstName = newFirstname;
            lastName = newLastname;
            Alias = newAlias;
            BattleShip = newPlayerspaceShip;
            firstName = FormatNames(firstName);
            lastName = FormatNames(lastName);
            Name = $"{firstName}{lastName}";
        }

        private static string FormatNames(string name)
        {
            name = $"{char.ToUpper(name[0])}{name.Substring(1).ToLower()}";
            return name;
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Player other)
            {
                return firstName == other.Name;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
