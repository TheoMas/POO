using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    public abstract class Spaceship : ISpaceship
    {
        public string Name { get; set; }
        public double Structure { get; set; }
        public double Shield { get; set; }
        public bool IsDestroyed { get; set; }
        public int MaxWeapons { get; set; }
        public double CurrentStructure { get; set; }
        public double CurrentShield { get; set; }
        public List<Weapons> SpaceshipWeapons { get; }
        public double AverageDamages { get; set; }
        public bool BelongsPlayer { get; set; }

        public Spaceship(string newName) 
        {
            this.Name = newName;
            this.MaxWeapons = 3;
            this.BelongsPlayer = false;
            this.SpaceshipWeapons = new List<Weapons>();
        }
        public Spaceship(string newName, int newMaxStructure, int newMaxShield) 
        {
            this.Name = newName;
            this.Structure = newMaxStructure;
            this.Shield = newMaxShield;
            this.CurrentStructure = newMaxStructure;
            this.CurrentShield = newMaxShield;
            this.IsDestroyed = false;
            this.MaxWeapons = 3;
            this.BelongsPlayer = false;
            SpaceshipWeapons = new List<Weapons>();
        }

        public void AddWeapon(Weapons weapon)
        {
            bool weaponAdded = false;
            foreach (Weapons armoryWeapon in Armory.GetInstance().GetWeaponList())
            {
                if(weapon == armoryWeapon)
                {
                    SpaceshipWeapons.Add(weapon);
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
                        SpaceshipWeapons.Add(weapon);
                        weaponAdded = true;
                    }
                }
                if (!weaponAdded)
                {
                    throw new ArmoryException($"Ajout d'un arme non-existante dans l'armurerie, utilisation de la fonction Armory.getInstance().GetWeaponList() requise");

                }
            }
        }

        public void RemoveWeapon(Weapons weapon) 
        { 
            SpaceshipWeapons.Remove(weapon);
        }

        public void ClearWeapons()
        {
            SpaceshipWeapons.Clear();
        }

        public void ViewWeapons()
        {
            foreach (Weapons weapon in SpaceshipWeapons)
            {
                Weapons.WeaponInfo(weapon);
            }
        }
        public void ViewShip()
        {
            Console.WriteLine($" Structure max : {Structure} \n Structure actuelle : {CurrentStructure} \n Boucliers max : {Shield} \n" +
                $" Boucliers actuels : {CurrentShield} \n Vaisseau détruit : {IsDestroyed} \n");
            Console.WriteLine(" - Liste des armes : \n");
            ViewWeapons();
        }

        public double AverageDamage()
        {
            double damageTotal = 0;
            double nbWeapons = 0;
            foreach (Weapons weapon in SpaceshipWeapons)
            {
                damageTotal += weapon.AverageDamage;
                nbWeapons++;
            }
            return damageTotal/nbWeapons;
        }

        public virtual void TakeDamages(double damages)
        {
            if(this.CurrentShield > 0)
            {
                if (this.CurrentShield - damages >= 0)
                {
                    CurrentShield -= damages;
                }
                else
                {
                    double reste = damages - CurrentShield;
                    CurrentShield = 0;
                    CurrentStructure -= reste;
                }
            }
            else
            {
                CurrentStructure -= damages;
            }
            if (CurrentStructure <= 0)
            {
                CurrentStructure = 0;
                IsDestroyed = true;
            }
        }

        public void RepairShield(double repair)
        {
            if(this.CurrentShield + repair <= this.Shield)
            {
                this.CurrentShield += repair;
            }
            else
            {
                this.CurrentShield = this.Shield;
            }

        }

        public virtual void ShootTarget(Spaceship target)
        {
            foreach(Weapons weapon in SpaceshipWeapons)
            {
                target.TakeDamages(weapon.Shoot());
            }
        }

        public virtual void ReloadWeapons()
        {
            foreach(Weapons weapon in SpaceshipWeapons)
            {
                if (!weapon.IsReload)
                {
                    weapon.TimeBeforReload--;
                }
            }
        }

        public void FullRepair()
        {
            this.CurrentStructure = this.Structure;
            this.CurrentShield = this.Shield;
            this.IsDestroyed = false;
            
            // Réinitialiser le temps de rechargement de toutes les armes
            foreach (Weapons weapon in SpaceshipWeapons)
            {
                weapon.TimeBeforReload = 0;
            }
        }
    }
}
