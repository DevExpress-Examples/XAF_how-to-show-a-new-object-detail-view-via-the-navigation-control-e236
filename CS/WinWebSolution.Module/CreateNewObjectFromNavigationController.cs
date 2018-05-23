using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;

namespace WinSolution.Module {
    public class CreateNewObjectFromNavigationController : ShowNavigationItemController {
        private NewObjectViewController newController;
        private DetailView createdDetailView;
        private const string CreateNewObjectItemId = "CreateNewIssue";
        private const string CreateNewObjectNavigationItemActiveKey = "CreationAllowed";
        private const string CreateNewObjectNavigationItemDefaultPath = "Issues/" + CreateNewObjectItemId;
        public CreateNewObjectFromNavigationController() {
            TargetWindowType = WindowType.Main;
        }
        protected override void InitializeItems() {
            base.InitializeItems();
            ChoiceActionItem newNavigationItem = FindNewNavigationItem();
            if (newNavigationItem != null)
                newNavigationItem.Active[CreateNewObjectNavigationItemActiveKey] = SecuritySystem.IsGranted(new ObjectAccessPermission(typeof(DemoIssue), ObjectAccess.Create));
        }
        protected override void ShowNavigationItem(SingleChoiceActionExecuteEventArgs e) {
            if ((e.SelectedChoiceActionItem != null) && e.SelectedChoiceActionItem.Enabled.ResultValue && e.SelectedChoiceActionItem.Id == CreateNewObjectItemId) {
                Frame workFrame = Application.CreateFrame(TemplateContext.ApplicationWindow);
                workFrame.SetView(Application.CreateListView(Application.CreateObjectSpace(), typeof(DemoIssue), true));
                newController = workFrame.GetController<NewObjectViewController>();
                if (newController != null) {
                    ChoiceActionItem newObjectItem = FindNewObjectItem();
                    if (newObjectItem != null) {
                        newController.NewObjectAction.Executed += NewObjectAction_Executed;
                        newController.NewObjectAction.DoExecute(newObjectItem);
                        newController.NewObjectAction.Executed -= NewObjectAction_Executed;
                        e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        e.ShowViewParameters.CreatedView = createdDetailView;
                        //Cancel the default processing for this navigation item.
                        return;
                    }
                }
            }
            //Continue the default processing for other navigation items.
            base.ShowNavigationItem(e);
        }
        private ChoiceActionItem FindNewObjectItem() {
            foreach (ChoiceActionItem item in newController.NewObjectAction.Items)
                if (item.Data == typeof(DemoIssue))
                    return item;
            return null;
        }
        private ChoiceActionItem FindNewNavigationItem() {
            return ShowNavigationItemAction.FindItemByIdPath(CreateNewObjectNavigationItemDefaultPath);
        }
        private void NewObjectAction_Executed(object sender, ActionBaseEventArgs e) {
            createdDetailView = e.ShowViewParameters.CreatedView as DetailView;
            //Cancel showing the default View by the NewObjectAction
            e.ShowViewParameters.CreatedView = null;
        }
    }
}