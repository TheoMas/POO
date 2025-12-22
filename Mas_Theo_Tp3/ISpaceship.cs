using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    public interface ISpaceship
    {
        string Name { get; set; }
        double Structure { get; set; }
        double Shield { get; set; }
        bool IsDestroyed { get; }
        int MaxWeapons { get; }
        List<Weapons> SpaceshipWeapons { get; }
        double AverageDamages { get; }
        double CurrentStructure { get; set; }
        double CurrentShield { get; set; }
        bool BelongsPlayer { get; }
        void TakeDamages(double damages);
        void RepairShield(double repair);
        void ShootTarget(Spaceship target);
        void ReloadWeapons();
        void AddWeapon(Weapons weapon);
        void RemoveWeapon(Weapons oWeapon);
        void ClearWeapons();
        void ViewShip();
        void ViewWeapons();
        
    }
}
