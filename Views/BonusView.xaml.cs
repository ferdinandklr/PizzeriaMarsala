﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using Microsoft.Win32;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette vue affiche les bonus
    /// </summary>
    public partial class BonusView : Page
    {
        #region Attributs
        MainWindow main_window;
        public long BillOrderId { get; set; } = 0;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        public BonusView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on définit le data context
            this.DataContext = this;

            // on enregistre l'object windows localement
            this.main_window = main_window;

            // on charge le titre de l'application
            AppTitle.Content = new AppTitleComponent();

            // on charge la barre des menus
            MenuBar.Content = new ViewSwitcherComponent(main_window);
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Méthode qui créée une facture
        /// </summary>
        private void CreateBill(object sender, RoutedEventArgs e)
        {
            Order command = Pizzeria.FindOrder(BillOrderId);
            if (command != null)
            {
                SaveFileDialog file_dialog = new SaveFileDialog();
                if (file_dialog.ShowDialog() == true)
                {
                    command.SaveBill(file_dialog.FileName);
                }
            }
        }

        /// <summary>
        /// Méthode qui sauvegarde les clients
        /// </summary>
        private void SaveCustomers(object sender, RoutedEventArgs e)
        {
            SaveFileDialog file_dialog = new SaveFileDialog();
            if (file_dialog.ShowDialog() == true)
            {
                Pizzeria.SaveCustomers(file_dialog.FileName);
            }
        }

        /// <summary>
        /// Méthode qui sauvegarde les commis
        /// </summary>
        private void SaveWorkers(object sender, RoutedEventArgs e)
        {
            SaveFileDialog file_dialog = new SaveFileDialog();
            if (file_dialog.ShowDialog() == true)
            {
                Pizzeria.SaveWorkers(file_dialog.FileName);
            }
        }

        /// <summary>
        /// Méthode qui sauvegarde les livreurs
        /// </summary>
        private void SaveDeliverers(object sender, RoutedEventArgs e)
        {
            SaveFileDialog file_dialog = new SaveFileDialog();
            if (file_dialog.ShowDialog() == true)
            {
                Pizzeria.SaveDeliverers(file_dialog.FileName);
            }
        }

        /// <summary>
        /// Méthode qui sauvegarde les commandes
        /// </summary>
        private void SaveOrders(object sender, RoutedEventArgs e)
        {
            SaveFileDialog file_dialog = new SaveFileDialog();
            if (file_dialog.ShowDialog() == true)
            {
                Pizzeria.SaveOrders(file_dialog.FileName);
            }
        }
        #endregion
    }
}
