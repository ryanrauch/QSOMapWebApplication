using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QSOMapWebApplication.Data.Contracts
{
    // NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xmldata.qrz.com")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://xmldata.qrz.com", IsNullable = false)]
    public partial class QRZDatabase
    {

        private QRZDatabaseSession sessionField;

        private decimal versionField;

        private QRZCallsign callsignField;

        /// <remarks/>
        public QRZDatabaseSession Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public decimal version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public QRZCallsign Callsign
        {
            get
            {
                return this.callsignField;
            }
            set
            {
                this.callsignField = value;
            }
        }
    }
}
