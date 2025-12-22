using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    internal class SpaceInvaders
    {
        public static Player player1 { get; private set; }
        public List<Spaceship> ennemies { get; private set; }
        private static Random rand = new Random();

        public SpaceInvaders(Player player) 
        {
            player1 = player;
            ennemies = new List<Spaceship>();
            InitEnemies();
        }

        private void InitEnemies()
        {
            //init des vaisseaux ennemis
            ennemies.Add(new Dart("Dart I"));
            ennemies.Add(new BWing("BWing I"));
            ennemies.Add(new Rocinante("Rocinante I"));
            ennemies.Add(new F18("F18 I"));
            ennemies.Add(new Tardis("Tardis I"));
        }

        private void PlayRound()
        {
            #region Gestion du tour
            Console.WriteLine("\n========================================");
            Console.WriteLine("               NOUVEAU TOUR"              );
            Console.WriteLine("========================================\n");

            RegenerateShields();

            int playerShootOrder = rand.Next(0, GetAliveEnemiesCount() + 1);

            int enemyIndex = 0;
            bool playerHasShot = false;

            foreach (Spaceship enemy in ennemies)
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
            #endregion
        }

        private void RegenerateShields()
        {
            #region Gestion de la régénération des boucliers
            Console.WriteLine("--- Régénération des boucliers ---");

            if (player1.BattleShip.CurrentShield < player1.BattleShip.Shield)
            {
                double oldShield = player1.BattleShip.CurrentShield;
                player1.BattleShip.RepairShield(2);
                Console.WriteLine($"[{player1.BattleShip.Name}] Boucliers : {oldShield:F1} -> {player1.BattleShip.CurrentShield:F1}");
            }

            foreach (Spaceship enemy in ennemies)
            {
                if (!enemy.IsDestroyed && enemy.CurrentShield < enemy.Shield)
                {
                    double oldShield = enemy.CurrentShield;
                    enemy.RepairShield(2);
                    Console.WriteLine($"[{enemy.Name}] Boucliers : {oldShield:F1} -> {enemy.CurrentShield:F1}");
                }
            }
            Console.WriteLine();
            #endregion
        }

        #region Gestion des tirs
        private void PlayerShoot()
        {
            #region Tir du joueur
            List<Spaceship> aliveEnemies = ennemies.Where(e => !e.IsDestroyed).ToList();
            
            if (aliveEnemies.Count == 0)
                return;

            Spaceship target = aliveEnemies[rand.Next(aliveEnemies.Count)];

            Console.WriteLine($"\n[{player1.Alias}] tire sur [{target.Name}] !");
            
            double targetShieldBefore = target.CurrentShield;
            double targetStructureBefore = target.CurrentStructure;

            player1.BattleShip.ShootTarget(target);

            DisplayDamageInfo(player1.BattleShip, target, targetShieldBefore, targetStructureBefore);
            #endregion
        }
  
        private void EnemyShoot(Spaceship enemy)
        {
            #region Tir ennemi
            Console.WriteLine($"\n[{enemy.Name}] tire sur [{player1.BattleShip.Name}] !");
            
            double targetShieldBefore = player1.BattleShip.CurrentShield;
            double targetStructureBefore = player1.BattleShip.CurrentStructure;

            enemy.ShootTarget(player1.BattleShip);

            DisplayDamageInfo(enemy, player1.BattleShip, targetShieldBefore, targetStructureBefore);
            #endregion
        }
        #endregion

        private void DisplayDamageInfo(Spaceship attacker, Spaceship target, double shieldBefore, double structureBefore)
        {
            #region Affichage des dégats
            double shieldDamage = shieldBefore - target.CurrentShield;
            double structureDamage = structureBefore - target.CurrentStructure;
            double totalDamage = shieldDamage + structureDamage;

            Console.WriteLine($"   Dégâts infligés : {totalDamage:F1}");
            
            if (shieldDamage > 0)
                Console.WriteLine($"   Boucliers : {shieldBefore:F1} -> {target.CurrentShield:F1}");
            
            if (structureDamage > 0)
                Console.WriteLine($"   Structure : {structureBefore:F1} -> {target.CurrentStructure:F1}");

            if (target.CurrentStructure <= 0 && !target.IsDestroyed)
            {
                target.IsDestroyed = true;
                Console.WriteLine($"   💥 [{target.Name}] a été DÉTRUIT !");
            }
            #endregion
        }

        private void ReloadAllWeapons()
        {
            player1.BattleShip.ReloadWeapons();
            
            foreach (Spaceship enemy in ennemies)
            {
                if (!enemy.IsDestroyed)
                    enemy.ReloadWeapons();
            }
        }

        public int GetAliveEnemiesCount()
        {
            return ennemies.Count(e => !e.IsDestroyed);
        }

        private void DisplayRoundStatus()
        {
            #region Affichage du statut de fin de round
            Console.WriteLine("\n--- État des vaisseaux ---");
            Console.WriteLine($"[{player1.BattleShip.Name}] Structure: {player1.BattleShip.CurrentStructure:F1}/{player1.BattleShip.Structure} | Boucliers: {player1.BattleShip.CurrentShield:F1}/{player1.BattleShip.Shield}");
            
            foreach (Spaceship enemy in ennemies)
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
            #endregion
        }

        public static void StartGame(Player player)
        {
            #region Gestion du début de la partie
            SpaceInvaders spaceInvader = new SpaceInvaders(player);

            #region Affichage des informations de début de partie
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("             SPACE INVADERS             ");
            Console.WriteLine("========================================\n");

            Console.WriteLine($"Pilote : {player1}\n");

            Console.WriteLine("--- Vaisseau du joueur ---\n");
            player1.BattleShip.ViewShip();

            Console.WriteLine("\n--- Liste des ennemis ---");
            foreach(Spaceship enemy in spaceInvader.ennemies)
            {
                Console.WriteLine($"- {enemy.Name} (Structure: {enemy.Structure}, Boucliers: {enemy.Shield})");
            }

            Console.WriteLine("\n\nAppuyez sur une touche pour commencer la bataille...");
            Console.ReadKey();
            Console.Clear();
            #endregion

            #region Boucle de jeu principale
            int roundNumber = 1;
            while (!player1.BattleShip.IsDestroyed && spaceInvader.GetAliveEnemiesCount() > 0)
            {
                Console.WriteLine($"\n╔════════════════════════════════════╗");
                Console.WriteLine($"║         TOUR {roundNumber,-2}                  ║");
                Console.WriteLine($"╚════════════════════════════════════╝");
                
                spaceInvader.PlayRound();
                roundNumber++;

                if (!player1.BattleShip.IsDestroyed && spaceInvader.GetAliveEnemiesCount() > 0)
                {
                    Console.WriteLine("Appuyez sur une touche pour le prochain tour...");
                    Console.ReadKey();
                }
            }
            #endregion

            #region Afficher le résultat final
            Console.WriteLine("\n========================================");
            Console.WriteLine("          FIN DE LA BATAILLE");
            Console.WriteLine("========================================\n");

            if (player1.BattleShip.IsDestroyed)
            {
                Console.WriteLine("💀 DÉFAITE ! Votre vaisseau a été détruit...");
                Console.WriteLine($"Ennemis restants : {spaceInvader.GetAliveEnemiesCount()}");
            }
            else
            {
                Console.WriteLine("🎉 VICTOIRE ! Tous les ennemis ont été éliminés !");
                Console.WriteLine($"État final de votre vaisseau :");
                Console.WriteLine($"Structure: {player1.BattleShip.CurrentStructure:F1}/{player1.BattleShip.Structure}");
                Console.WriteLine($"Boucliers: {player1.BattleShip.CurrentShield:F1}/{player1.BattleShip.Shield}");
            }

            // ✅ RÉPARATION DU VAISSEAU À LA FIN DE LA PARTIE
            Console.WriteLine("\n🔧 Réparation du vaisseau en cours...");
            player1.BattleShip.FullRepair();
            Console.WriteLine("✅ Vaisseau complètement réparé et prêt pour la prochaine bataille !");
            Console.WriteLine($"   Structure: {player1.BattleShip.CurrentStructure}/{player1.BattleShip.Structure}");
            Console.WriteLine($"   Boucliers: {player1.BattleShip.CurrentShield}/{player1.BattleShip.Shield}");
            Console.WriteLine($"   Toutes les armes sont rechargées.");
            #endregion

            Console.WriteLine("\nAppuyez sur une touche pour retourner au menu...");
            Console.ReadKey();
            #endregion
        }


        static void Main(string[] args)
        {
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         SPACE INVADERS - INIT          ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            // Initialisation de l'armurerie de base
            Armory.GetInstance().AddWeapon(new Weapons("ASTEROHACHE", 3, 100, Weapons.EWeaponType.Explosive, 4));

            // Gestion des arguments pour l'import de fichier
            if (args.Length > 0)
            {
                string filePath = args[0];
                int minSize = args.Length > 1 && int.TryParse(args[1], out int size) ? size : 3;
                
                List<string> blacklist = new List<string>();
                if (args.Length > 2)
                {
                    blacklist = args[2].Split(',').Select(s => s.Trim()).ToList();
                }

                try
                {
                    Console.WriteLine($"Importation des armes depuis : {filePath}");
                    WeaponImporter importer = new WeaponImporter(filePath, minSize, blacklist);
                    importer.Import();
                    Console.WriteLine("✅ Armes importées avec succès !\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Erreur lors de l'importation : {ex.Message}\n");
                }
            }

            // Initialisation du joueur par défaut
            MenuManager.Initialize();

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();

            // Lancement du menu principal
            MenuManager.ShowMainMenu();
        }
    }
}
