using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    internal class Tardis : Spaceship, IAbility
    {
        public Tardis(string newName) : base(newName)
        {
            this.Structure = 1;
            this.Shield = 0;
            this.CurrentStructure = this.Structure;
            this.CurrentShield = this.Shield;
        }

        public void UseAbility(List<Spaceship> spaceships)
        {
            Random rand = new Random();
            int pickedShipIndex = rand.Next(0, spaceships.Count);
            int goingToIndex = rand.Next(0, spaceships.Count);
            Spaceship pickedShip = spaceships[pickedShipIndex];
            Spaceship prevShip = spaceships[goingToIndex];
            spaceships[goingToIndex] = pickedShip;
            spaceships[pickedShipIndex] = prevShip;
        }

    }
}
