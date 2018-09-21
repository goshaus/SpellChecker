using System;
using System.Collections.Generic;
using SpellChecker.Entity.VantagePointTree;
using SpellChecker.Entity;


namespace SpellChecker.SpellCheckProcessor
{
    public class LevenshteinDistanceAnalyzer : INodeAnalyzer<Word>
    {
        public readonly int insertionCost;
        public readonly int deletionCost;
        public readonly int substitutionCost;
        public readonly int errorCost;
        public int MaxDistance { get; private set; }
        private List<int> searchResultDistances;


        public LevenshteinDistanceAnalyzer(int maxDistance, int insertionCost, int deletionCost, int substitutionCost, int errorCost)
        {
            this.insertionCost = insertionCost;
            this.deletionCost = deletionCost;
            this.substitutionCost = substitutionCost;
            this.errorCost = errorCost;
            this.MaxDistance = maxDistance;
            this.searchResultDistances = new List<int>();
        }

        /// <summary>
        /// Return of distances of compared words
        /// </summary>
        public int[] GetSearchResult()
        {
            this.searchResultDistances.Reverse();
            return this.searchResultDistances.ToArray();
        }

        /// <summary>
        /// Return Levenshtein distance
        /// </summary>
        public int CalculateDistance(Word firstString, Word secondString)
        {
            var patternLength = firstString.Length;
            var targetLength = secondString.Length;

            if (patternLength == 0)
                return targetLength;

            if (targetLength == 0)
                return patternLength;

            var matrix = new int[patternLength + 1, targetLength + 1];


            for (var i = 0; i <= patternLength; matrix[i, 0] = i++) { }
            for (var j = 0; j <= targetLength; matrix[0, j] = j++) { }


            for (var i = 1; i <= patternLength; i++)
            {
                for (var j = 1; j <= targetLength; j++)
                {
                    var cost = (secondString[j - 1] == firstString[i - 1]) ? 0 : substitutionCost;

                    matrix[i, j] = Math.Min(Math.Min(matrix[i - 1, j] + insertionCost,
                                                     matrix[i, j - 1] + deletionCost),
                                                     matrix[i - 1, j - 1] + cost);
                }
            }

            return matrix[patternLength, targetLength];
        }

        /// <summary>
        /// Return Levenshtein distance for tree search engine and create analyze result
        /// </summary>
        public int SearchComparator(Word pattern, Word target)
        {
            int patternLength = pattern.Length;
            int targetLength = target.Length;
            int[,] distanceMatrix = new int[patternLength + 1, targetLength + 1];
            char[,] prescription = new char[patternLength + 1, targetLength + 1];

            for (int i = 0; i <= patternLength; i++)
            {
                distanceMatrix[i, 0] = i;
                prescription[i, 0] = 'D';
            }
            for (int i = 0; i <= targetLength; i++)
            {
                distanceMatrix[0, i] = i;
                prescription[0, i] = 'I';
            }

            for (int i = 1; i <= patternLength; i++)
            {
                for (int j = 1; j <= targetLength; j++)
                {
                    int cost = pattern[i - 1] == target[j - 1] ? 0 : this.substitutionCost;

                    if (distanceMatrix[i, j - 1] < distanceMatrix[i - 1, j] && distanceMatrix[i, j - 1] < distanceMatrix[i - 1, j - 1] + cost)
                    {
                        distanceMatrix[i, j] = distanceMatrix[i, j - 1] + this.insertionCost;
                        prescription[i, j] = 'I';
                    }
                    else if (distanceMatrix[i - 1, j] < distanceMatrix[i - 1, j - 1] + cost)
                    {
                        distanceMatrix[i, j] = distanceMatrix[i - 1, j] + this.deletionCost;
                        prescription[i, j] = 'D';
                    }
                    else
                    {
                        distanceMatrix[i, j] = distanceMatrix[i - 1, j - 1] + cost;
                        prescription[i, j] = (cost == this.substitutionCost) ? 'R' : 'M';
                    }
                }
            }

            int distance = distanceMatrix[patternLength, targetLength];
            if (distance <= MaxDistance)
            {
                if (CheckEditorialPrescriptionRestriction(ref prescription, patternLength, targetLength))
                {
                    this.searchResultDistances.Add(distance);
                }
                else
                {
                    this.searchResultDistances.Add(this.errorCost);
                }
            }

            return distance;
        }

        /// <summary>
        /// Check words prescription on restrictions
        /// </summary>
        private bool CheckEditorialPrescriptionRestriction(ref char[,] prescription, int rows, int cols)
        {
            int i = rows;
            int j = cols;
            char lastOperation = ' ';
            bool result = true;

            do
            {
                char operation = prescription[i, j];
                if (operation == lastOperation && (operation == 'I' || operation == 'D'))
                {
                    result = false;
                    break;
                }

                if (operation == 'R' || operation == 'M')
                {
                    i--;
                    j--;
                }
                else if (operation == 'D')
                {
                    i--;
                }
                else
                {
                    j--;
                }
                lastOperation = operation;
            } while (i != 0 || j != 0);

            return result;
        }
    }
}
