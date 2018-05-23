using DevExpress.ExpressApp.Model;
using System;
using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.Metadata;
using DevExpress.ExpressApp;

namespace ShowNewObjectExample.Module {
    [DefaultClassOptions]
    [ImageName("BO_Report")]
    public class DemoIssue : BaseObject {
        public DemoIssue(Session session) : base(session) { }
        public override void AfterConstruction() {
            base.AfterConstruction();
            _CreatedBy = Session.GetObjectByKey<SimpleUser>(SecuritySystem.CurrentUserId);
        }
        [Persistent("ModifiedOn"), ValueConverter(typeof(UtcDateTimeConverter))]
        protected DateTime _ModifiedOn = DateTime.Now;
        [PersistentAlias("_ModifiedOn")]
        [ModelDefault("EditMask", "G")]
        [ModelDefault("DisplayFormat", "{0:G}")]
        public DateTime ModifiedOn {
            get { return _ModifiedOn; }
        }
        internal virtual void UpdateModifiedOn() {
            UpdateModifiedOn(DateTime.Now);
        }
        internal virtual void UpdateModifiedOn(DateTime date) {
            _ModifiedOn = date;
            Save();
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue) {
            base.OnChanged(propertyName, oldValue, newValue);
            if(propertyName == "Subject" || propertyName == "Description") {
                UpdateModifiedOn();
            }
        }
        private string _Subject;
        public string Subject {
            get { return _Subject; }
            set { SetPropertyValue("Subject", ref _Subject, value); }
        }
        private string _Description;
        public string Description {
            get { return _Description; }
            set { SetPropertyValue("Description", ref _Description, value); }
        }
        [Persistent("CreatedBy")]
        private SimpleUser _CreatedBy;
        [PersistentAlias("_CreatedBy")]
        public SimpleUser CreatedBy {
            get { return _CreatedBy; }
            internal set { _CreatedBy = value; }
        }
    }
    /*public class DemoUpdater : ModuleUpdater {
        public DemoUpdater(Session session, Version currentDBVersion) : base(session, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();
            SimpleUser user = new SimpleUser(Session);
            user.UserName = "Sam";
            user.SetPassword("");
            user.Save();
            DemoIssue obj1 = new DemoIssue(Session);
            obj1.Subject = "Issue 3";
            obj1.CreatedBy = Session.FindObject<SimpleUser>(null);
            obj1.UpdateModifiedOn();
            obj1.Save();
            DemoIssue obj2 = new DemoIssue(Session);
            obj2.Subject = "Issue 2";
            obj2.CreatedBy = Session.FindObject<SimpleUser>(null);
            obj2.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1));
            obj2.Save();
            DemoIssue obj3 = new DemoIssue(Session);
            obj3.Subject = "Issue 1";
            obj3.CreatedBy = Session.FindObject<SimpleUser>(null);
            obj3.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2));
            obj3.Save();
        }
    }*/
}
