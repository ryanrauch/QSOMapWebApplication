using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QSOMapWebApplication.Data.Contracts
{
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xmldata.qrz.com")]
    public partial class QRZDatabaseSession
    {

        private string keyField;

        private string countField;

        private string subExpField;

        private string gMTimeField;

        private string remarkField;

        /// <remarks/>
        public string Key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        public string Count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }

        /// <remarks/>
        public string SubExp
        {
            get
            {
                return this.subExpField;
            }
            set
            {
                this.subExpField = value;
            }
        }

        /// <remarks/>
        public string GMTime
        {
            get
            {
                return this.gMTimeField;
            }
            set
            {
                this.gMTimeField = value;
            }
        }

        /// <remarks/>
        public string Remark
        {
            get
            {
                return this.remarkField;
            }
            set
            {
                this.remarkField = value;
            }
        }
    }


}
