using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TP0_MAS_Théo
{
    public class Tp0
    {

        static string FuseNames(string prenom, string nom)
        {
            return prenom.ToLower() + " " + nom.ToUpper();
        }

        static float CalculateIMC(int poids, int taille)
        {
            float tailleEnMetres = Convert.ToSingle(taille) / 100;
            return poids / (tailleEnMetres * tailleEnMetres);
        }

        static void CommentIMC(float IMC)
        {
            const string COM_1 = "Attention a l'anorexie !";
            const string COM_2 = "Vous êtes un peu maigrichon!";
            const string COM_3 = "Vous êtes de corpulence normale !";
            const string COM_4 = "Vous êtes en surpoids !";
            const string COM_5 = "Obésité modérée !";
            const string COM_6 = "Obésité sévère !";
            const string COM_7 = "Obésité morbide !";

            if (IMC < 16.5f)
                Console.WriteLine(COM_1);
            else if (IMC < 18.5f)
                Console.WriteLine(COM_2);
            else if (IMC < 25f)
                Console.WriteLine(COM_3);
            else if (IMC < 30f)
                Console.WriteLine(COM_4);
            else if (IMC < 35f)
                Console.WriteLine(COM_5);
            else if (IMC < 40f)
                Console.WriteLine(COM_6);
            else
                Console.WriteLine(COM_7);
        }

        static void CheckNbOfHair()
        {
            bool i = true;
            int nbCheveux = 0;
            Console.WriteLine("Combien de cheveux as-tu sur la tête ? : ");
            while (!(int.TryParse(Console.ReadLine(), out nbCheveux) == true && nbCheveux > 100000))
            {
                Console.WriteLine("Erreur de saisie, recommence ! (indice : il te faut plus de 100 000 cheveux, si t'es chauve, tu passe pas !)");
                Console.WriteLine("Combien de cheveux as-tu sur la tête ? : ");
            }
            Console.WriteLine("Tu as : " + nbCheveux + " cheveux sur la tête !");
        }

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Hello World!");
                Console.WriteLine("Puis-je avoir ton prénom, sacré plaisantin ? : ");
                string prenom = Console.ReadLine();
                while (prenom.Any(char.IsDigit))
                {
                    Console.WriteLine("Erreur de saisie, recommence ! (indice : y'a pas de chiffre dans un prénom)");
                    Console.WriteLine("Puis-je avoir ton prénom, sacré plaisantin ? : ");
                    prenom = Console.ReadLine();
                }

                Console.WriteLine("Il me faudrai aussi ton nom, vil malandrin : ");
                string nom = Console.ReadLine();
                while (nom.Any(char.IsDigit))
                {
                    Console.WriteLine("Erreur de saisie, recommence ! (indice : y'a pas de chiffre dans un nom)");
                    Console.WriteLine("Il me faudrai aussi ton nom, vil malandrin : ");
                    nom = Console.ReadLine();
                }

                Console.WriteLine("Enchanté " + FuseNames(prenom, nom) + " !");

                Console.WriteLine("Quel est ton age ? : ");
                int age = Convert.ToInt32(Console.ReadLine());
                while (age < 0)
                {
                    Console.WriteLine("Erreur de saisie, recommence ! (indice : l'age est un nombre positif)");
                    Console.WriteLine("Quel est ton age ? : ");
                    age = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("Quelle est ta taille (en cm) ? : ");
                int taille = Convert.ToInt32(Console.ReadLine());
                while (taille < 0)
                {
                    Console.WriteLine("Erreur de saisie, recommence ! (indice : la taille est un nombre positif)");
                    Console.WriteLine("Quelle est ta taille (en cm) ? : ");
                    taille = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("Quel est ton poids (en kg) ? : ");
                int poids = Convert.ToInt32(Console.ReadLine());
                while (poids < 0)
                {
                    Console.WriteLine("Erreur de saisie, recommence ! (indice : le poids est un nombre positif)");
                    Console.WriteLine("Quel est ton poids (en kg) ? : ");
                    poids = Convert.ToInt32(Console.ReadLine());
                }

                Console.WriteLine("Quelle est ton année de naissance ? : ");
                int anneeNaissance = Convert.ToInt32(Console.ReadLine());

                if ((DateTime.Now.Year - anneeNaissance) < 18)
                {
                    Console.WriteLine("Tu es mineur, tu peux même pas te bourrer la tronche un samedi soir, rentre chez toi, espèce de petit nabot!");
                }
                else
                {
                    Console.WriteLine("Tu es majeur, tu peux te bourrer la tronche un samedi soir, mais fais gaffe à pas finir comme un sac à patates dans le caniveau, hein!");
                }

                Console.WriteLine("Ton IMC est de : " + CalculateIMC(poids, taille).ToString("0.0"));

                CommentIMC(CalculateIMC(poids, taille));

                CheckNbOfHair();

                Console.WriteLine("Que veux-tu faire maintenant ? (1 : Quitter, 2 : Recommencer, 3 : Compter jusqu'a 10, 4 : Appeler Tata Jacqueline) : ");
                int switcher;
                while (!((int.TryParse(Console.ReadLine(), out switcher) == true)
                    && switcher > 0
                    && switcher < 4))
                {
                    Console.WriteLine("Erreur de saisie, recommence ! (indice : il faut écrire un nombre entre 1 et 4 !)");
                    Console.WriteLine("Que veux-tu faire maintenant ? (1 : Quitter, 2 : Recommencer, 3 : Compter jusqu'a 10, 4 : Appeler Tata Jacqueline) : ");
                }
                switch (switcher)
                {
                    case 1:
                        Console.WriteLine("Au revoir !");
                        Thread.Sleep(3000);
                        return;
                    case 2:
                        Console.WriteLine("Et c'est reparti !");
                        break;
                    case 3:
                        for (int i = 1; i <= 10; i++)
                        {
                            Console.WriteLine(i);
                            Thread.Sleep(1000);
                        }
                        Console.WriteLine("Au revoir !");
                        Thread.Sleep(3000);
                        return;
                    case 4:
                        for (int i = 1; i <= 3; i++)
                        {
                            Console.WriteLine("BIP");
                            Thread.Sleep(1000);
                        }
                        Console.WriteLine("Bonjour, c'est le répondeur de Tata Jacqueline, je ne suis pas là pour le moment, j'ai mon cours de Zumba WaterPolo ! Laisse un message après le bip !");
                        Thread.Sleep(1000);
                        Console.WriteLine("BIP");
                        Thread.Sleep(3000);
                        Console.WriteLine("Au revoir !");
                        Thread.Sleep(3000);
                        return;
                }

            }
        }
    }
}
