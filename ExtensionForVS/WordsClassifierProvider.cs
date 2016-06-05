using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ExtensionForVS
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("text")] // This classifier applies to all text files.
    internal class WordsClassifierProvider : IClassifierProvider
    {
        // Disable "Field is never assigned to..." compiler's warning. Justification: the field is assigned by MEF.
#pragma warning disable 649
            
        [Import]
        private IClassificationTypeRegistryService classificationRegistry;

#pragma warning restore 649

        #region IClassifierProvider
        
        static WordsClassifier editorClassifier;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            if (editorClassifier == null)
                editorClassifier = new WordsClassifier(classificationRegistry);

            return editorClassifier;
        }

        #endregion
    }
}
