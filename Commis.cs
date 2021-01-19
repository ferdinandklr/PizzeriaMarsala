﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzeriaMarsala
{
    class Commis:Personne
    {
        private string presence; //"surplace" ou "enconges"
        private DateTime embauche;

        public Commis() : base()
        {
            this.presence = null;
            this.embauche = default;
        }

        public Commis(string nom, string prenom, string adresse, long numero,string presence, DateTime embauche) : base(nom, prenom, adresse, numero)
        {
            this.presence = presence;
            this.embauche = embauche;
        }
        //Propriétés
        //ToString?
    }
}
