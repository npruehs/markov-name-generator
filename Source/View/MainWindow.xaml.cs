// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MarkovNameGenerator.View
{
    using System.Windows;
    using System.Windows.Input;

    using MarkovNameGenerator.Control;

    /// <summary>
    ///   Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
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

        private void CanExecuteAcceptName(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteAcceptName();
        }

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

        private void CanExecuteRejectName(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteRejectName();
        }

        private void CanExecuteSaveAs(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this.controller.CanExecuteSaveAs();
        }

        private void ExecutedAcceptName(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.AcceptName();
        }

        private void ExecutedClose(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.Close();
        }

        private void ExecutedNew(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.GenerateName();
        }

        private void ExecutedOpen(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.Open();
        }

        private void ExecutedRejectName(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.RejectName();
        }

        private void ExecutedSaveAs(object sender, ExecutedRoutedEventArgs e)
        {
            this.controller.SaveAs();
        }

        #endregion
    }
}