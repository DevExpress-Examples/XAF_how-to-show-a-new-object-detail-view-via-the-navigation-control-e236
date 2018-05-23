using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Updating;
using DevExpress.ExpressApp;

namespace ShowNewObjectExample.Win {
    public partial class ShowNewObjectExampleWindowsFormsApplication : WinApplication {
        public ShowNewObjectExampleWindowsFormsApplication() {
            InitializeComponent();
        }

        private void ShowNewObjectExampleWindowsFormsApplication_DatabaseVersionMismatch(object sender, DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs e) {
			e.Updater.Update();
			e.Handled = true;
        }
    }
}
