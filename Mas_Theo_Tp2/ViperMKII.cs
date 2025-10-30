using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp1
{
    internal class ViperMKII : Spaceship
    {
        public ViperMKII(string newName) : base(newName)
        {
            this.Structure = 10;
            this.Shield = 3;
            this.CurrentStructure = this.Structure;
            this.CurrentShield = this.Shield;
            this.BelongsPlayer = true;
            this.SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Mitrailleuse"));
            this.SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("EMG"));
            this.SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Missile"));
        }

        public override void ShootTarget(Spaceship target)
        {
            int i = 1;
            int choix;
            Weapons choixUser;
            foreach(Weapons weapon in this.SpaceshipWeapons)
            {
                Console.WriteLine($"{i} - {weapon.Name} , Reload time left : {weapon.TimeBeforReload} \n");
                i++;
            }
            Console.WriteLine("Choissisez le numéro d'arme a utiliser : ");
            choix = Convert.ToInt32(Console.ReadLine());
            bool choixUserIsValid = false;
            while (!choixUserIsValid)
            {
                if (choix < 3 || choix > 0
                    && !SpaceshipWeapons[choix].IsReload)
                {
                    choixUser = SpaceshipWeapons[choix];
                    target.TakeDamages(choixUser.Shoot());
                    choixUserIsValid = true;
                }
                else if (choix > 3 || choix < 0)
                {
                    Console.WriteLine("Le nombre entré ne fait pas parti des choix possibles, veuillez en selectionner un autre");
                }
                else if (SpaceshipWeapons[choix].IsReload)
                {
                    Console.WriteLine("Cette arme est en rechargement veuillez en selectionner une autre");
                }
            }
            
        }
    }
}
