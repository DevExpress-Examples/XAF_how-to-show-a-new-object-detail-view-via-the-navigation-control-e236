using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Persistent.Base;

using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Security;


namespace HowToCreateNewObjectViaNavigationControl.Module.Win {
   public partial class NewObjectFromNavigationItemController : ShowNavigationItemController {
      private View createdView;
      protected override void ShowNavigationItem(SingleChoiceActionExecuteEventArgs args) {
         base.ShowNavigationItem(args);
         if ((args.SelectedChoiceActionItem != null) && args.SelectedChoiceActionItem.Enabled) {
            if (args.SelectedChoiceActionItem.Id == "NewTask") {

               //Create a new Frame and set a new View for it
               Frame frame = Application.CreateFrame(TemplateContext.ApplicationWindow);
               ObjectSpace objectSpace = Application.CreateObjectSpace();
               CollectionSource collectionSource =
                  (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(Task), "");
               frame.SetView(Application.CreateListView(Application.GetListViewId(typeof(Task)), collectionSource, true));

               //Subscribe the NewObjectAction's Executed event
               NewObjectViewController controller = frame.GetController<NewObjectViewController>();
               controller.NewObjectAction.Executed += new EventHandler<ActionBaseEventArgs>(NewObjectAction_Executed);
               
               //Execute the NewObjectAction
               controller.NewObjectAction.DoExecute(null);
               //Show the NewObjectAction's View when clicking the "NewTask" navigation item
               args.ShowViewParameters.CreatedView = createdView;
            }
         }
      }
      protected override void OnActivated() {
         base.OnActivated();
         Window.ViewChanged += new EventHandler(Window_ViewChanged);
      }
      //Deactivate the SaveAndClose Action
      void Window_ViewChanged(object sender, EventArgs e) {
         if (Window.View != null) {
            if (Window.View.Id == "Task_DetailView") {
               DetailViewController detailViewController = Window.GetController<DetailViewController>();
               detailViewController.SaveAndCloseAction.Active.SetItemValue("TaskDetailViewInMainWindow", false);
            }
         }
      }
      void NewObjectAction_Executed(object sender, ActionBaseEventArgs e) {
         //Get the View to be set for the NewObjectAction "NewTask" item
         createdView = e.ShowViewParameters.CreatedView;
         //Cancel showing this View by the NewObjectAction
         e.ShowViewParameters.CreatedView = null;
      }
      protected override void InitializeItems() {
         base.InitializeItems();
         foreach (ChoiceActionItem group in ShowNavigationItemAction.Items) {
            foreach (ChoiceActionItem item in group.Items) {
               if (item.Id == "NewTask")
                  item.Active.SetItemValue("CreationAllowed", SecuritySystem.IsGranted(new
                     ObjectAccessPermission(typeof(Task), ObjectAccess.Create)));
            }
         }
      }
   }
}
