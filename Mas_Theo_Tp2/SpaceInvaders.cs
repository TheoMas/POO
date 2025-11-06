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
        private static Random rand = new Random();

        public SpaceInvaders() 
        {
            Ennemies = new List<Spaceship>();
            Init();
        }

        private void Init()
        {
            //init des armes
            Armory.GetInstance().AddWeapon(new Weapons("ASTEROHACHE", 3, 100, Weapons.EWeaponType.Explosive, 4));

            //init des vaisseaux ennemis
            Ennemies.Add(new Dart("Dart I"));
            Ennemies.Add(new BWing("BWing I"));
            Ennemies.Add(new Rocinante("Rocinante I"));
            Ennemies.Add(new F18("F18 I"));
            Ennemies.Add(new Tardis("Tardis I"));

            //init des joueurs
            player1 = new Player("Théo","MAS", "TMA", new ViperMKII("Le GOAT"));
        }

        private void PlayRound()
        {
            Console.WriteLine("\n========================================");
            Console.WriteLine("          NOUVEAU TOUR");
            Console.WriteLine("========================================\n");

            RegenerateShields();

            int playerShootOrder = rand.Next(0, GetAliveEnemiesCount() + 1);

            int enemyIndex = 0;
            bool playerHasShot = false;

            foreach (Spaceship enemy in Ennemies)
            {
                if (enemy.IsDestroyed)
                {
                    enemyIndex++;
                }

                if (!playerHasShot && enemyIndex == playerShootOrder)
                {
                    PlayerShoot();
                    playerHasShot = true;

                    if (GetAliveEnemiesCount() == 0)
                        return;
                }

                if (!enemy.IsDestroyed)
                {
                    EnemyShoot(enemy);
                    if (player1.BattleShip.IsDestroyed)
                        return;
                }

                enemyIndex++;
            }
            if (!playerHasShot)
            {
                PlayerShoot();
            }
            ReloadAllWeapons();

            DisplayRoundStatus();
        }

        private void RegenerateShields()
        {
            Console.WriteLine("--- Régénération des boucliers ---");

            if (player1.BattleShip.CurrentShield < player1.BattleShip.Shield)
            {
                double oldShield = player1.BattleShip.CurrentShield;
                player1.BattleShip.RepairShield(2);
                Console.WriteLine($"[{player1.BattleShip.Name}] Boucliers : {oldShield:F1} -> {player1.BattleShip.CurrentShield:F1}");
            }

            foreach (Spaceship enemy in Ennemies)
            {
                if (!enemy.IsDestroyed && enemy.CurrentShield < enemy.Shield)
                {
                    double oldShield = enemy.CurrentShield;
                    enemy.RepairShield(2);
                    Console.WriteLine($"[{enemy.Name}] Boucliers : {oldShield:F1} -> {enemy.CurrentShield:F1}");
                }
            }
            Console.WriteLine();
        }

        private void PlayerShoot()
        {
            List<Spaceship> aliveEnemies = Ennemies.Where(e => !e.IsDestroyed).ToList();
            
            if (aliveEnemies.Count == 0)
                return;

            Spaceship target = aliveEnemies[rand.Next(aliveEnemies.Count)];

            Console.WriteLine($" [{player1.Alias}] tire sur [{target.Name}] !");
            
            double targetShieldBefore = target.CurrentShield;
            double targetStructureBefore = target.CurrentStructure;

            player1.BattleShip.ShootTarget(target);

            DisplayDamageInfo(player1.BattleShip, target, targetShieldBefore, targetStructureBefore);
        }

        private void EnemyShoot(Spaceship enemy)
        {
            Console.WriteLine($" [{enemy.Name}] tire sur [{player1.BattleShip.Name}] !");
            
            double targetShieldBefore = player1.BattleShip.CurrentShield;
            double targetStructureBefore = player1.BattleShip.CurrentStructure;

            enemy.ShootTarget(player1.BattleShip);

            DisplayDamageInfo(enemy, player1.BattleShip, targetShieldBefore, targetStructureBefore);
        }

        private void DisplayDamageInfo(Spaceship attacker, Spaceship target, double shieldBefore, double structureBefore)
        {
            double shieldDamage = shieldBefore - target.CurrentShield;
            double structureDamage = structureBefore - target.CurrentStructure;
            double totalDamage = shieldDamage + structureDamage;

            Console.WriteLine($"   Dégâts infligés : {totalDamage:F1}");
            
            if (shieldDamage > 0)
                Console.WriteLine($"   Boucliers : {shieldBefore:F1} -> {target.CurrentShield:F1}");
            
            if (structureDamage > 0)
                Console.WriteLine($"   Structure : {structureBefore:F1} -> {target.CurrentStructure:F1}");

            // Vérifier si le vaisseau est détruit
            if (target.CurrentStructure <= 0 && !target.IsDestroyed)
            {
                target.IsDestroyed = true;
                Console.WriteLine($"    [{target.Name}] a été DÉTRUIT !");
            }
            Console.WriteLine();
        }

        private void ReloadAllWeapons()
        {
            player1.BattleShip.ReloadWeapons();
            
            foreach (Spaceship enemy in Ennemies)
            {
                if (!enemy.IsDestroyed)
                    enemy.ReloadWeapons();
            }
        }

        private int GetAliveEnemiesCount()
        {
            return Ennemies.Count(e => !e.IsDestroyed);
        }

        private void DisplayRoundStatus()
        {
            Console.WriteLine("--- État des vaisseaux ---");
            Console.WriteLine($"[{player1.BattleShip.Name}] Structure: {player1.BattleShip.CurrentStructure:F1}/{player1.BattleShip.Structure} | Boucliers: {player1.BattleShip.CurrentShield:F1}/{player1.BattleShip.Shield}");
            
            foreach (Spaceship enemy in Ennemies)
            {
                if (!enemy.IsDestroyed)
                {
                    Console.WriteLine($"[{enemy.Name}] Structure: {enemy.CurrentStructure:F1}/{enemy.Structure} | Boucliers: {enemy.CurrentShield:F1}/{enemy.Shield}");
                }
                else
                {
                    Console.WriteLine($"[{enemy.Name}] ☠️  DÉTRUIT");
                }
            }
            Console.WriteLine();
        }

        static void Main()
        {
            SpaceInvaders spaceInvader = new SpaceInvaders();

            Console.WriteLine("========================================");
            Console.WriteLine("       SPACE INVADERS ");
            Console.WriteLine("========================================\n");

            Console.WriteLine($"Pilote : {player1}\n");
 
            Console.WriteLine("--- Armurerie ---\n");
            Armory.GetInstance().ViewArmory();

            Console.WriteLine("\n--- Vaisseau du joueur ---\n");
            player1.BattleShip.ViewShip();

            Console.WriteLine("\n--- Liste des ennemis ---");
            foreach(Spaceship enemy in spaceInvader.Ennemies)
            {
                Console.WriteLine($"- {enemy.Name} (Structure: {enemy.Structure}, Boucliers: {enemy.Shield})");
            }

            Console.WriteLine("\n\nAppuyez sur une touche pour commencer la bataille...");
            Console.ReadKey();
            Console.Clear();

            // Boucle de jeu principale
            int roundNumber = 1;
            while (!player1.BattleShip.IsDestroyed && spaceInvader.GetAliveEnemiesCount() > 0)
            {
                Console.WriteLine($"\n╔════════════════════════════════════╗");
                Console.WriteLine($"║         TOUR {roundNumber}                  ║");
                Console.WriteLine($"╚════════════════════════════════════╝");
                
                spaceInvader.PlayRound();
                roundNumber++;

                if (!player1.BattleShip.IsDestroyed && spaceInvader.GetAliveEnemiesCount() > 0)
                {
                    Console.WriteLine("Appuyez sur une touche pour le prochain tour...");
                    Console.ReadKey();
                }
            }

            // Afficher le résultat final
            Console.WriteLine("\n========================================");
            Console.WriteLine("          FIN DE LA BATAILLE");
            Console.WriteLine("========================================\n");

            if (player1.BattleShip.IsDestroyed)
            {
                Console.WriteLine("DÉFAITE ! Votre vaisseau a été détruit...");
                Console.WriteLine($"Ennemis restants : {spaceInvader.GetAliveEnemiesCount()}");
            }
            else
            {
                Console.WriteLine("VICTOIRE ! Tous les ennemis ont été éliminés !");
                Console.WriteLine($"État final de votre vaisseau :");
                Console.WriteLine($"Structure: {player1.BattleShip.CurrentStructure:F1}/{player1.BattleShip.Structure}");
                Console.WriteLine($"Boucliers: {player1.BattleShip.CurrentShield:F1}/{player1.BattleShip.Shield}");
            }

            Console.WriteLine("\nAppuyez sur une touche pour quitter...");
            Console.ReadKey();
        }
    }
}
