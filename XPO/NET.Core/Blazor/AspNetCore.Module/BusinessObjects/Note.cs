using System;
using DevExpress.Xpo;
using System.ComponentModel;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using System.Text;
using DevExpress.Persistent.BaseImpl;

namespace AspNetCore.Module.BusinessObjects {

    [DefaultProperty(nameof(Text))]
    public class Note : BaseObject {
        private String author;
        private DateTime dateTime;
        private String text;
        public Note(Session session) : base(session) { }
        public String Author {
            get { return author; }
            set { SetPropertyValue(nameof(Author), ref author, value); }
        }
        public DateTime DateTime {
            get { return dateTime; }
            set { SetPropertyValue(nameof(DateTime), ref dateTime, value); }
        }
        [Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
        public String Text {
            get { return text; }
            set { SetPropertyValue(nameof(Text), ref text, value); }
        }
    }
}