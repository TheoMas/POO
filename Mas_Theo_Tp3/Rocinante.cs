using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    internal class Rocinante : Spaceship
    {
        private static Random rand = new Random();
        public Rocinante(string newName) : base(newName)
        {
            this.Structure = 3;
            this.Shield = 5;
            this.CurrentStructure = this.Structure;
            this.CurrentShield = this.Shield;
            this.SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Torpille"));
        }

        public override void TakeDamages(double damages)
        {
            int esquive = rand.Next(1, 2);
            if (esquive == 1)
            {
                Console.WriteLine($"Le vaisseau de type Rocinante {this.Name} ennemi a esquivé !");
                return;
            }
            else if (this.CurrentShield > 0)
            {
                double reste = 0;
                if (this.CurrentShield - damages < 0)
                {
                    reste = CurrentShield - damages;
                    CurrentShield = 0;
                    CurrentStructure -= damages;
                }
            }
            else
            {
                CurrentShield -= damages;
            }
        }
    }
}
