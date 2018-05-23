using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Actions;
using NewObjectFromNavigationExample.Module.BusinessObjects;

namespace NewObjectFromNavigationExample.Module.Controllers {
    public class NewObjectFromNavigationController : WindowController {
        public NewObjectFromNavigationController() {
            TargetWindowType = WindowType.Main;
        }
        protected override void OnActivated() {
            base.OnActivated();
            ShowNavigationItemController showNavigationItemController = Frame.GetController<ShowNavigationItemController>();
            showNavigationItemController.CustomShowNavigationItem += showNavigationItemController_CustomShowNavigationItem;
        }
        void showNavigationItemController_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e) {
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "NewIssue") {
                SingleChoiceAction newObjectAction = GetNewObjectAction();
                if (newObjectAction != null) {
                    newObjectAction.DoExecute(new ChoiceActionItem() { Data = typeof(Issue) });
                }
                else {
                    IObjectSpace objectSpace = Application.CreateObjectSpace();
                    Issue newIssue = objectSpace.CreateObject<Issue>();
                    DetailView detailView = Application.CreateDetailView(objectSpace, newIssue);
                    detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                    e.ActionArguments.ShowViewParameters.CreatedView = detailView;
                }
                e.Handled = true;
            }
        }
        protected virtual SingleChoiceAction GetNewObjectAction() {
            return Frame.GetController<NewObjectViewController>().NewObjectAction;
        }
    }
        
}
