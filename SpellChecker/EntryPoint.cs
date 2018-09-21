using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using SpellChecker.IOHandle;
using SpellChecker.SpellCheckProcessor;
using SpellChecker.Entity.WordDictionary;
using SpellChecker.Entity.Text;


namespace SpellChecker
{
    class EntryPoint
    {
        static void Main(string[] args)
        {
            int maxEditions = 2;
            int insertionCost = 1;
            int deletionCost = 1;
            int substitutionCost = 2;
            int errorCost = -1;
            Stream inputStream = Console.OpenStandardInput();
            Stream outputStream = Console.OpenStandardOutput();

            LevenshteinDistanceAnalyzer analyzer = new LevenshteinDistanceAnalyzer(maxEditions, insertionCost, deletionCost, substitutionCost, errorCost);
            WordParser parser = new WordParser(inputStream);
            RichTextWriter writer = new RichTextWriter(outputStream);
            StaticWordDictionaryBuilder dictionaryBuilder = new StaticWordDictionaryBuilder(parser, analyzer);
            RichTextBuilder textBuilder = new RichTextBuilder(parser);
            WordNeighborsFormater formater = new WordNeighborsFormater();
            SpellCheckProcessor.SpellChecker spellChecker = new SpellCheckProcessor.SpellChecker(dictionaryBuilder, textBuilder, "===");
            spellChecker.Check(writer, formater, 2);
            writer.Flush();
        }
    }
}
