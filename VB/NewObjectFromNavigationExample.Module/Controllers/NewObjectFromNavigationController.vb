Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.ExpressApp.Actions
Imports NewObjectFromNavigationExample.Module.BusinessObjects

Namespace NewObjectFromNavigationExample.Module.Controllers
	Public Class NewObjectFromNavigationController
		Inherits WindowController
		Public Sub New()
			TargetWindowType = WindowType.Main
		End Sub
		Protected Overrides Sub OnActivated()
			MyBase.OnActivated()
			Dim showNavigationItemController As ShowNavigationItemController = Frame.GetController(Of ShowNavigationItemController)()
			AddHandler showNavigationItemController.CustomShowNavigationItem, AddressOf showNavigationItemController_CustomShowNavigationItem
		End Sub
		Private Sub showNavigationItemController_CustomShowNavigationItem(ByVal sender As Object, ByVal e As CustomShowNavigationItemEventArgs)
			If e.ActionArguments.SelectedChoiceActionItem.Id = "NewIssue" Then
				Dim newObjectAction As SingleChoiceAction = GetNewObjectAction()
				If newObjectAction IsNot Nothing Then
					newObjectAction.DoExecute(New ChoiceActionItem() With {.Data = GetType(Issue)})
				Else
					Dim objectSpace As IObjectSpace = Application.CreateObjectSpace()
					Dim newIssue As Issue = objectSpace.CreateObject(Of Issue)()
					Dim detailView As DetailView = Application.CreateDetailView(objectSpace, newIssue)
					detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit
					e.ActionArguments.ShowViewParameters.CreatedView = detailView
				End If
				e.Handled = True
			End If
		End Sub
		Protected Overridable Function GetNewObjectAction() As SingleChoiceAction
			Return Frame.GetController(Of NewObjectViewController)().NewObjectAction
		End Function
	End Class

End Namespace
