using System;

namespace Mas_Theo_Tp1
{
    public interface IWeapon
    {
        string Name { get; set; }
        double MinDamage { get; set; }
        double MaxDamage { get; set; }
        double AverageDamage { get; }
        double ReloadTime { get; set; }
        double TimeBeforReload { get; set; }
        bool IsReload { get; }
        double Shoot();
    }
}