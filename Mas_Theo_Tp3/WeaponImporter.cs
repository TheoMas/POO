using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mas_Theo_Tp3
{
    internal class WeaponImporter
    {
        Dictionary<string, int> WordCountInFile = new Dictionary<string, int>();
        string path;
        int minTaille = 5;
        List<string> blacklist = new List<string>();
        string file;

        public WeaponImporter(string newPath, int newMinTaille, List<string> newBlacklist)
        {
            path = newPath;
            file = File.ReadAllText(path);
            minTaille = newMinTaille;
            blacklist = newBlacklist;

        }

        public void Import()
        {
            Random random = new Random();
            #region Remplissage du dictionnaire
            string[] fileWords = file.Split(';');
            
            foreach(string word in fileWords)
            {
                string normalizedWord = NormalizeWord(word);
                if (VerifWord(normalizedWord))
                {
                    if (WordCountInFile.ContainsKey(normalizedWord))
                    {
                        WordCountInFile[normalizedWord]++;
                    }
                    else
                    {
                        WordCountInFile.Add(normalizedWord, 1);
                    }
                }
            }
            #endregion

            #region Création de l'arme
            foreach (KeyValuePair<string, int> word in WordCountInFile)
            {
                if (VerifWord(word.Key))
                {
                    int maxDamage = 0;
                    int minDamage = 0;

                    #region Assignation des dégats
                    // Le plus grand entre la longueur du mot et son nombre de répétitions donne le max damage, l'autre donne le minDamage
                    if (word.Key.Length > word.Value)
                    {
                        maxDamage = word.Key.Length;
                        minDamage = word.Value;
                    } 
                    else if (word.Value > word.Key.Length)
                    {
                        maxDamage = word.Value;
                        minDamage = word.Key.Length;
                    }
                    else
                    {
                        maxDamage = word.Value;
                        minDamage = word.Value;
                    }
                    #endregion

                    Armory.GetInstance().AddWeapon(new Weapons(word.Key, minDamage, maxDamage, GetRandomWeaponType(random), 2));
                }
            }
            #endregion
        }

        private Weapons.EWeaponType GetRandomWeaponType(Random random)
        {
            Array values = Enum.GetValues(typeof(Weapons.EWeaponType));
            Weapons.EWeaponType randomWeaponType = (Weapons.EWeaponType)values?.GetValue(random.Next(values.Length));
            return randomWeaponType;
        }

        private string NormalizeWord(string word)
        {
            #region Verif NULL
            if (string.IsNullOrEmpty(word))
                return word;
            #endregion

            #region Normalisation du mot
            // Trim pour enlever les espaces et sauts de ligne au début/fin
            string normalizedWord = word.Trim();
            
            // Première lettre en majuscule, reste en minuscule
            normalizedWord = char.ToUpper(normalizedWord[0]) + normalizedWord.Substring(1).ToLower();
            
            // Retirer la ponctuation
            normalizedWord = new string(normalizedWord.Where(c => !char.IsPunctuation(c)).ToArray());
            
            // Retirer les sauts de ligne et espaces restants
            normalizedWord = normalizedWord.Replace("\n", "").Replace("\r", "").Replace(" ", "");
            #endregion

            return normalizedWord;
        }

        private bool VerifWord(string wordToVerif)
        {
            #region Vérification de la blacklist et de la taille
            if (wordToVerif != null 
                && wordToVerif.Length > minTaille
                && !blacklist.Contains(wordToVerif))
            {
                return true;
            }
            return false;
            #endregion
        }
    }
}
