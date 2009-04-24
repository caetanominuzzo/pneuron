using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace primeira.Data.Version
{
    public class VersionData : IRevision
    {
        private Guid _id;
        private DateTime _createTime;
        private AuthorshipData _authorship;
        private AuthorshipData _authorship2;
        private VersionNumber _number;

        public Guid Id
        {
            get { return _id; }
        }

        public DateTime CreateTime
        {
            get { return _createTime; }
        }

        public AuthorshipData Authorship
        {
            get
            {
                return _authorship;
            }
        }

        public AuthorshipData Authorship2
        {
            get
            {
                return _authorship2;
            }
        }

        public VersionNumber Number
        {
            get
            {
                return _number;
            }
        }

        public VersionData(Guid id)
        {
            this._id = id;
            this._authorship = new AuthorshipData();
            this._authorship.OnRevision += new OnRevisionDelegate(object_OnRevision);

            this._authorship2 = new AuthorshipData();
            this._authorship2.OnRevision += new OnRevisionDelegate(object_OnRevision);
            
            
            //this._number = new VersionNumber();
            //this._number.OnRevision += new OnRevisionDelegate(object_OnRevision);
            this._createTime = DateTime.Now;
        }

        void object_OnRevision(object sender, object oldValue, object newValue)
        {
            PropertyInfo[] pp = this.GetType().GetProperties();

            foreach (PropertyInfo p in pp)
            {
                if (p.PropertyType == sender.GetType())
                    if (p.GetValue(this, null) == sender)
                        if (OnRevision != null)
                            OnRevision(sender, oldValue, newValue);
            }
        }



        public VersionData() : this(Guid.NewGuid())
        {
        }

        public event OnRevisionDelegate OnRevision;
    }
}
