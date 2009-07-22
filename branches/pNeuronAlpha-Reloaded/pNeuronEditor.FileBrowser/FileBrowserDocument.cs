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
                Name = "File Browser Configuration File",
                DefaultName = "default",
                Description = "pNeuron Topology File",
                Extension = ".filebrowser",
                Id = new Guid("513ff96c-0d23-44f4-82ab-0dea5a62dcd3"),
                Icon = Image.FromFile(@"C:\Users\caetano.CWIPOA\Pictures\folder_noborder.gif"),
                DefaultEditor = typeof(FileBrowserEditor),
                Options = (DocumentDefinitionOptions.DontShowLabel | DocumentDefinitionOptions.Virtual)
            };

        public static DocumentDefinition DocumentDefinition
        {
            get { return _definition; }
        }

        public override DocumentDefinition GetDefinition
        {
            get { return _definition; }
        }

        private List<string> _recent = new List<string>();

        public void AddRecent(string filename)
        {
            if(!_recent.Contains(filename))
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
    }
}
