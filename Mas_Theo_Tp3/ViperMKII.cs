using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    internal class ViperMKII : Spaceship
    {
        private static Random rand = new Random();

        public ViperMKII(string newName) : base(newName)
        {
            this.Structure = 10;
            this.Shield = 15;
            this.CurrentStructure = this.Structure;
            this.CurrentShield = this.Shield;
            this.BelongsPlayer = true;
            this.SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Mitrailleuse"));
            this.SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("EMG"));
            this.SpaceshipWeapons.Add(Armory.GetInstance().GetWeaponByName("Missile"));
        }

        public override void ShootTarget(Spaceship target)
        {
            // Afficher l'état de toutes les armes
            Console.WriteLine("   📋 État des armes :");
            foreach (Weapons weapon in SpaceshipWeapons)
            {
                if (weapon.IsReload)
                {
                    Console.WriteLine($"      - {weapon.Name} : ⏳ Rechargement ({weapon.TimeBeforReload} tour(s) restant(s))");
                }
                else
                {
                    Console.WriteLine($"      - {weapon.Name} : ✅ Prête");
                }
            }
            Console.WriteLine();

            // Récupérer toutes les armes rechargées (TimeBeforReload == 0)
            List<Weapons> availableWeapons = SpaceshipWeapons.Where(w => !w.IsReload).ToList();

            if (availableWeapons.Count == 0)
            {
                Console.WriteLine("   ⚠️ Aucune arme disponible ! Toutes les armes sont en rechargement.");
                return;
            }

            // Choisir aléatoirement une arme parmi celles disponibles
            Weapons selectedWeapon = availableWeapons[rand.Next(availableWeapons.Count)];

            Console.WriteLine($"   🔫 Utilisation de : {selectedWeapon.Name}");
            
            // Tirer avec l'arme sélectionnée
            double damage = selectedWeapon.Shoot();
            target.TakeDamages(damage);
        }
    }
}
