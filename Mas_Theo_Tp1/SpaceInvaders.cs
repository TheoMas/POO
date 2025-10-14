using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp1
{
    internal class SpaceInvaders
    {
        public static Player player1 { get; private set; }
        public static Player player2 { get; private set; }
        public static Player player3 { get; private set; }
        public SpaceInvaders() 
        {
            Init();
        }

        private static void Init()
        {

            //init des armes
            List<Weapons> sp2Weapons = new List<Weapons>();
            foreach (Weapons weapon in Armory.GetInstance().GetWeaponList())
            {
                sp2Weapons.Add(weapon);
            }
            Weapons ASTEROHACHE = new Weapons("ASTEROHACHE", 1000, 3, Weapons.EWeaponType.Direct);
            Armory.GetInstance().AddWeapon(ASTEROHACHE);

            //init des vaisseaux
            Spaceship spaceship1 = new Spaceship(50, 30, 50, 30);
            Spaceship spaceship2 = new Spaceship(50, 30, 50, 30);
            Spaceship spaceship3 = new Spaceship(50, 30, 50, 30);
            spaceship1.AddWeapon(Armory.GetInstance().GetWeaponByName("PATATOR9000"));
            spaceship2.AddMultipleWeapons(sp2Weapons);
            spaceship3.AddWeapon(Armory.GetInstance().GetWeaponByName("FULGUROQUETTES"));
            spaceship3.AddWeapon(Armory.GetInstance().GetWeaponByName("ASTEROHACHE"));

            //init des joueurs
            player1 = new Player("Théo", "MAS", "TMA", spaceship1);
            player2 = new Player("Gaetan", "KREMSER", "GKR", spaceship2);
            player3 = new Player("Julie", "FROMAGEAT", "JFR", spaceship3);
        }

        static void Main()
        {
            SpaceInvaders spaceInvader = new SpaceInvaders();

            Console.WriteLine(player1.ToString());
            Console.WriteLine(player2.ToString());
            Console.WriteLine(player3.ToString() + "\n");

            Console.WriteLine(" - Armurerie : \n");
            Armory.GetInstance().ViewArmory();

            Console.WriteLine("\n - Vaisseau du joueur 1 : \n");
            player1.playerspaceShip.ViewShip();
            Console.ReadKey();
        }
    }
}
