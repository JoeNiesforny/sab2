using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace ExtensionForVS
{
    internal static class WordsClassifierClassificationDefinition
    {
        // This disables "The field is never used" compiler's warning. Justification: the field is used by MEF.
#pragma warning disable 169

        [Export]
        [Name("findWords")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition editorContentTypeDefinition = null;
        
        [Export]
        [Name("findWords")]
        internal static ClassificationTypeDefinition editorClassificationDefinition = null;

        [Export]
        [Name("findWords.GoodWords")]
        [BaseDefinition("findWords")]
        internal static ClassificationTypeDefinition goodWordsClassificationDefinition = null;

        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "findWords.GoodWords")]
        [Name("findWords.GoodWords")]
        internal sealed class GoodWordsFormat : ClassificationFormatDefinition
        {
            public GoodWordsFormat()
            {
                DisplayName = "Good Word";
                ForegroundColor = Colors.Gold;
                BackgroundColor = Colors.Blue;
            }
        }

        [Export]
        [Name("findWords.BadWords")]
        [BaseDefinition("findWords")]
        internal static ClassificationTypeDefinition badWordsClassificationDefinition = null;
        
        [Export(typeof(EditorFormatDefinition))]
        [ClassificationType(ClassificationTypeNames = "findWords.BadWords")]
        [Name("findWords.BadWords")]
        internal sealed class BadWordsFormat : ClassificationFormatDefinition
        {
            public BadWordsFormat()
            {
                DisplayName = "Bad Word";
                ForegroundColor = Colors.Red;
                BackgroundColor = Colors.Black;
            }
        }

#pragma warning restore 169
    }
}
