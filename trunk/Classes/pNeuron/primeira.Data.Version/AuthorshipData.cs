using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace primeira.Data.Version
{
    public class AuthorshipData : IRevision
    {
        private string _author;
        private string _description;
        private string _email;
        private string _site;
        private string _autoUpdateUrl;

        private bool _modified;

        public string author
        {
            get { return _author; }
            set
            {
                if (_author != value)
                {
                    _modified = true;
                    if (OnRevision != null)
                        OnRevision(this, _author, value);

                    _author = value;
                }
            }
        }
        public string description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _modified = true;
                    if (OnRevision != null)
                        OnRevision("description", _description, value);

                    _description = value;
                }
            }
        }
        public string email
        {
            get { return _email; }
            set
            {
                if (_email != value)
                {
                    _modified = true;
                    if (OnRevision != null)
                        OnRevision("email", _email, value);

                    _email = value;
                }
            }
        }
        public string site
        {
            get { return _site; }
            set
            {
                if (_site != value)
                {
                    _modified = true;
                    if (OnRevision != null)
                        OnRevision("site", _site, value);

                    _site = value;
                }
            }
        }
        public string autoUpdateUrl
        {
            get { return _autoUpdateUrl; }
            set
            {
                if (_autoUpdateUrl != value)
                {
                    _modified = true;
                    if (OnRevision != null)
                        OnRevision("autoUpdateUrl", _autoUpdateUrl, value);

                    _autoUpdateUrl = value;
                }
            }
        }

        public event OnRevisionDelegate OnRevision;
    }
}
