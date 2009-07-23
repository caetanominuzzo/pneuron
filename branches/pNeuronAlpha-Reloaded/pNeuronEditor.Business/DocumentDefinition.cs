using System;
using System.Drawing;

namespace pNeuronEditor.Business
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
        None = 0,
        DontShowLabel =1,
        TimerSaver = 2,
        KeepOnCloseTabs =4,
        ShowInRecent = 8,
        ShowDraft = 16,
        ShowInOpen = 32,
        Virtual = 64,
        UserFile = KeepOnCloseTabs | TimerSaver | ShowInRecent | ShowDraft | ShowInOpen,
    }
}
