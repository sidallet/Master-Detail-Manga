﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Modele   
{
    public class Listes : INotifyPropertyChanged
{
        /// <summary>
        /// Classe qui va etre serializée et qui va effectuer les méthodes sur nos classes
        /// </summary>
       
        public ReadOnlyCollection<Compte> ListeCompte { get; private set; }
        private IList<Compte> listeCompte { get; set; } 
        
        public ReadOnlyDictionary<Genre,SortedSet<Manga>> CollectionManga { get; private set; }
        private IDictionary<Genre, SortedSet<Manga>> collectionManga { get; set; }

        public IList<Genre> ListeGenre { get; private set; }

    // indice du prof : pour le dictionnary on doit faire la propriété calculée au niveau d'une classe qui peut avoir le genre donné par l'utilisateur. Côté vue peut etre ?
        private Compte compte;
        public Compte CompteCourant 
        { 
            get => compte;  
            set
            {
                if (compte !=value)
                {
                    compte = value;
                    OnPropertyChanged(nameof(CompteCourant));
                }
                
            }
        }

        private Genre genreCourant { get; set; }
        public Genre GenreCourant
        {
            get => genreCourant;
            set
            {
                if (genreCourant != value)
                {
                    genreCourant = value;
                    OnPropertyChanged(nameof(genreCourant));
                }

            }
        }


        //private SortedSet<Manga> listeMangaCourant = new SortedSet<Manga>();

        public ObservableCollection<Manga> ListeMangaCourant { get; private set; } = new ObservableCollection<Manga>();
        //J'ai mis une observable collection car quand on change de genre on change les éléments à l'intérieur et donc il faut notifier la vue car ce sont les éléments 
        //à 'intérieur qui change et non l'objet pointé par la référence

        /*public SortedSet<Manga> ListeMangaCourant
        {
            get => listeMangaCourant;
            set
            {
                if (listeMangaCourant != value)
                {
                    listeMangaCourant = value;
                    OnPropertyChanged(nameof(listeMangaCourant));
                }

            }
        }*/

        private Manga mangaCourant;
        public Manga MangaCourant
        {
            get => mangaCourant;
            set
            {
                if (mangaCourant != value)
                {
                    mangaCourant = value;
                    OnPropertyChanged(nameof(mangaCourant));
                }
            }
        }

        void OnPropertyChanged(string nomProp)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomProp));

        /// <summary>
        /// Constructeur de la classe
        /// </summary>
        /// <param name="lCompte">Listes des comptes</param>
        /// <param name="cManga">Dictionnaire des manga, clé : genres, valeur : liste de manga</param>
        public Listes(List<Compte> lCompte, Dictionary<Genre, SortedSet<Manga>> cManga,List<Genre> lGenre)
        {
            listeCompte = lCompte;
            ListeCompte = new ReadOnlyCollection<Compte>(listeCompte);
            //ListeCompte = new List<Compte>(listeCompte);
            collectionManga = cManga;
            CollectionManga = new ReadOnlyDictionary<Genre, SortedSet<Manga>>(collectionManga);
            ListeGenre = lGenre;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Permet de recuperer le genre à partir de son nom de l'enum
        /// </summary>
        /// <param name="nomGenre">nom du genre dans l'enum</param>
        /// <returns>g de la classe Genre</returns>
        public Genre RecupererGenre(GenreDispo nomGenre)
       {
            var genres = CollectionManga.Keys;
            var g = from genre in genres
                      where genre.NomGenre.Equals(nomGenre)
                      select genre;
            return g.FirstOrDefault();
        }

        /// <summary>
        /// Permet de renvoyer une liste de manga du même genre
        /// </summary>
        /// <param name="g">Genre de manga voulu</param>
        /// <returns>Liste de manga du genre passé en parametre</returns>
        public void ListeParGenre(Genre g) 
        {
            //SortedSet<Manga> listeDuGenre = new SortedSet<Manga>();
            ListeMangaCourant.Clear();
            var temp = from k in CollectionManga

                       where k.Key.NomGenre.Equals(g.NomGenre)
                       select k.Value;

            foreach (var t in temp)
            {

                foreach (Manga m in t)
                {
                    ListeMangaCourant.Add(m);
                }
            }
            //return listeDuGenre;


        }



        /// <summary>
        /// Permet d'ajouter un manga dans le dictionnaire
        /// </summary>
        /// <param name="m">Manga a ajouter</param>
        /// <param name="g">Genre du manga pour le placer dans la bonne clé</param>
        public void AjouterManga(Manga m, Genre g) //a modifier a terme
        {
            foreach(KeyValuePair<Genre,SortedSet<Manga>> kvp in CollectionManga)
            {
                if (kvp.Key.Equals(g))
                {
                    if (!kvp.Value.Contains(m))
                    {
                        kvp.Value.Add(m);
                    }
                }
            }
        }
        /// <summary>
        /// Permet supprimer un manga dans le dictionnaire
        /// </summary>
        /// <param name="m">Manga a supprimer</param>
        /// <param name="g">Genre du manga pour le supprimer au niveau de la bonne clé</param>
        public void SupprimerManga(Manga m, Genre g) /// a modifier a terme
        {
            foreach (KeyValuePair<Genre, SortedSet<Manga>> kvp in CollectionManga)
            {
                if (kvp.Key.Equals(g))
                {
                    if (kvp.Value.Contains(m))
                    {
                        kvp.Value.Remove(m);
                    }
                }
            }
        }

        /// <summary>
        /// permet de modifier les informations d'un manga : les elements en parametres peuvent etre modifié
        /// </summary>
        /// <param name="g">Genre du manga pour le retrouver dans la bonne clé</param>
        /// <param name="to">titre original</param>
        /// <param name="dTome">date du dernier tome</param>
        /// <param name="nbTome">nombre de tome</param>
        /// <param name="couv">lien vers l'image de couverture</param>
        /// <param name="synop">texte permetant de lancer l'intrigue du manga</param>
        public void ModifierManga(Genre g, string to, DateTime dTome, int nbTome, string couv, string synop)
        {
            foreach (KeyValuePair<Genre, SortedSet<Manga>> kvp in CollectionManga)
            {
                if (kvp.Key.Equals(g))
                {
                    foreach(Manga manga in kvp.Value)
                    {
                        if (manga.TitreOriginal.Equals(to))
                        {
                            manga.Modifier(dTome, nbTome, couv, synop);
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Permet d'ajouter un avis a un manga
        /// </summary>
        /// <param name="a">Avis </param>
        /// <param name="g">Genre du manga pour le retrouver dans le dictionnaire</param>
        /// <param name="m">Manga qui va recevoir l'avis</param>
        public void AjouterAvis(Avis a, Genre g, Manga m)
        {
            foreach (KeyValuePair<Genre, SortedSet<Manga>> kvp in CollectionManga)
            {
                if (kvp.Key.Equals(g))
                {
                    foreach (Manga man in kvp.Value)
                    {
                        if (man.Equals(m))
                        {
                            man.AjouterAvis(a);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Cherche le meilleur manga = meilleure note de tous
        /// </summary>
        /// <returns>Manga le plus apprecié</returns>
        public Manga ChercherMeilleurManga()
        {
            Manga leMeilleur = new Manga("test", "test", "test", "test", "test", "test", new DateTime(2000), new DateTime(2001), 1, "/test/", "testtest",GenreDispo.Shonen);

            foreach (KeyValuePair<Genre, SortedSet<Manga>> kvp in CollectionManga)
            {
                //var m = kvp.Value.Max(man => man.MoyenneNote);
                foreach (Manga man in kvp.Value)
                {
                    if (man.MoyenneNote > leMeilleur.MoyenneNote)
                    {
                        leMeilleur = man;
                    }
                }
            }
            return leMeilleur;

        }
        /// <summary>
        /// Permet de modifier le profil, appel la fonction du même nom dans Compte
        /// </summary>
        /// <param name="oldPseudo">ancien pseudo</param>
        /// <param name="newPseudo">nouveau pseudo qu'on souhaite</param>
        /// <param name="genrePref">genres preferes qu'on souhaite </param>
        public void ModifierProfil(string oldPseudo, string newPseudo, GenreDispo[] genrePref)
        {
            foreach(Compte compte in ListeCompte)
            {
                if (compte.Pseudo.Equals(oldPseudo))
                {
                    compte.ModifierProfil(newPseudo, genrePref);
                }
            }
        }

        /// <summary>
        /// Cherche un utilisateur à travers toute la collection de compte
        /// </summary>
        /// <param name="pseudo">pseudo du Compte à retrouver</param>
        /// <param name="motDePasse">mot de passe du Compte à retrouver</param>
        /// <returns>Le compte cherché</returns>
        public bool ChercherUtilisateur(string pseudo, string motDePasse)
        {
            foreach(Compte c in ListeCompte)
            {
                if(c.Pseudo.Equals(pseudo) && c.MotDePasse.Equals(motDePasse))
                {
                    CompteCourant = c;
                    return true;
                }
            }
            return false; 
        }

        /*public void ChercherListeParGenre(Genre g)
        {
            //SortedSet<Manga> listeTemp = new SortedSet<Manga>();
            
            var temp = from k in CollectionManga

                       where k.Key.NomGenre.Equals(g.NomGenre)
                       select k.Value;

            foreach (var t in temp)
            {

                foreach (Manga m in t)
                {
                    listeMangaCourant.Add(m);
                }
            }
            //ListeMangaCourant = listeTemp;
            


        }*/
        /// <summary>
        /// Permet d'ajouter un utilisateur
        /// </summary>
        /// <param name="c">Compte à ajouter</param>
        public void AjouterUtilisateur(Compte c)
        {            
            if(!listeCompte.Contains(c))
            {
                listeCompte.Add(c);
            }
        }

        /// <summary>
        /// Appel la méthode Compte pour ajouter un Manga favori à la liste de favori d'un Compte
        /// </summary>
        /// <param name="m">Manga à ajouter aux favoris</param>
        /// <param name="c">Compte qui reçoit le favori</param>
        public void AjouterFavoriManga(Manga m, Compte c)
        {
            c.AjouterFavori(m);
        }

        /// <summary>
        /// Appel la méthode Compte pour supprimer un Manga favori de la liste de favori d'un Compte
        /// </summary>
        /// <param name="m">Manga à supprimer des favoris</param>
        /// <param name="c">Compte qui veut supprimer le favori</param>
        public void SupprimerFavoriManga(Manga m, Compte c)
        {
            c.SupprimerFavori(m);
        }



        /// <summary>
        /// Permet de transformer une instance en chaîne de caractères
        /// </summary>
        public override string ToString()
        {
            string r= "Liste des comptes : \n\n ";
           
            foreach(Compte c in ListeCompte)
            {
                r += $"{c} \n";
            }
            r += " Collection de mangas : \n\n ";
            foreach (KeyValuePair<Genre, SortedSet<Manga>> kvp in CollectionManga)
            {
                r += $"{kvp.Key} : \n ";
                foreach (Manga m in kvp.Value)
                {
                    r += $"{m} \n";
                }
            }
            return r;
        }

       


    }
}
