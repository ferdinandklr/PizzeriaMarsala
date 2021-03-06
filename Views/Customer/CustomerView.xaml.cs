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

namespace PizzeriaMarsala
{
    /// <summary>
    /// Cette vue permet d'afficher des clients
    /// </summary>
    public partial class CustomerView : Page
    {
        #region Attribut
        MainWindow main_window;
        #endregion

        #region Constructeur
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="main_window">Une référence à la fenêtre principale</param>
        public CustomerView(MainWindow main_window)
        {
            // on initialise les composants
            InitializeComponent();

            // on sauvegarde l'objet fenêtre
            this.main_window = main_window;

            // on charge la barre des menus
            MenuBar.Content = new ViewSwitcherComponent(main_window);

            // on créer le content presenter
            ListContentPresenterComponent presenter = new ListContentPresenterComponent(
                Pizzeria.SortCustomersByName, Pizzeria.SortCustomersByTown, Pizzeria.SortCustomersByTotalOrders,
                main_window.SwitchToCreateCustomerView, Pizzeria.AddFileToCustomerList,
                "CustomerDataTemplate",
                "PAR NOM", "PAR VILLE", "PAR CMD TOTALES", (o) => { main_window.SwitchToEditCustomerView((Customer)o); },
                (o) => {
                    long phone_number;
                    if (long.TryParse((string)o, out phone_number))
                    {
                        Customer customer = Pizzeria.FindCustomer(phone_number);
                        if (customer != null)
                        {
                            main_window.SwitchToEditCustomerView(customer);
                        }
                    }
                }
            );

            // on affiche le content presenter dans l'interface
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = Pizzeria.CustomersList;
        }
        #endregion
    }
}
