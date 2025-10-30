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
        public List<Spaceship> Ennemies { get; private set; }
        public SpaceInvaders() 
        {
            Init();
        }

        private void Init()
        {
            //init des armes
            Armory.GetInstance().AddWeapon(new Weapons("ASTEROHACHE", 3, 100, Weapons.EWeaponType.Explosive, 4));

            //init des vaisseaux
            Ennemies.Add(new Dart("Dart I"));
            Ennemies.Add(new BWing("BWing I"));
            Ennemies.Add(new Rocinante("Rocinante I"));
            Ennemies.Add(new F18("F18 I"));
            Ennemies.Add(new Tardis("Tardis I"));

            //init des joueurs
            player1 = new Player("Théo","MAS", "TMA", new ViperMKII("Le GOAT"));
        }

        static void Main()
        {
            SpaceInvaders spaceInvader = new SpaceInvaders();

            Console.WriteLine(player1.ToString() + "\n");
 

            Console.WriteLine(" - Armurerie : \n");
            Armory.GetInstance().ViewArmory();

            Console.WriteLine("\n - Vaisseau du joueur 1 : \n");
            player1.BattleShip.ViewShip();
            Console.ReadKey();
        }
    }
}
