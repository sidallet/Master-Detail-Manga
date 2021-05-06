﻿using System;
using System.Collections.Generic;
using System.Text;


/// A FINIR : voir sur le diagramme de classe, ??? signifie methodes peut etre inutiles ? 
/// Methode "ModifierProfil" inutile ? les set des proprietes suffisent 

namespace Modele
{
    public class Compte : IEquatable<Compte>
    {
        public List<Manga> LesFavoris;
        public string Pseudo { get; private set; }

        public int Age 
        {
            get
            {
                DateTime now = DateTime.Today;
                int age = now.Year - dateNaissance.Year;               
                if (dateNaissance > now.AddYears(-age))
                    age--;
                return age;
            }

        }

        private DateTime dateNaissance;

        public DateTime DateInscription { get; private set; }

        public string MotDePasse { get; private set; }

        public GenreDispo GenresPreferes { get; private set; }

        public Compte(string pseudo, string dateDeNaissance, DateTime dateInscription, GenreDispo genrepref, string motDePasse)
        {
            Pseudo = pseudo ?? throw new ArgumentNullException(nameof(pseudo));
            dateNaissance = Convert.ToDateTime(dateDeNaissance);
            
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

        public bool Equals(Compte other) 
        {
            if (Pseudo == other.Pseudo && MotDePasse == other.MotDePasse)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return false;
            if (ReferenceEquals(obj, this)) return true;
            if (GetType().Equals(obj.GetType())) return false;
            return Equals((obj as Compte));
        }

        public override int GetHashCode()
        {
            var hashCode = -1968196876;
            
            hashCode = hashCode * -1521134295 + Pseudo.GetHashCode();
           
            hashCode = hashCode * -1521134295 + MotDePasse.GetHashCode();
           
            return hashCode;
        }
    }
}