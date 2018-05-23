Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports NewObjectFromNavigationExample.Module.Controllers
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.ExpressApp.Win
Imports DevExpress.ExpressApp.SystemModule

Namespace NewObjectFromNavigationExample.Module.Win.Controllers
	Public Class WinNewObjectFromNavigationController
		Inherits NewObjectFromNavigationController
		Protected Overrides Function GetNewObjectAction() As SingleChoiceAction
			Dim strategy As MdiShowViewStrategy = TryCast(Application.ShowViewStrategy, MdiShowViewStrategy)
			If strategy IsNot Nothing Then
				Dim activeInspector As WinWindow = strategy.GetActiveInspector()
				If activeInspector Is Nothing Then
					Return Nothing
				End If
				Return activeInspector.GetController(Of NewObjectViewController)().NewObjectAction
			End If
			Return MyBase.GetNewObjectAction()
		End Function
	End Class
End Namespace
