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
    public partial class CustomerView : Page
    {

        MainWindow main_window;

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
                New, OpenFile,
                "CustomerDataTemplate",
                "PAR NOM", "PAR VILLE", "PAR CMD TOTALES", (o) => { main_window.SwitchToEditCustomerView((Customer)o); }
            );

            // on affiche le content presenter dans l'interface
            ListContentPresenter.Content = presenter;
            presenter.ItemsControlList.DataContext = Pizzeria.ClientList;
        }

        public void New()
        {
            main_window.SwitchToCreateCustomerView();
        }

        public void OpenFile(String file_url)
        {
            Console.WriteLine(file_url);
        }
    }
}
