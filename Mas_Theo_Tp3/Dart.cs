using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    internal class Dart : Spaceship
    {
        public Dart(string newName) : base (newName)
        {
            this.Structure = 10;
            this.Shield = 3;
            this.CurrentStructure = this.Structure;
            this.CurrentShield = this.Shield;
            base.SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Laser"));
        }

        public override void ReloadWeapons()
        {
            foreach (Weapons weapon in SpaceshipWeapons)
            {
                if (weapon.Type == Weapons.EWeaponType.Direct)
                {
                    weapon.TimeBeforReload = 0;
                }
                else if (!weapon.IsReload)
                {
                    weapon.TimeBeforReload--;
                }
            }
        }

    }
}
