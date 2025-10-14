using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp1
{
    public sealed class Armory
    {
        private static Armory? armoryInstance;
        private List<Weapons>armoryWeaponList = new List<Weapons>();

        private void Init()
        {
            Weapons explosiveWeapon = new Weapons("PATATOR 9000", 100, 10, Weapons.EWeaponType.Explosive);
            Weapons directWeapon = new Weapons("GATLING TURBO-COSMIQUE", 10, 1, Weapons.EWeaponType.Direct);
            Weapons guidedWeapon = new Weapons("FULGUROQUETTES", 50, 50, Weapons.EWeaponType.Guided);
            armoryWeaponList.Add(explosiveWeapon);
            armoryWeaponList.Add(directWeapon);
            armoryWeaponList.Add(guidedWeapon);
        }
        private Armory() 
        {
            Init();
        }

        //Déclaration de l'instance du singleton d'armory
        public static Armory GetInstance() 
        {
            if (armoryInstance == null)
            {
                armoryInstance = new Armory();
            }
            return armoryInstance;
        }

        public void ViewArmory()
        {
            foreach (Weapons weapon in armoryWeaponList)
            {
                Weapons.WeaponInfo(weapon);
            }
        }

        public List<Weapons> GetWeaponList()
        {
            return armoryWeaponList;
        }

        public void AddWeapon(Weapons weapon)
        {
            armoryWeaponList.Add(weapon);
        }

        public void RemoveWeapon(Weapons weapon)
        {
            armoryWeaponList.Remove(weapon);
        }

        public void ClearWeapons(Weapons weapon)
        {
            armoryWeaponList.Clear();
        }

        public Weapons GetWeaponByName(string weaponName)
        {
            while (true)
            {
                try
                {
                    foreach (Weapons weapon in armoryWeaponList)
                    {
                        if (weaponName == weapon.name)
                        {
                            return weapon;
                        }
                    }
                    throw new ArmoryException($"L'arme {weaponName} ne fait par partie de l'armurerie, veuillez" +
                        $" selectionner l'une des armes suivantes : ");
                }
                catch (ArmoryException ex)
                {
                    Console.WriteLine(ex.Message);
                    Armory.GetInstance().ViewArmory();
                    Console.WriteLine("Arme séléctionnée : ");
                    weaponName = Console.ReadLine().ToString();
                }
            }
        }
    }
}
