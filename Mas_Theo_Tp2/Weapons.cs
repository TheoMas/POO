using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp1
{
    public class Weapons : IWeapon
    {
        public string Name { get; set; }
        public double MaxDamage { get; set; }
        public double MinDamage { get; set; }
        public enum EWeaponType
        {
            Direct,
            Explosive,
            Guided
        };

        public EWeaponType Type { get; set; } 
        public double AverageDamage { get; set; }
        public double ReloadTime { get; set; }
        public double TimeBeforReload { get; set; }
        public bool IsReload => ReloadTime == 0 ? true : false;
        private static Random rand = new Random();

        public Weapons(string newName, int newMinDamage, int newMaxDamage, EWeaponType newType, double newReloadTime) 
        {
            Name = newName;
            MaxDamage = newMaxDamage;
            MinDamage = newMinDamage;
            Type = newType;
            ReloadTime = newReloadTime;
            TimeBeforReload = newReloadTime;
        }

        public static void WeaponInfo(Weapons weapon)
        {
            Console.WriteLine($" Nom : {weapon.Name} \n Dégats max : {weapon.MaxDamage} \n Dégats min : {weapon.MinDamage} \n Type : {weapon.Type} \n");
        }

        public double Shoot()
        {
            if (IsReload)
            {
                return 0;
            }

            if (this.Type == EWeaponType.Direct)
            {
                int luck = rand.Next(1,10);
                if (luck == 1)
                {
                    return 0;
                } 
                else
                {
                    return rand.NextDouble() * (this.MaxDamage - this.MinDamage) + this.MinDamage;
                }
            }
            else if (this.Type == EWeaponType.Explosive)
            {
                int luck = rand.Next(1, 4);
                if (luck == 1)
                {
                    return 0;
                }
                else
                {
                    return (rand.NextDouble() * (this.MaxDamage - this.MinDamage) + this.MinDamage) * this.ReloadTime;
                }
            }
            else if (this.Type == EWeaponType.Guided)
            {
                return this.MinDamage;
            }
            else
            {
                throw new Exception("Type non implémenté");
            }
        }
    }
}
