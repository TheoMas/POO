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
            Console.WriteLine("Choisissez le numéro d'arme à utiliser : ");
            
            bool choixUserIsValid = false;
            while (!choixUserIsValid)
            {
                choix = Convert.ToInt32(Console.ReadLine());
                
                if (choix > 0 && choix <= SpaceshipWeapons.Count)
                {
                    choixUser = SpaceshipWeapons[choix - 1];
                    
                    if (choixUser.IsReload)
                    {
                        Console.WriteLine("Cette arme est en rechargement, veuillez en sélectionner une autre :");
                    }
                    else
                    {
                        double damage = choixUser.Shoot();
                        target.TakeDamages(damage);
                        choixUserIsValid = true;
                    }
                }
                else
                {
                    Console.WriteLine("Le nombre entré ne fait pas partie des choix possibles, veuillez en sélectionner un autre :");
                }
            }
        }
    }
}
