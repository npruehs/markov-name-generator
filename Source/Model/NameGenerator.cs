// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NameGenerator.cs" company="Slash Games">
//   Copyright (c) Slash Games. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MarkovNameGenerator.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NameGenerator
    {
        #region Fields

        private readonly Random random = new Random();

        #endregion

        #region Public Properties

        public int Order { get; set; }

        public List<string> SampleData { get; set; }

        #endregion

        #region Public Methods and Operators

        public string GenerateName()
        {
            // Pick random sample name.
            var randomSampleNameIndex = this.random.Next(this.SampleData.Count);
            var randomSampleName = this.SampleData[randomSampleNameIndex];

            // Start with first letters.
            var currentSubname = randomSampleName.Substring(0, this.Order);
            var finalName = currentSubname;

            while (true)
            {
                // Find sample names containing current sub name.
                var matchingSamples = this.SampleData.Where(sample => sample.Contains(currentSubname));

                // Count occurrences.
                var occurences = new Dictionary<string, int>();

                foreach (var matchingSample in matchingSamples)
                {
                    var index = matchingSample.IndexOf(currentSubname, StringComparison.Ordinal);
                    string nextLetter;

                    // Check for end of word.
                    if (index + this.Order < matchingSample.Length)
                    {
                        nextLetter = matchingSample.Substring(index + this.Order, 1);
                    }
                    else
                    {
                        nextLetter = string.Empty;
                    }

                    if (occurences.ContainsKey(nextLetter))
                    {
                        occurences[nextLetter]++;
                    }
                    else
                    {
                        occurences[nextLetter] = 1;
                    }
                }

                // Create array containing each letter as often as it was contained by sample data names.
                var sumProbability = occurences.Values.Sum();
                var probableLetters = new string[sumProbability];
                int nextLetterIndex = 0;

                foreach (var occurence in occurences)
                {
                    int occurenceProcessed = 0;

                    while (occurenceProcessed < occurence.Value)
                    {
                        probableLetters[nextLetterIndex] = occurence.Key;
                        occurenceProcessed++;
                        nextLetterIndex++;
                    }
                }

                // Pick next letter.
                var newLetter = probableLetters[this.random.Next(sumProbability)];

                if (newLetter.Equals(string.Empty))
                {
                    break;
                }

                finalName += newLetter;
                currentSubname = currentSubname.Substring(1, 1) + newLetter;
            }

            return finalName;
        }

        #endregion
    }
}