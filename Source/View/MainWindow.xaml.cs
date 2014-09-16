// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MarkovNameGenerator
{
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        private readonly App controller;

        #endregion

        #region Constructors and Destructors

        public MainWindow()
        {
            this.InitializeComponent();

            this.controller = (App)Application.Current;
        }

        #endregion

        #region Methods

        private void CanExecuteClose(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteClose();
        }

        private void CanExecuteNew(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteNew();
        }

        private void CanExecuteOpen(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteOpen();
        }

        private void ExecutedClose(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.ExecutedClose();
        }

        private void ExecutedNew(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.GenerateName();
        }

        private void ExecutedOpen(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.ExecutedOpen();
        }

        #endregion
    }
}