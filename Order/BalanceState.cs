﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Différents états possibles pour le solde d'une commande
    /// Enumération pour être certain que chaque élément créé ne puisse prendre qu'une valeur autorisée
    /// Valeurs autorisées : enattente,ok,perdue
    /// </summary>
    public enum BalanceState
    {
        enattente,
        ok,
        perdue
    }
}