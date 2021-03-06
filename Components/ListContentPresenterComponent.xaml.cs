﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Collections.Specialized;
using Microsoft.Win32;
using System.ComponentModel;

namespace PizzeriaMarsala
{
    /// <summary>
    /// Le DataTemplateSelector permet de choisir le bon template en fonction de ce que l'on essaye d'afficher
    /// (cf fichier xaml pour plus d'informations)
    /// </summary>
    public class TemplateSelector : DataTemplateSelector
    {
        String Template;

        public TemplateSelector(String template)
        {
            Template = template;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement element = container as FrameworkElement;
            return (element.FindResource(Template) as DataTemplate);
        }
    }

    /// <summary>
    /// Classe représentation le content presenter
    /// Elle permet d'afficher dans l'interface différents types de contenu
    /// </summary>
    public partial class ListContentPresenterComponent : Page, INotifyPropertyChanged
    {
        #region Attributs
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void Action();
        public delegate void OpenFileAction(String file_url);
        public delegate void ObjectClicked(object el);

        public Action SortMethod1;
        public Action SortMethod2;
        public Action SortMethod3;
        public Action NewElement;
        public OpenFileAction OpenFile;
        public ObjectClicked ObjectClickedFunc;

        public string NameSortMenu1 { get; set; }
        public string NameSortMenu2 { get; set; }
        public string NameSortMenu3 { get; set; }

        public ObjectClicked SelectionFunction;
        private string _SelectorValue;
        public string SelectorValue
        {
            get => _SelectorValue;
            set
            {
                _SelectorValue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectorValue"));
            }
        }
        #endregion

        #region Constructeur
        /// <summary>
        /// Crééer un content presenter
        /// </summary>
        /// <param name="sort_method_1">La première méthode de sort</param>
        /// <param name="sort_method_2">La seconde méthode de sort</param>
        /// <param name="sort_method_3">La troisième méthode de sort</param>
        /// <param name="new_element">La fonction appelée lorsqu'on clique sur le bouton crééer un élément</param>
        /// <param name="open_file">La fonction appelée lorsqu'on clique sur le bouton ouvrir un fichier</param>
        /// <param name="data_template">Le nom du data template à utiliser</param>
        /// <param name="name_sort_menu_1">Le nom affiché dans le premier menu de sort</param>
        /// <param name="name_sort_menu_2">Le nom affiché dans le second menu de sort</param>
        /// <param name="name_sort_menu_3">Le npm affiché dans le troisième menu de sort</param>
        /// <param name="object_clicked_function">La fonction appelée lorsqu'on clique sur une carte dans l'interface</param>
        public ListContentPresenterComponent(
            Action sort_method_1, Action sort_method_2, Action sort_method_3,
            Action new_element, OpenFileAction open_file,
            string data_template,
            string name_sort_menu_1, string name_sort_menu_2, string name_sort_menu_3,
            ObjectClicked object_clicked_function, ObjectClicked selection_function
        )
        {
            // on initialise les composants
            InitializeComponent();

            // on définit le contexte
            this.DataContext = this;

            // on définit la fonction appelée lorsqu'on clique sur un élément dans l'interface
            ObjectClickedFunc = object_clicked_function;

            // on sauvegarde les paramêtres de la fênetre
            SortMethod1 = sort_method_1;
            SortMethod2 = sort_method_2;
            SortMethod3 = sort_method_3;
            NewElement = new_element;
            OpenFile = open_file;

            // Lorsque la liste des éléments affichés change, on met à jour l'interface
            ((INotifyCollectionChanged)ItemsControlList.Items).CollectionChanged += (s, e) => { ResizeWrapPanelElements(); };

            // on enregistre le nom des menus
            NameSortMenu1 = name_sort_menu_1;
            NameSortMenu2 = name_sort_menu_2;
            NameSortMenu3 = name_sort_menu_3;

            // on enregistre notre delegate WindowResized
            this.SizeChanged += (sender, e) => { ResizeWrapPanelElements(); };

            // on affiche le titre
            AppTitle.Content = new AppTitleComponent();

            // on charge le bon data template
            ItemsControlList.ItemTemplateSelector = new TemplateSelector(data_template);

            // on sauvegarde la fonction
            SelectionFunction = selection_function;
        }
        #endregion

        #region Méthodes
        #region Ces deux fonctions font en sorte que le WrapPanel change de taille en même temps que la fenêtre
        // variable qui contient le WrapPanel contenant les commandes
        private WrapPanel CommandWrapPanel = null;

        /// <summary>
        /// Fonction qui s'auto-execute lorsque le wrappanel a finit de charger
        /// </summary>
        private void WrapPanelLoaded(object sender, RoutedEventArgs evnt)
        {
            if (sender.GetType() == typeof(WrapPanel))
            {
                CommandWrapPanel = (WrapPanel)sender;
            }
            ResizeWrapPanelElements();
        }

        /// <summary>
        /// fonction qui est exécutée à chaque fois qu'on veut changer la taille des éléments du wrap panel
        /// </summary>
        public void ResizeWrapPanelElements()
        {
            if (CommandWrapPanel != null)
            {
                double prefered_element_size = 250;
                double element_margin = 5;
                int n = (int)(CommandWrapPanel.ActualWidth / (prefered_element_size + 2 * element_margin));
                if (CommandWrapPanel.Children.Count < n) { n = CommandWrapPanel.Children.Count; }
                if (n == 0) { n = 1; }
                double desired_width = CommandWrapPanel.ActualWidth / n;
                foreach (UIElement element in CommandWrapPanel.Children)
                {
                    ((ContentPresenter)element).Width = desired_width;
                }
            }
        }
        #endregion

        #region Les fonctions suivantes vont être executés lorsque l'utilisateur va clicker sur les boutons de l'interface
        private void Tri1Click(object sender, MouseButtonEventArgs e) { SortMethod1(); }
        private void Tri2Click(object sender, MouseButtonEventArgs e) { SortMethod2(); }
        private void Tri3Click(object sender, MouseButtonEventArgs e) {  SortMethod3(); }

        private void AjouterClick(object sender, MouseButtonEventArgs e) { NewElement(); }
        private void ImporterClick(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog file_dialog = new OpenFileDialog();
            if (file_dialog.ShowDialog() == true)
            {
                OpenFile(file_dialog.FileName);
            }
        }

        private void ElementClicked(object sender, MouseButtonEventArgs e)
        {
            ObjectClickedFunc(((Border)sender).Tag);
        }

        private void UseSelection(object sender, MouseButtonEventArgs e)
        {
            SelectionFunction(SelectorValue);
        }
        #endregion

        #endregion
    }
}
