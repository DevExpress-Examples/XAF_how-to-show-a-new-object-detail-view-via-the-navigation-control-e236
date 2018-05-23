Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Actions
Imports DevExpress.Persistent.Base

Imports DevExpress.ExpressApp.SystemModule
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security


Namespace ShowNewObjectExample.Module
	Public Class CreateNewObjectFromNavigationController
		Inherits ShowNavigationItemController
		Private newController As NewObjectViewController
		Private createdDetailView As DetailView
		Private Const CreateNewObjectItemId As String = "CreateNewIssue"
		Private Const CreateNewObjectNavigationItemActiveKey As String = "CreationAllowed"
		Private Const CreateNewObjectNavigationItemDefaultPath As String = "Issues/" & CreateNewObjectItemId
		Public Sub New()
			TargetWindowType = WindowType.Main
		End Sub
		Protected Overrides Sub InitializeItems()
			MyBase.InitializeItems()
			Dim newNavigationItem As ChoiceActionItem = FindNewNavigationItem()
			If newNavigationItem IsNot Nothing Then
				newNavigationItem.Active(CreateNewObjectNavigationItemActiveKey) = SecuritySystem.IsGranted(New ObjectAccessPermission(GetType(DemoIssue), ObjectAccess.Create))
			End If
		End Sub
		Protected Overrides Sub ShowNavigationItem(ByVal e As SingleChoiceActionExecuteEventArgs)
			If (e.SelectedChoiceActionItem IsNot Nothing) AndAlso e.SelectedChoiceActionItem.Enabled.ResultValue AndAlso e.SelectedChoiceActionItem.Id = CreateNewObjectItemId Then
				Dim workFrame As Frame = Application.CreateFrame(TemplateContext.ApplicationWindow)
				workFrame.SetView(Application.CreateListView(Application.CreateObjectSpace(), GetType(DemoIssue), True))
				newController = workFrame.GetController(Of NewObjectViewController)()
				If newController IsNot Nothing Then
					Dim newObjectItem As ChoiceActionItem = FindNewObjectItem()
					If newObjectItem IsNot Nothing Then
						AddHandler newController.NewObjectAction.Executed, AddressOf NewObjectAction_Executed
						newController.NewObjectAction.DoExecute(newObjectItem)
						RemoveHandler newController.NewObjectAction.Executed, AddressOf NewObjectAction_Executed
						e.ShowViewParameters.TargetWindow = TargetWindow.Default
						e.ShowViewParameters.CreatedView = createdDetailView
						'Cancel the default processing for this navigation item.
						Return
					End If
				End If
			End If
			'Continue the default processing for other navigation items.
			MyBase.ShowNavigationItem(e)
		End Sub
		Private Function FindNewObjectItem() As ChoiceActionItem
			For Each item As ChoiceActionItem In newController.NewObjectAction.Items
				If item.Data Is GetType(DemoIssue) Then
					Return item
				End If
			Next item
			Return Nothing
		End Function
		Private Function FindNewNavigationItem() As ChoiceActionItem
			Return ShowNavigationItemAction.FindItemByIdPath(CreateNewObjectNavigationItemDefaultPath)
		End Function
		Private Sub NewObjectAction_Executed(ByVal sender As Object, ByVal e As ActionBaseEventArgs)
			createdDetailView = TryCast(e.ShowViewParameters.CreatedView, DetailView)
			'Cancel showing the default View by the NewObjectAction
			e.ShowViewParameters.CreatedView = Nothing
		End Sub
	End Class

End Namespace
