using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp1
{
    internal class Spaceship
    {
        private int maxStructure;
        private int maxShield;
        private int currentStructure;
        private int currentShield;
        private bool isDestroyed;
        public List<Weapons> spaceshipWeapons { get; }

        public Spaceship(int newMaxStructure, int newMaxShield, int newCurrentStructure, int newCurrentShield) 
        {
            this.maxStructure = newMaxStructure;
            this.maxShield = newMaxShield;
            this.currentStructure = newCurrentStructure;
            this.currentShield = newCurrentShield;
            this.isDestroyed = false;
            spaceshipWeapons = new List<Weapons>();
        }

        public void AddWeapon(Weapons weapon)
        {
            bool weaponAdded = false;
            foreach (Weapons armoryWeapon in Armory.GetInstance().GetWeaponList())
            {
                if(weapon == armoryWeapon)
                {
                    spaceshipWeapons.Add(weapon);
                    weaponAdded = true;
                }
            }
            if (!weaponAdded)
            {
                throw new ArmoryException($"Ajout d'un arme non-existante dans l'armurerie, utilisation de la fonction Armory.getInstance().GetWeaponByName() requise");

            }
        }

        public void AddMultipleWeapons(List<Weapons> weaponList)
        {
            bool weaponAdded = false;
            foreach (Weapons weapon in weaponList)
            {
                foreach (Weapons armoryWeapon in Armory.GetInstance().GetWeaponList())
                {
                    if (weapon == armoryWeapon)
                    {
                        spaceshipWeapons.Add(weapon);
                        weaponAdded = true;
                    }
                }
                if (!weaponAdded)
                {
                    throw new ArmoryException($"Ajout d'un arme non-existante dans l'armurerie, utilisation de la fonction Armory.getInstance().GetWeaponList() requise");

                }
            }
        }

        public void RemoveWeapons(Weapons weapon) 
        { 
            spaceshipWeapons.Remove(weapon);
        }

        public void ClearWeapons()
        {
            spaceshipWeapons.Clear();
        }

        public void ViewWeapons()
        {
            foreach (Weapons weapon in spaceshipWeapons)
            {
                Weapons.WeaponInfo(weapon);
            }
        }
        public void ViewShip()
        {
            Console.WriteLine($" Structure max : {maxStructure} \n Structure actuelle : {currentStructure} \n Boucliers max : {maxShield} \n" +
                $" Boucliers actuels : {currentShield} \n Vaisseau détruit : {isDestroyed} \n");
            Console.WriteLine(" - Liste des armes : \n");
            ViewWeapons();
        }

        public double AverageDamage()
        {
            double damageTotal = 0;
            double nbWeapons = 0;
            foreach (Weapons weapon in spaceshipWeapons)
            {
                damageTotal += (weapon.maxDamage + weapon.minDamage) / 2;
                nbWeapons++;
            }
            return damageTotal/nbWeapons;
        }
    }
}
