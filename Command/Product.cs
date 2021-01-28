﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Classe abstraite : aucun objet de type Product n'est directement créé
    /// Cette classe permet de définir le type produit dont héritent les classes Pizza et Beverage
    /// </summary>
    /// <attribut>
    /// Price: le prix associé à un produit, il est calculé dans les classes filles
    /// </attribut>
    public abstract class Product
    {
        public double Price;
    }
}
