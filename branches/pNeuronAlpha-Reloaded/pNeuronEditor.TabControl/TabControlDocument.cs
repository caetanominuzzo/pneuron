using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using pNeuronEditor.Business;

namespace pNeuronEditor.TabControl
{
    [DataContract()]
    public class TabControlDocument : DocumentBase
    {
        private static DocumentDefinition _definition =
            new DocumentDefinition()
            {
                Name = "Tab Control",
                DefaultName = "default",
                Description = "Tab Control",
                Extension = ".tabcontrol",
                Id = new Guid("513ff96c-0d23-44f4-82ab-0dea5a62dcd3"),
                DefaultEditor = typeof(TabControlEditor),
                Options = DocumentDefinitionOptions.Virtual,
            };

        public static DocumentDefinition DocumentDefinition
        {
            get { return _definition; }
        }

        public override DocumentDefinition GetDefinition
        {
            get { return _definition; }
        }

        #region Data

        private IEnumerable<string> _openEditors;
        private string _selectedTab;

        [DataMember()]
        public string[] OpenEditors
        {
            get { return (from x in TabManager.GetInstance().GetEditorByOptions(DocumentDefinitionOptions.KeepOnCloseTabs).OrderBy(xx=>xx.TimeOPen) select x.Filename).ToArray(); }
            set { _openEditors = value; }
        }

        [DataMember()]
        public string SelectedTab
        {
            get { return TabManager.GetInstance().ActiveEditor.Filename; }
            set { _selectedTab = value; }
        }

        public string GetSelectedTab()
        {
            return _selectedTab;
        }

        public IEnumerable<string> GetOpenTabsFilename()
        {
            if(_openEditors == null)
                _openEditors = new string[0];

            return _openEditors.Cast<string>();
        }

        #endregion

        public static DocumentBase ToObject(string filename)
        {
            return DocumentBase.ToObject(filename, typeof(TabControlDocument));
        }
    }
}


