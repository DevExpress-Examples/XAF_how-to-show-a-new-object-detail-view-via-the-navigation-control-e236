using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace NewObjectFromNavigationExample.Module.BusinessObjects {
    [DefaultClassOptions, ImageName("BO_List")]
    public class Issue : BaseObject {
        public Issue(Session session) : base(session) { }
        private string subject;
        public string Subject {
            get { return subject; }
            set { SetPropertyValue("Subject", ref subject, value); }
        }
        private string description;
        [Size(SizeAttribute.Unlimited)]
        public string Description {
            get { return description; }
            set { SetPropertyValue("Description", ref description, value); }
        }
    }
}
