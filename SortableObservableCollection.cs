﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PizzeriaMarsala
{
    public class SortableObservableCollection<T> : ObservableCollection<T>, IList<T> where T : class, IToCSV
    {

        // définition d'une comparaison entre deux éléments
        public delegate int ComparisonFunction(T el1, T el2);

        // définition d'une fonction qui vérifie si un élément
        // de la liste est celui que l'on cherche
        public delegate bool ValidationFunction(T el);

        /*
         * fonction de tri par insertion qui prend en entrée
         * notre fonction de comparaison
         */
        public void Sort(ComparisonFunction comparison_function)
        {
            for (int i = 1; i < this.Count; i++)
            {
                T val = this[i];
                int j = i - 1;
                while (j >= 0 && comparison_function(val, this[j]) < 0)
                {
                    this[j + 1] = this[j];
                    j--;
                }
                this[j + 1] = val;
            }
        }

        /*
         * Cette fonction cherche un élément dans la liste
         * Si elle le trouve, elle le renvoie
         */
        public T Find(ValidationFunction validation_function)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (validation_function(this[i]))
                {
                    return this[i];
                }
            }
            return null;
        }

        public List<T> ToList()
        {
            return (List<T>)this.Items;
        }

        public static bool NonVide(SortableObservableCollection<T> s)
        {
            return (s != null && s.Count != 0);
        }

        public void EnregistrerDansFichierTXT(string nomFichier)
        {
            List<T> liste = this.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.ToString()));
            }
            sw.Close();
        }

        public void EnregistrerDansFichierCSV(string nomFichier)
        {
            List<T> liste = this.ToList();
            StreamWriter sw = new StreamWriter(nomFichier);
            if (liste != null && liste.Count != 0)
            {
                liste.ForEach(x => sw.WriteLine(x.ToCSV()));
            }
            sw.Close();
        }

    }
}
