//------------------------------------------------------------------------------
// <copyright file="EditorClassifier1.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace ExtensionForVS
{
    internal class WordsClassifier : IClassifier
    {
        private readonly List<string> _goodWordList = new List<string>()
        {
            "var",
            "core",
            "class",
            "string",
            "static",
            "int",
            "byte"
        };


        private readonly List<string> _badWordList = new List<string>()
        {
            "todo",
            "deadbeef",
            "lol",
            "kurcze",
            "bad",
            "wrong",
            "bug",
            "fuck",
            "shit",
            "//",
            "/*",
            "*/"
        };

        private readonly IClassificationTypeRegistryService _classificationTypeRegistry;
        
        internal WordsClassifier(IClassificationTypeRegistryService registry)
        {
            this._classificationTypeRegistry = registry;
        }

        #region IClassifier

#pragma warning disable 67

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            ITextSnapshot snapshot = span.Snapshot;

            List<ClassificationSpan> spans = new List<ClassificationSpan>();

            if (snapshot.Length == 0)
                return spans;

            int startno = span.Start.GetContainingLine().LineNumber;
            int endno = (span.End - 1).GetContainingLine().LineNumber;

            for (int i = startno; i <= endno; i++)
            {
                ITextSnapshotLine line = snapshot.GetLineFromLineNumber(i);

                IClassificationType type = null;
                string text = line.Snapshot.GetText(new SnapshotSpan(line.Start, line.Length)); 

                foreach (var goodWord in _goodWordList)
                    if (text.Contains(goodWord))
                    {
                        type = _classificationTypeRegistry.GetClassificationType("findWords.GoodWords");
                        break;
                    }

                if (type == null)
                    foreach (var badword in _badWordList)
                        if (text.Contains(badword))
                        {
                            type = _classificationTypeRegistry.GetClassificationType("findWords.BadWords");
                            break;
                        }

                if (type != null)
                    spans.Add(new ClassificationSpan(line.Extent, type));
            }

            return spans;
        }

        #endregion
    }
}
