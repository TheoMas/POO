using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp1
{
    public class Weapons
    {
        public string name { get; }
        public int maxDamage { get; }
        public int minDamage { get; }

        public enum EWeaponType
        {
            Direct,
            Explosive,
            Guided
        }

        public EWeaponType type { get; }

        public Weapons(string newName, int newMaxDamage, int newMinDamage, EWeaponType newType) 
        {
            name = newName;
            maxDamage = newMaxDamage;
            minDamage = newMinDamage;
            type = newType;
        }

        public static void WeaponInfo(Weapons weapon)
        {
            Console.WriteLine($" Nom : {weapon.name} \n Dégats max : {weapon.maxDamage} \n Dégats min : {weapon.minDamage} \n Type : {weapon.type} \n");
        }
    }
}
