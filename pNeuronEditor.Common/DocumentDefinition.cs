using System;
using System.Drawing;

namespace primeira.pNeuron.Editor.Business
{
        
    public class DocumentDefinition
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DefaultName { get; set; }
        public string Extension { get; set; }
        public DocumentDefinitionOptions Options { get; set; }
        public Image Icon { get; set; }
        public Type DefaultEditor { get; set; }

        public DocumentDefinition() { }
    }

    [Flags()]
    public enum DocumentDefinitionOptions
    {
        None = 0x0,
        Virtual = 0x1,
        DontShowLabel = 0x2,
    }
}
