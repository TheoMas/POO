using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    internal class F18 : Spaceship, IAbility
    {
        public F18(string newName) : base(newName)
        {
            this.Structure = 15;
            this.Shield = 0;
            this.CurrentStructure = this.Structure;
            this.CurrentShield = this.Shield;
        }
        
        public void UseAbility(List<Spaceship> spaceships)
        {
            if(spaceships.Any(spaceship => (spaceship == this) && (spaceship.Name == this.Name)))
            {
                int spaceshipIndex = spaceships.FindIndex(spaceship => (spaceship == this) && (spaceship.Name == this.Name));
                if (spaceships[spaceshipIndex - 1].GetType() == typeof(ViperMKII))
                {
                    spaceships[spaceshipIndex - 1].TakeDamages(10);
                    this.TakeDamages(this.CurrentStructure);
                }
                else if(spaceships[spaceshipIndex + 1].GetType() == typeof(ViperMKII))
                {
                    spaceships[spaceshipIndex + 1].TakeDamages(10);
                    this.TakeDamages(this.CurrentStructure);
                }
            }
        }
    }
}
