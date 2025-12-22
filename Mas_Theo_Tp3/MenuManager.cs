using System;
using System.Collections.Generic;
using System.Linq;

namespace Mas_Theo_Tp3
{
    internal static class MenuManager
    {
        private static List<Player> players = new List<Player>();
        private static Player? currentPlayer = null;

        #region Méthode d'initialisation pour créer le joueur par défaut
        public static void Initialize()
        {
            // Création du joueur par défaut : Théo MAS (TMA) avec le vaisseau "GOAT"
            ViperMKII defaultShip = new ViperMKII("GOAT");
            Player defaultPlayer = new Player("Théo", "MAS", "TMA", defaultShip);
            players.Add(defaultPlayer);
            currentPlayer = defaultPlayer;

            Console.WriteLine(" Joueur par défaut créé et sélectionné :");
            Console.WriteLine($"   Pilote : {defaultPlayer.Name} ({defaultPlayer.Alias})");
            Console.WriteLine($"   Vaisseau : {defaultPlayer.BattleShip.Name}\n");
        }
        #endregion

        #region Menu Principal
        public static void ShowMainMenu()
        {
            bool exit = false;
            while (!exit)
            {
                #region Affichage menu
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║     SPACE INVADERS - MENU PRINCIPAL    ║");
                Console.WriteLine("╚════════════════════════════════════════╝");

                // Affichage du joueur actuel
                if (currentPlayer != null)
                {
                    Console.WriteLine($"\n🎮 Joueur actuel : {currentPlayer.Name} ({currentPlayer.Alias})");
                    Console.WriteLine($"   Vaisseau : {currentPlayer.BattleShip.Name}");
                }
                
                Console.WriteLine("\n1. Gestion des joueurs");
                Console.WriteLine("2. Gestion du vaisseau");
                Console.WriteLine("3. Gestion de l'armurerie");
                Console.WriteLine("4. Voir les statistiques des armes");
                Console.WriteLine("5. Lancer la partie");
                Console.WriteLine("6. Quitter");
                Console.Write("\nVotre choix : ");
                #endregion

                #region Switch de gestion du menu
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ShowPlayerManagementMenu();
                        break;
                    case "2":
                        ShowSpaceshipManagementMenu();
                        break;
                    case "3":
                        ShowArmoryManagementMenu();
                        break;
                    case "4":
                        ShowWeaponStatistics();
                        break;
                    case "5":
                        if (currentPlayer != null)
                        {
                            SpaceInvaders.StartGame(currentPlayer);
                        }
                        else
                        {
                            Console.WriteLine("\n❌ Aucun joueur sélectionné ! Veuillez créer et choisir un joueur.");
                            Console.WriteLine("Appuyez sur une touche pour continuer...");
                            Console.ReadKey();
                        }
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("\n❌ Choix invalide !");
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                        break;
                }
                #endregion
            }
        }
        #endregion

