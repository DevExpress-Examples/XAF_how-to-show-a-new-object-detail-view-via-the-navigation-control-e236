using System;

using DevExpress.ExpressApp.Updating;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Persistent.BaseImpl;

namespace ShowNewObjectExample.Module {
    public class Updater : ModuleUpdater {
        public Updater(IObjectSpace objectSpace, Version currentDBVersion) : base(objectSpace, currentDBVersion) { }
        public override void UpdateDatabaseAfterUpdateSchema() {
            base.UpdateDatabaseAfterUpdateSchema();

            SimpleUser user = ObjectSpace.FindObject<SimpleUser>(CriteriaOperator.Parse("UserName == 'Sam'"));
            if (user == null) {
                user = ObjectSpace.CreateObject<SimpleUser>();
                user.UserName = "Sam";
                user.SetPassword("");
            }

            DemoIssue obj1 = ObjectSpace.FindObject<DemoIssue>(CriteriaOperator.Parse("Subject == 'Issue 1'"));
            if (obj1 == null) {
                obj1 = ObjectSpace.CreateObject<DemoIssue>();
                obj1.Subject = "Issue 1";
                obj1.CreatedBy = ObjectSpace.FindObject<SimpleUser>(null);
                obj1.UpdateModifiedOn();
            }

            DemoIssue obj2 = ObjectSpace.FindObject<DemoIssue>(CriteriaOperator.Parse("Subject == 'Issue 2'"));
            if (obj2 == null) {
                obj2 = ObjectSpace.CreateObject<DemoIssue>();
                obj2.Subject = "Issue 2";
                obj2.CreatedBy = ObjectSpace.FindObject<SimpleUser>(null);
                obj2.UpdateModifiedOn();
            }


            DemoIssue obj3 = ObjectSpace.FindObject<DemoIssue>(CriteriaOperator.Parse("Subject == 'Issue 3'"));
            if (obj3 == null) {
                obj3 = ObjectSpace.CreateObject<DemoIssue>();
                obj3.Subject = "Issue 3";
                obj3.CreatedBy = ObjectSpace.FindObject<SimpleUser>(null);
                obj3.UpdateModifiedOn();
            }

        }
    }
}
