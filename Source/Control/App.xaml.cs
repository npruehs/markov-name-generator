// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MarkovNameGenerator.Control
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Media;

    using MarkovNameGenerator.Model;
    using MarkovNameGenerator.View;

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

        private const string SampleDataLabelText = "{0} names in sample data set.";

        #endregion

        #region Static Fields

        private static readonly Color AcceptedNameColor = Colors.Green;

        private static readonly Color NewNameColor = Colors.Black;

        private static readonly Color RejectedNameColor = Colors.Red;

        #endregion

        #region Fields

        private readonly List<string> sampleData = new List<string>();

        private string lastName;

        private MainWindow mainWindow;

        private NameGenerator nameGenerator;

        private string sampleDataFileName;

        #endregion

        #region Public Methods and Operators

        public void AcceptName()
        {
            if (!this.sampleData.Contains(this.lastName))
            {
                this.sampleData.Add(this.lastName);
                this.mainWindow.LastName.Foreground = new SolidColorBrush(AcceptedNameColor);
                this.mainWindow.StatusText.Text = string.Format(SampleDataLabelText, this.sampleData.Count);
            }
        }

        public bool CanExecuteAcceptName()
        {
            return !string.IsNullOrEmpty(this.lastName);
        }

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

        public bool CanExecuteRejectName()
        {
            return !string.IsNullOrEmpty(this.lastName);
        }

        public bool CanExecuteSaveAs()
        {
            return this.sampleData.Count > 0;
        }

        public void Close()
        {
            Environment.Exit(0);
        }

        public void GenerateName()
        {
            var newName = this.nameGenerator.GenerateName();

            this.SetName(newName);

            this.mainWindow.LastName.Foreground = this.sampleData.Contains(newName)
                                                      ? new SolidColorBrush(AcceptedNameColor)
                                                      : new SolidColorBrush(NewNameColor);
        }

        public void Open()
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

            // Update UI.
            this.SetName(string.Empty);
            this.mainWindow.StatusText.Text = string.Format(SampleDataLabelText, this.sampleData.Count);
        }

        public void RejectName()
        {
            this.sampleData.Remove(this.lastName);
            this.mainWindow.LastName.Foreground = new SolidColorBrush(RejectedNameColor);
            this.mainWindow.StatusText.Text = string.Format(SampleDataLabelText, this.sampleData.Count);
        }

        public void SaveAs()
        {
            // Show save file dialog box.
            SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    AddExtension = true,
                    CheckPathExists = true,
                    DefaultExt = SampleDataFileExtension,
                    FileName = "SampleData",
                    Filter = SampleDataFileFilter,
                    ValidateNames = true
                };

            var result = saveFileDialog.ShowDialog();

            if (result != true)
            {
                return;
            }

            // Update main window title.
            this.sampleDataFileName = saveFileDialog.FileName;
            this.mainWindow.Title = string.Format("{0} - {1}", MainWindowTitle, this.sampleDataFileName);

            // Open file stream.
            using (var fileStream = saveFileDialog.OpenFile())
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    foreach (var name in this.sampleData)
                    {
                        streamWriter.WriteLine(name);
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void OnActivated(object sender, EventArgs e)
        {
            this.mainWindow = (MainWindow)this.MainWindow;
            this.mainWindow.Title = MainWindowTitle;
        }

        private void SetName(string newName)
        {
            this.lastName = newName;
            this.mainWindow.LastName.Text = newName;
        }

        #endregion
    }
}