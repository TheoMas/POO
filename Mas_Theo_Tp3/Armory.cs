using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    public sealed class Armory
    {
        private static Armory? armoryInstance;
        private List<Weapons>armoryWeaponList = new List<Weapons>();

        private void Init()
        {
            this.armoryWeaponList.Add(new Weapons("Laser", 2, 3, Weapons.EWeaponType.Direct, 2));
            this.armoryWeaponList.Add(new Weapons("Hammer", 1, 8, Weapons.EWeaponType.Explosive, 1.5));
            this.armoryWeaponList.Add(new Weapons("Torpille", 3, 3, Weapons.EWeaponType.Guided, 2));
            this.armoryWeaponList.Add(new Weapons("Mitrailleuse", 6, 8, Weapons.EWeaponType.Direct, 1));
            this.armoryWeaponList.Add(new Weapons("EMG", 1, 7, Weapons.EWeaponType.Explosive, 4));
            this.armoryWeaponList.Add(new Weapons("Missile", 4, 100, Weapons.EWeaponType.Guided, 4));
            this.armoryWeaponList.Add(new Weapons("Poiscaille", 100, 100, Weapons.EWeaponType.Explosive, 10));
            this.armoryWeaponList.Add(new Weapons("Bang-Bang", 10, 11, Weapons.EWeaponType.Direct, 1));
            this.armoryWeaponList.Add(new Weapons("Zap", 3, 5, Weapons.EWeaponType.Direct, 2));
        }
        private Armory() 
        {
            Init();
        }

        //Déclaration de l'instance du singleton d'armory
        public static Armory GetInstance() 
        {
            #region Gestion singleton
            if (armoryInstance == null)
            {
                armoryInstance = new Armory();
            }
            return armoryInstance;
            #endregion
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
                        if (weaponName == weapon.Name)
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

        public List<Weapons> GetBestAverageDamages()
        {
            // Trier par dégâts moyens décroissants et prendre les 5 premières
            return armoryWeaponList.OrderByDescending(weapon => weapon.AverageDamage).Take(5).ToList();
        }

        public List<Weapons> GetBestMinDamages()
        {
            // Trier par dégâtsminimums décroissants et prendre les 5 premières
            return armoryWeaponList.OrderByDescending(weapon => weapon.MinDamage).Take(5).ToList();
        }
    }
}
