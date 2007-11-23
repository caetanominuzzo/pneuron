using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace primeira.pHistory
{
    public class pHistoryItem
    {
        Guid m_guid;

        public Guid Guid
        {
            get { return m_guid; }
        }

        byte[] m_content;

        public byte[] Content
        {
            get { return m_content; }
            set { m_content = value; }
        }

        Bitmap m_cache;

        public Bitmap Cache
        {
            get { return m_cache; }
            set { m_cache = value; }
        }

        DateTime m_modified;

        public DateTime Modified
        {
            get { return m_modified; }
        }

        string m_email;

        public string Email
        {
            get { return m_email; }
            set { m_email = value; }
        }

        List<pHistoryItem> m_history;

        public List<pHistoryItem> History
        {
            get { return m_history; }
            set { m_history = value; }
        }

        public pHistoryItem()
        {
            m_guid = new Guid();
            m_modified = DateTime.Now;
        }
    }
}