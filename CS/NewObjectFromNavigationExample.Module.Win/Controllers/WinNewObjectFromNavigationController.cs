using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NewObjectFromNavigationExample.Module.Controllers;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.SystemModule;

namespace NewObjectFromNavigationExample.Module.Win.Controllers {
    public class WinNewObjectFromNavigationController : NewObjectFromNavigationController {
        protected override SingleChoiceAction GetNewObjectAction() {
            MdiShowViewStrategy strategy = Application.ShowViewStrategy as MdiShowViewStrategy;
            if (strategy != null) {
                WinWindow activeInspector = strategy.GetActiveInspector();
                if (activeInspector == null) return null;
                return activeInspector.GetController<NewObjectViewController>().NewObjectAction;
            }
            return base.GetNewObjectAction();
        }
    }
}
