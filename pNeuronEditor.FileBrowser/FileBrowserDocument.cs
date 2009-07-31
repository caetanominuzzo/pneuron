using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using pNeuronEditor.Business;

namespace pNeuronEditor
{
    [DataContract()]
    public class FileBrowserDocument : DocumentBase
    {
        private static DocumentDefinition _definition =
            new DocumentDefinition()
            {
                Name = "File Browser Configuration",
                DefaultName = "default",
                Description = "File & Tab Operations",
                Extension = ".filebrowser",
                Id = new Guid("513ff96c-0d23-44f4-82ab-0dea5a62dcd3"),
                Icon = Image.FromFile(@"D:\Media\Icons\24x24\folder_noborder.png"),
                DefaultEditor = typeof(FileBrowserEditor),
                Options = (DocumentDefinitionOptions.DontShowLabel | DocumentDefinitionOptions.TimerSaver | DocumentDefinitionOptions.KeepOnCloseTabs)
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

        private List<string> _recent = new List<string>();

        public void AddRecent(string filename)
        {
            if (!_recent.Contains(filename))
                _recent.Add(filename);
        }

        [DataMember()]
        public string[] Recent
        {
            get { return _recent.ToArray(); }
            set
            {
                if (_recent == null)
                    _recent = new List<string>(value.Length);
                else
                    _recent.Clear();

                _recent.AddRange(value);
            }

        }

        #endregion

        public static DocumentBase ToObject(string filename)
        {
            return DocumentBase.ToObject(filename, typeof(FileBrowserDocument));
        }

    }
}
