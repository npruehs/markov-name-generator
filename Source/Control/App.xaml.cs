// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MarkovNameGenerator
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using MarkovNameGenerator.Model;

    using Microsoft.Win32;

    /// <summary>
    ///   Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        #region Constants

        private const string MainWindowTitle = "Markov Name Generator";

        private const int MarkowChainOrder = 2;

        private const string SampleDataFileExtension = ".txt";

        private const string SampleDataFileFilter =
            "Sample data files (" + SampleDataFileExtension + "|*" + SampleDataFileExtension;

        #endregion

        #region Fields

        private readonly List<string> sampleData = new List<string>();

        private MainWindow mainWindow;

        private NameGenerator nameGenerator;

        private string sampleDataFileName;

        #endregion

        #region Public Methods and Operators

        public bool CanExecuteClose()
        {
            return true;
        }

        public bool CanExecuteNew()
        {
            return this.sampleData.Count > 0;
        }

        public bool CanExecuteOpen()
        {
            return true;
        }

        public void ExecutedClose()
        {
            Environment.Exit(0);
        }

        public void ExecutedOpen()
        {
            // Show open file dialog box.
            OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    AddExtension = true,
                    CheckFileExists = true,
                    CheckPathExists = true,
                    DefaultExt = SampleDataFileExtension,
                    FileName = "SampleData",
                    Filter = SampleDataFileFilter,
                    ValidateNames = true
                };

            var result = openFileDialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            // Update main window title.
            this.sampleDataFileName = openFileDialog.FileName;
            this.mainWindow.Title = string.Format("{0} - {1}", MainWindowTitle, this.sampleDataFileName);

            // Open sample data file.
            this.sampleData.Clear();

            using (var fileStream = openFileDialog.OpenFile())
            {
                using (var streamReader = new StreamReader(fileStream))
                {
                    string s;
                    while ((s = streamReader.ReadLine()) != null)
                    {
                        this.sampleData.Add(s);
                    }
                }
            }

            // Create new name generator.
            this.nameGenerator = new NameGenerator { Order = MarkowChainOrder, SampleData = this.sampleData };
        }

        public void GenerateName()
        {
            this.mainWindow.LastName.Text = this.nameGenerator.GenerateName();
        }

        #endregion

        #region Methods

        private void OnActivated(object sender, EventArgs e)
        {
            this.mainWindow = (MainWindow)this.MainWindow;
            this.mainWindow.Title = MainWindowTitle;
        }

        #endregion
    }
}