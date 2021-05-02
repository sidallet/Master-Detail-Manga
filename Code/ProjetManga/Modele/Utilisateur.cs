﻿using System;
using System.Collections.Generic;
using System.Text;


/// A FINIR : voir sur le diagramme de classe, ??? signifie methodes peut etre inutiles ? 
/// Methode "ModifierProfil" inutile ? les set des proprietes suffisent 

namespace Modele
{
    public class Utilisateur
    {
        public List<Manga> LesFavoris;
        public string Pseudo { get; private set; }

        public int Age { get; private set; }

        public DateTime DateInscription { get; private set; }

        public string MotDePasse { get; private set; }

        public GenreDispo GenresPreferes { get; private set; }

        public Utilisateur(string pseudo, int age, DateTime dateInscription, GenreDispo genrepref, string motDePasse)
        {
            Pseudo = pseudo ?? throw new ArgumentNullException(nameof(pseudo));
            Age = age;
            DateInscription = dateInscription;
            MotDePasse = motDePasse ?? throw new ArgumentNullException(nameof(motDePasse));
            GenresPreferes = genrepref;

            LesFavoris = new List<Manga>();

        }

        public override string ToString() ///testé
        {
            string r;
            r = $"[Utilisateur] {Pseudo} / {Age} / {DateInscription} /  / *{MotDePasse}*\n "; //{GenresPreferes.GetDescription()}
            if (LesFavoris != null)
            {
                r += "Liste des favoris : \n";
                foreach (Manga m in LesFavoris)
                {
                    r += "\t\t" + m.TitreOriginal;
                }
            }
            return r;
        }

        public override bool Equals(object obj)
        {
            if (Pseudo == ((Utilisateur)obj).Pseudo && MotDePasse == ((Utilisateur)obj).MotDePasse)
                return true;
            return false;
        }

        public void AjouterFavori(Manga m) ///testé
        {
            if (LesFavoris == null)
            {
                LesFavoris = new List<Manga>();
            }
            LesFavoris.Add(m);

        }
        public void SupprimerFavori(Manga m)
        {
            if (LesFavoris.Contains(m))
            {
                LesFavoris.Remove(m);
            }

        }

   

    }
}