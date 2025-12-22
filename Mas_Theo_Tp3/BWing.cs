using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    internal class BWing : Spaceship
    {
        public BWing(string newName) : base(newName)
        {
            this.Structure = 30;
            this.Shield = 0;
            this.CurrentStructure = this.Structure;
            this.CurrentShield = this.Shield;
            this.SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Hammer"));
        }
        public override void ReloadWeapons()
        {
            foreach (Weapons weapon in SpaceshipWeapons)
            {
                if (weapon.Type == Weapons.EWeaponType.Explosive)
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
