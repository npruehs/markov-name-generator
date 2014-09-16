// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EvaluationCommands.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MarkovNameGenerator.Control
{
    using System.Windows.Input;

    public static class EvaluationCommands
    {
        #region Static Fields

        /// <summary>
        ///   Command to consider a generated name for improving the quality of future names.
        /// </summary>
        public static ICommand AcceptName = new RoutedCommand("Accept Name", typeof(EvaluationCommands));

        /// <summary>
        ///   Command to reject a generated name for improving the quality of future names.
        /// </summary>
        public static ICommand RejectName = new RoutedCommand("Reject Name", typeof(EvaluationCommands));

        #endregion
    }
}