        #region Menu Gestion des Joueurs
        private static void ShowPlayerManagementMenu()
        {
            bool back = false;
            while (!back)
            {
                #region Affichage menu de gestion des joueurs
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║           GESTION DES JOUEURS          ║");
                Console.WriteLine("╚════════════════════════════════════════╝\n");
                
                if (currentPlayer != null)
                    Console.WriteLine($"Joueur actuel : {currentPlayer.Name} ({currentPlayer.Alias})\n");
                else
                    Console.WriteLine("Aucun joueur sélectionné\n");

                Console.WriteLine("1. Créer un joueur");
                Console.WriteLine("2. Supprimer un joueur");
                Console.WriteLine("3. Choisir le joueur courant");
                Console.WriteLine("4. Voir tous les joueurs");
                Console.WriteLine("5. Retour");
                Console.Write("\nVotre choix : ");
                #endregion

                #region Switch menu de gestion des joueurs
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreatePlayer();
                        break;
                    case "2":
                        DeletePlayer();
                        break;
                    case "3":
                        SelectCurrentPlayer();
                        break;
                    case "4":
                        ViewAllPlayers();
                        break;
                    case "5":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\n❌ Choix invalide !");
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                        break;
                }
                #endregion
            }
        }

        #region Création d'un joueur
        private static void CreatePlayer()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║          CRÉATION D'UN JOUEUR          ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.Write("Prénom : ");
            string? firstName = Console.ReadLine();
            
            Console.Write("Nom : ");
            string? lastName = Console.ReadLine();
            
            Console.Write("Alias : ");
            string? alias = Console.ReadLine();
            
            Console.Write("Nom du vaisseau : ");
            string? shipName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || 
                string.IsNullOrWhiteSpace(alias) || string.IsNullOrWhiteSpace(shipName))
            {
                Console.WriteLine("\n❌ Tous les champs sont obligatoires !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            ViperMKII ship = new ViperMKII(shipName);
            Player newPlayer = new Player(firstName, lastName, alias, ship);
            players.Add(newPlayer);

            Console.WriteLine($"\n✅ Joueur {newPlayer.Name} créé avec succès !");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #region Suppression d'un joueur
        private static void DeletePlayer()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         SUPPRESSION D'UN JOUEUR        ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            if (players.Count == 0)
            {
                Console.WriteLine("❌ Aucun joueur disponible !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i].Name} ({players[i].Alias})");
            }

            Console.Write("\nNuméro du joueur à supprimer (0 pour annuler) : ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= players.Count)
            {
                Player toRemove = players[choice - 1];
                if (currentPlayer == toRemove)
                    currentPlayer = null;
                
                players.RemoveAt(choice - 1);
                Console.WriteLine($"\n✅ Joueur {toRemove.Name} supprimé !");
            }
            else if (choice != 0)
            {
                Console.WriteLine("\n❌ Choix invalide !");
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #region Sélection d'un joueur
        private static void SelectCurrentPlayer()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║          SÉLECTION DU JOUEUR           ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            if (players.Count == 0)
            {
                Console.WriteLine("❌ Aucun joueur disponible ! Créez un joueur d'abord.");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i].Name} ({players[i].Alias}) - Vaisseau: {players[i].BattleShip.Name}");
            }

            Console.Write("\nNuméro du joueur à sélectionner (0 pour annuler) : ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= players.Count)
            {
                currentPlayer = players[choice - 1];
                Console.WriteLine($"\n✅ Joueur {currentPlayer.Name} sélectionné !");
            }
            else if (choice != 0)
            {
                Console.WriteLine("\n❌ Choix invalide !");
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #region Affichage de la liste des joueurs
        private static void ViewAllPlayers()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║           LISTE DES JOUEURS            ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            if (players.Count == 0)
            {
                Console.WriteLine("❌ Aucun joueur disponible !");
            }
            else
            {
                foreach (var player in players)
                {
                    Console.WriteLine($"- {player.Name} ({player.Alias})");
                    Console.WriteLine($"  Vaisseau: {player.BattleShip.Name}");
                    Console.WriteLine($"  Structure: {player.BattleShip.CurrentStructure}/{player.BattleShip.Structure}");
                    Console.WriteLine($"  Boucliers: {player.BattleShip.CurrentShield}/{player.BattleShip.Shield}");
                    Console.WriteLine($"  Armes: {player.BattleShip.SpaceshipWeapons.Count}\n");
                }
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion
        #endregion

        #region Menu Gestion du Vaisseau
        private static void ShowSpaceshipManagementMenu()
        {
            #region Gestion Joueur Courant
            if (currentPlayer == null)
            {
                Console.Clear();
                Console.WriteLine("❌ Aucun joueur sélectionné ! Veuillez d'abord créer et choisir un joueur.");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }
            #endregion

            
            bool back = false;
            while (!back)
            {
                #region Affichage Menu de Gestion du Vaisseau
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║           GESTION DU VAISSEAU          ║");
                Console.WriteLine("╚════════════════════════════════════════╝\n");
                Console.WriteLine($"Vaisseau : {currentPlayer.BattleShip.Name}");
                Console.WriteLine($"Armes équipées : {currentPlayer.BattleShip.SpaceshipWeapons.Count}/{currentPlayer.BattleShip.MaxWeapons}\n");

                Console.WriteLine("1. Ajouter une arme");
                Console.WriteLine("2. Supprimer une arme");
                Console.WriteLine("3. Voir les armes du vaisseau");
                Console.WriteLine("4. Retour");
                Console.Write("\nVotre choix : ");
                #endregion

                #region Switch du menu de gestion du vaiseau
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddWeaponToShip();
                        break;
                    case "2":
                        RemoveWeaponFromShip();
                        break;
                    case "3":
                        ViewShipWeapons();
                        break;
                    case "4":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\n❌ Choix invalide !");
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                        break;
                }
                #endregion
            }
        }

        #region Ajout d'une arme sur le vaisseau
        private static void AddWeaponToShip()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║            AJOUTER UNE ARME            ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            if (currentPlayer!.BattleShip.SpaceshipWeapons.Count >= currentPlayer.BattleShip.MaxWeapons)
            {
                Console.WriteLine($"❌ Le vaisseau a déjà atteint le nombre maximum d'armes ({currentPlayer.BattleShip.MaxWeapons}) !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            var armory = Armory.GetInstance().GetWeaponList();
            if (armory.Count == 0)
            {
                Console.WriteLine("❌ Aucune arme disponible dans l'armurerie !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Armes disponibles :\n");
            for (int i = 0; i < armory.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {armory[i].Name} (Dégâts: {armory[i].MinDamage}-{armory[i].MaxDamage}, Type: {armory[i].Type})");
            }

            Console.Write("\nNuméro de l'arme à ajouter (0 pour annuler) : ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= armory.Count)
            {
                try
                {
                    currentPlayer.BattleShip.AddWeapon(armory[choice - 1]);
                    Console.WriteLine($"\n✅ Arme {armory[choice - 1].Name} ajoutée au vaisseau !");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Erreur : {ex.Message}");
                }
            }
            else if (choice != 0)
            {
                Console.WriteLine("\n❌ Choix invalide !");
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #region Suppression d'une arme du vaisseaus
        private static void RemoveWeaponFromShip()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║          SUPPRIMER UNE ARME            ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            var weapons = currentPlayer!.BattleShip.SpaceshipWeapons;
            if (weapons.Count == 0)
            {
                Console.WriteLine("❌ Le vaisseau n'a aucune arme !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Armes du vaisseau :\n");
            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {weapons[i].Name} (Dégâts: {weapons[i].MinDamage}-{weapons[i].MaxDamage})");
            }

            Console.Write("\nNuméro de l'arme à supprimer (0 pour annuler) : ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= weapons.Count)
            {
                var weaponToRemove = weapons[choice - 1];
                currentPlayer.BattleShip.RemoveWeapon(weaponToRemove);
                Console.WriteLine($"\n✅ Arme {weaponToRemove.Name} retirée du vaisseau !");
            }
            else if (choice != 0)
            {
                Console.WriteLine("\n❌ Choix invalide !");
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #region Affichage des armes du vaisseau
        private static void ViewShipWeapons()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║            ARMES DU VAISSEAU           ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.WriteLine($"Vaisseau : {currentPlayer!.BattleShip.Name}\n");
            currentPlayer.BattleShip.ViewWeapons();

            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #endregion

        #region Menu Gestion de l'Armurerie
        private static void ShowArmoryManagementMenu()
        {
            bool back = false;
            while (!back)
            {
                #region Affichage du menu de gestion de l'armurerie
                Console.Clear();
                Console.WriteLine("╔════════════════════════════════════════╗");
                Console.WriteLine("║          GESTION DE L'ARMURERIE        ║");
                Console.WriteLine("╚════════════════════════════════════════╝\n");
                Console.WriteLine($"Nombre d'armes : {Armory.GetInstance().GetWeaponList().Count}\n");

                Console.WriteLine("1. Ajouter une arme");
                Console.WriteLine("2. Modifier une arme");
                Console.WriteLine("3. Supprimer une arme");
                Console.WriteLine("4. Créer des armes depuis un fichier");
                Console.WriteLine("5. Voir toutes les armes");
                Console.WriteLine("6. Retour");
                Console.Write("\nVotre choix : ");
                #endregion

                #region Switch du menu de gestion de l'armurerie
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddWeaponToArmory();
                        break;
                    case "2":
                        ModifyWeapon();
                        break;
                    case "3":
                        RemoveWeaponFromArmory();
                        break;
                    case "4":
                        ImportWeaponsFromFile();
                        break;
                    case "5":
                        ViewArmory();
                        break;
                    case "6":
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\n❌ Choix invalide !");
                        Console.WriteLine("Appuyez sur une touche pour continuer...");
                        Console.ReadKey();
                        break;
                }
                #endregion
            }
        }

        #region Ajout d'une arme a l'armurerie
        private static void AddWeaponToArmory()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║            AJOUTER UNE ARME            ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.Write("Nom de l'arme : ");
            string? name = Console.ReadLine();

            Console.Write("Dégâts minimum : ");
            if (!int.TryParse(Console.ReadLine(), out int minDamage))
            {
                Console.WriteLine("\n❌ Valeur invalide !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            Console.Write("Dégâts maximum : ");
            if (!int.TryParse(Console.ReadLine(), out int maxDamage))
            {
                Console.WriteLine("\n❌ Valeur invalide !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nType d'arme :");
            Console.WriteLine("1. Direct");
            Console.WriteLine("2. Explosive");
            Console.WriteLine("3. Guided");
            Console.Write("Choix : ");
            
            Weapons.EWeaponType type = Weapons.EWeaponType.Direct;
            if (int.TryParse(Console.ReadLine(), out int typeChoice))
            {
                type = typeChoice switch
                {
                    1 => Weapons.EWeaponType.Direct,
                    2 => Weapons.EWeaponType.Explosive,
                    3 => Weapons.EWeaponType.Guided,
                    _ => Weapons.EWeaponType.Direct
                };
            }

            Console.Write("Temps de rechargement : ");
            if (!double.TryParse(Console.ReadLine(), out double reloadTime))
            {
                Console.WriteLine("\n❌ Valeur invalide !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("\n❌ Le nom est obligatoire !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            Weapons newWeapon = new Weapons(name, minDamage, maxDamage, type, reloadTime);
            Armory.GetInstance().AddWeapon(newWeapon);

            Console.WriteLine($"\n✅ Arme {name} ajoutée à l'armurerie !");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #region Modifier une arme de l'armurerie
        private static void ModifyWeapon()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║           MODIFIER UNE ARME            ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            var weapons = Armory.GetInstance().GetWeaponList();
            if (weapons.Count == 0)
            {
                Console.WriteLine("❌ Aucune arme disponible !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {weapons[i].Name} (Dégâts: {weapons[i].MinDamage}-{weapons[i].MaxDamage}, Type: {weapons[i].Type}, Reload: {weapons[i].ReloadTime})");
            }

            Console.Write("\nNuméro de l'arme à modifier (0 pour annuler) : ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > weapons.Count)
            {
                if (choice != 0)
                    Console.WriteLine("\n❌ Choix invalide !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            Weapons weaponToModify = weapons[choice - 1];
            Console.WriteLine($"\nModification de l'arme : {weaponToModify.Name}");
            Console.WriteLine("(Laissez vide pour conserver la valeur actuelle)\n");

            Console.Write($"Nouveau nom [{weaponToModify.Name}] : ");
            string? newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName))
                weaponToModify.Name = newName;

            Console.Write($"Nouveaux dégâts minimum [{weaponToModify.MinDamage}] : ");
            string? minDamageStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(minDamageStr) && int.TryParse(minDamageStr, out int newMinDamage))
                weaponToModify.MinDamage = newMinDamage;

            Console.Write($"Nouveaux dégâts maximum [{weaponToModify.MaxDamage}] : ");
            string? maxDamageStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(maxDamageStr) && int.TryParse(maxDamageStr, out int newMaxDamage))
                weaponToModify.MaxDamage = newMaxDamage;

            Console.Write($"Nouveau temps de rechargement [{weaponToModify.ReloadTime}] : ");
            string? reloadTimeStr = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(reloadTimeStr) && double.TryParse(reloadTimeStr, out double newReloadTime))
                weaponToModify.ReloadTime = newReloadTime;

            Console.WriteLine($"\n✅ Arme {weaponToModify.Name} modifiée !");
            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #region Suppression d'une arme de l'armurerie
        private static void RemoveWeaponFromArmory()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║          SUPPRIMER UNE ARME            ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            var weapons = Armory.GetInstance().GetWeaponList();
            if (weapons.Count == 0)
            {
                Console.WriteLine("❌ Aucune arme disponible !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {weapons[i].Name}");
            }

            Console.Write("\nNuméro de l'arme à supprimer (0 pour annuler) : ");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= weapons.Count)
            {
                var weaponToRemove = weapons[choice - 1];
                Armory.GetInstance().RemoveWeapon(weaponToRemove);
                Console.WriteLine($"\n✅ Arme {weaponToRemove.Name} supprimée !");
            }
            else if (choice != 0)
            {
                Console.WriteLine("\n❌ Choix invalide !");
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #region Import d'armes d'un fichier
        private static void ImportWeaponsFromFile()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║          IMPORTER DES ARMES            ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Console.Write("Chemin du fichier : ");
            string? filePath = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine("\n❌ Fichier introuvable !");
                Console.WriteLine("Appuyez sur une touche pour continuer...");
                Console.ReadKey();
                return;
            }

            Console.Write("Taille minimum des mots (ex: 3) : ");
            if (!int.TryParse(Console.ReadLine(), out int minSize))
                minSize = 3;

            Console.Write("Mots à exclure (séparés par des virgules) : ");
            string? blacklistStr = Console.ReadLine();
            List<string> blacklist = string.IsNullOrWhiteSpace(blacklistStr) 
                ? new List<string>() 
                : blacklistStr.Split(',').Select(s => s.Trim()).ToList();

            try
            {
                WeaponImporter importer = new WeaponImporter(filePath, minSize, blacklist);
                int countBefore = Armory.GetInstance().GetWeaponList().Count;
                importer.Import();
                int countAfter = Armory.GetInstance().GetWeaponList().Count;
                
                Console.WriteLine($"\n✅ {countAfter - countBefore} armes importées avec succès !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Erreur lors de l'importation : {ex.Message}");
            }

            Console.WriteLine("Appuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #region Affichage de l'armurerie
        private static void ViewArmory()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║           ARMURERIE COMPLÈTE           ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            Armory.GetInstance().ViewArmory();

            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion

        #endregion

        #region Statistiques des Armes
        private static void ShowWeaponStatistics()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║         STATISTIQUES DES ARMES         ║");
            Console.WriteLine("╚════════════════════════════════════════╝\n");

            #region Top 5 dégâts moyens
            Console.WriteLine("🏆 TOP 5 - Dégâts moyens les plus élevés :\n");
            List<Weapons> topAverage = Armory.GetInstance().GetBestAverageDamages();
            int rank = 1;
            foreach (Weapons weapon in topAverage)
            {
                Console.WriteLine($"{rank}. {weapon.Name} - Dégâts moyens: {weapon.AverageDamage:F2}");
                rank++;
            }
            #endregion

            Console.WriteLine("\n════════════════════════════════════════\n");

            #region Top 5 dégâts minimums
            Console.WriteLine("🏆 TOP 5 - Dégâts minimums les plus élevés :\n");
            List<Weapons> topMin = Armory.GetInstance().GetBestMinDamages();
            rank = 1;
            foreach (Weapons weapon in topMin)
            {
                Console.WriteLine($"{rank}. {weapon.Name} - Dégâts min: {weapon.MinDamage}");
                rank++;
            }
            #endregion

            Console.WriteLine("\n════════════════════════════════════════\n");

            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }
        #endregion
    }
}