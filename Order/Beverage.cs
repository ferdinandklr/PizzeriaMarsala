﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Définition de la classe boisson (une boisson est un produit)
    /// </summary>
    /// <attributs>
    /// Type: type de boisson (cf enum BeverageType)
    /// Volume: volume de la boisson
    /// </attributs>
    public class Beverage : Product, INotifyPropertyChanged
    {
        #region Attributs
        public event PropertyChangedEventHandler PropertyChanged;

        private BeverageType _Type;
        public BeverageType Type
        {
            get => _Type;
            set
            {
                _Type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }
        private double _Volume; // volume en cl
        public double Volume
        {
            get => _Volume;
            set
            {
                _Volume = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Volume"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }
        // prix en euros, Price est un attribut de la classe Product
        public new double Price { get => (double)Type / 10000 * Volume; }
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal de la classe Beverage
        /// </summary>
        /// <param name="type">type de boisson</param>
        /// <param name="volume">volume de la boisson</param>
        public Beverage(BeverageType type, double volume)
        {
            Type = type;
            Volume = volume;
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Cette méthode permet de convertir une boisson en un string (afin notamment de l'afficher dans la console)
        /// </summary>
        /// <returns>
        /// TypeDeBoisson (Volumecl) [Prix Euros]
        /// </returns>
        public override string ToString()
        {
            return $"{Type} ({Volume}cl) [{Price} Euros]";
        }

        /// <summary>
        /// Comparaison de deux boisson en fonction de son nom puis de son volume
        /// </summary>
        /// <param name="beverage1">Boisson 1</param>
        /// <param name="beverage2">Boisson 2</param>
        /// <returns>
        /// -1 si la boisson 1 a un nom se situant avant celui de la boisson 2 (ordre alphabétique)
        /// ou si les noms sont égaux mais que le volume de b1 < celui de b2
        /// 0 si les deux boissons ont le même nom et le même volume
        /// 1 sinon
        /// </returns>
        public static int CompareTypeVolume(Beverage beverage1, Beverage beverage2)
        {
            int compa = nameof(beverage1.Type).CompareTo(nameof(beverage2.Type));
            if (compa == 0)
            {
                compa = beverage1.Volume.CompareTo(beverage2.Volume);
            }
            return compa;
        }
        #endregion
    }
}
