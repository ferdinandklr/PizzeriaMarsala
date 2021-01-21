﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PizzeriaMarsala
{
    /*
     * Cette classe est abstraite, l'objet Personne n'étant pas utilisé dans l'exercice
     * On implémente l'interface IEquatable afin de vérifier si deux personnes sont les même
     * On implémente l'interface IComparable afin de pouvoir trier un tableau de personne par ordre alphabétique
     */
    abstract class Personne: IEquatable<Personne>, IComparable<Personne>
    {

        public string Nom { get; protected set; }
        public string Prenom { get; protected set; }
        public string Adresse { get; protected set; }
        public long NumeroTel { get; protected set; }

        public Personne(string nom, string prenom, string adresse, long numero_tel)
        {
            Nom = nom;
            Prenom = prenom;
            Adresse = adresse;
            NumeroTel = numero_tel;
        }

        public override string ToString()
        {
            return $"{Nom} ; {Prenom} ; {Adresse} ; {NumeroTel}";
        }

        public bool Equals(Personne personne)
        {
            return Nom == personne.Nom && Prenom == personne.Prenom;
        }

        public int CompareTo(Personne personne)
        {
            int resultat_comparaison = Nom.CompareTo(personne.Nom);
            if (resultat_comparaison == 0)
            {
                resultat_comparaison = Prenom.CompareTo(personne.Prenom);
            }
            return resultat_comparaison;
        }

        delegate object CreationInstanceDepuisString(string[] s);

        delegate List<object> CreationListeInstanceDepuisFichier(string nomFichier);

        public static List<string> CreationListeDepuisFichier(string nomFicher)
        {
            StreamReader sr = new StreamReader(nomFicher);
            List<string> liste = new List<string>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                liste.Add(ligne);
            }
            return liste;
        }
        public static List<Client> CreationListeClientsDepuisFichier(string nomFicher)
        {
            StreamReader sr = new StreamReader(nomFicher);
            List<Client> liste = new List<Client>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                string[] mots = ligne.Split(';');
                liste.Add(Client.CreationClientDepuisString(mots));
            }
            return liste;
        }

        public static List<Commis> CreationListeCommisDepuisFichier(string nomFicher)
        {
            StreamReader sr = new StreamReader(nomFicher);
            List<Commis> liste = new List<Commis>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                string[] mots = ligne.Split(';');
                liste.Add(Commis.CreationCommisDepuisString(mots));
            }
            return liste;
        }

        public static List<Livreur> CreationListeLivreursDepuisFichier(string nomFicher)
        {
            StreamReader sr = new StreamReader(nomFicher);
            List<Livreur> liste = new List<Livreur>();
            string ligne = "";
            while (sr.Peek() > 0)
            {
                ligne = sr.ReadLine();
                string[] mots = ligne.Split(';');
                liste.Add(Livreur.CreationLivreurDepuisString(mots));
            }
            return liste;
        }

        /*Méthode permettant de créer ou modifier un fichier à partir d'une liste
         * Permet nottamment de trier un fichier existant:
            * On créé la liste à partir du fichier
            * On trie cette liste avec le critère choisi
            * On remet la liste dans le fichier
         */
        public static void ModificationFichierDepuisListe(string nomFichier, object l)
        {
            StreamWriter sw = new StreamWriter(nomFichier);
            if(l is List<string>)
            {
                List<string> m = (List<string>)l;
                if (m != null && m.Count != 0)
                {
                    m.ForEach(x => sw.WriteLine(x.ToString()));
                }
            }
            if (l is List<Personne>)
            {
                List<Personne> m = (List<Personne>)l;
                if (m != null && m.Count != 0)
                {
                    m.ForEach(x => sw.WriteLine(x.ToString()));
                }
            }
            sw.Close();
        }

        //Méthode permettant d'ajouter un élément en fin de fichier

        public static void AjoutFichier(string nomFichier, object obj)
        {
            StreamWriter sw = new StreamWriter(nomFichier);
            sw.WriteLine(obj.ToString());
            sw.Close();
        }

        //Méthode permettant de modifier une ligne du fichier
        public static void ModificationLigneFichier(string nomFichier, int index,object obj)
        {
            List<string> l = CreationListeDepuisFichier(nomFichier);
            l.Insert(index, obj.ToString());
            StreamWriter sw = new StreamWriter(nomFichier);
            ModificationFichierDepuisListe(nomFichier, l);
            sw.Close();
        }

        public static string AssociationFichier(object liste)
        {
            string s = "";
            if (liste is List<Client>)
            {
                s="FichierClients";
            }
            else
            {
                if (liste is List<Livreur>)
                {
                    s="FichierLivreurs";
                }
                else
                {
                    s="FichierCommis";
                }
            }
            return s;
        }
        public static string ToString(List<object> l)
        {
            string s = "";
            if(l!=null && l.Count != 0)
            {
                l.ForEach(x => s+=x.ToString() + "\n") ;
            }
            return s;
        }
        public static int RechercheIndexNomPrenom(List<Personne> liste, string nom, string prenom)
        {
            int i = liste.FindIndex(x => x.Nom==nom && x.Prenom==prenom);
            return i;
        }

        public static void SupprimePersonne(List<Personne> liste,Personne p)
        {
            int i = liste.FindIndex(x => x.Equals((Personne)p));
            liste.RemoveAt(i);
            ModificationFichierDepuisListe(AssociationFichier(liste), liste);
        }

        public static void SupprimePersonne(List<Personne> liste, string nom, string prenom)
        {
            int i = RechercheIndexNomPrenom(liste,nom, prenom);
            liste.RemoveAt(i);
            ModificationFichierDepuisListe(AssociationFichier(liste), liste);
        }

        public static void ModificationListePersonnes(List<Personne> liste, string nom, string prenom, Personne p)
        {
            int i = RechercheIndexNomPrenom(liste, nom, prenom);
            liste[i]=p ;
            ModificationFichierDepuisListe(AssociationFichier(liste), liste);
        }
    }
}