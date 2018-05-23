Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.ExpressApp
Imports DevExpress.Persistent.BaseImpl

Namespace ShowNewObjectExample.Module
	Public Class Updater
		Inherits ModuleUpdater

		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()

			Dim user As SimpleUser = ObjectSpace.FindObject(Of SimpleUser)(CriteriaOperator.Parse("UserName == 'Sam'"))
			If user Is Nothing Then
				user = ObjectSpace.CreateObject(Of SimpleUser)()
				user.UserName = "Sam"
				user.SetPassword("")
			End If

			Dim obj1 As DemoIssue = ObjectSpace.FindObject(Of DemoIssue)(CriteriaOperator.Parse("Subject == 'Issue 1'"))
			If obj1 Is Nothing Then
				obj1 = ObjectSpace.CreateObject(Of DemoIssue)()
				obj1.Subject = "Issue 1"
				obj1.CreatedBy = ObjectSpace.FindObject(Of SimpleUser)(Nothing)
				obj1.UpdateModifiedOn()
			End If

			Dim obj2 As DemoIssue = ObjectSpace.FindObject(Of DemoIssue)(CriteriaOperator.Parse("Subject == 'Issue 2'"))
			If obj1 Is Nothing Then
				obj2 = ObjectSpace.CreateObject(Of DemoIssue)()
				obj2.Subject = "Issue 2"
				obj2.CreatedBy = ObjectSpace.FindObject(Of SimpleUser)(Nothing)
				obj2.UpdateModifiedOn()
			End If


			Dim obj3 As DemoIssue = ObjectSpace.FindObject(Of DemoIssue)(CriteriaOperator.Parse("Subject == 'Issue 3'"))
			If obj1 Is Nothing Then
				obj3 = ObjectSpace.CreateObject(Of DemoIssue)()
				obj3.Subject = "Issue 3"
				obj3.CreatedBy = ObjectSpace.FindObject(Of SimpleUser)(Nothing)
				obj3.UpdateModifiedOn()
			End If

		End Sub
	End Class
End Namespace
