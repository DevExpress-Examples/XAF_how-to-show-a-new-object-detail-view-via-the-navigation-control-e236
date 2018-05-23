Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Persistent.Base
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp

Namespace ShowNewObjectExample.Module
	<DefaultClassOptions, ImageName("BO_Report")> _
	Public Class DemoIssue
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Public Overrides Sub AfterConstruction()
			MyBase.AfterConstruction()
			_CreatedBy = Session.GetObjectByKey(Of SimpleUser)(SecuritySystem.CurrentUserId)
		End Sub
		<Persistent("ModifiedOn"), ValueConverter(GetType(UtcDateTimeConverter))> _
		Protected _ModifiedOn As DateTime = DateTime.Now
		<PersistentAlias("_ModifiedOn"), Custom("EditMask", "G"), Custom("DisplayFormat", "{0:G}")> _
		Public ReadOnly Property ModifiedOn() As DateTime
			Get
				Return _ModifiedOn
			End Get
		End Property
		Friend Overridable Sub UpdateModifiedOn()
			UpdateModifiedOn(DateTime.Now)
		End Sub
		Friend Overridable Sub UpdateModifiedOn(ByVal [date] As DateTime)
			_ModifiedOn = [date]
			Save()
		End Sub
		Protected Overrides Sub OnChanged(ByVal propertyName As String, ByVal oldValue As Object, ByVal newValue As Object)
			MyBase.OnChanged(propertyName, oldValue, newValue)
			If propertyName = "Subject" OrElse propertyName = "Description" Then
				UpdateModifiedOn()
			End If
		End Sub
		Private _Subject As String
		Public Property Subject() As String
			Get
				Return _Subject
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Subject", _Subject, value)
			End Set
		End Property
		Private _Description As String
		Public Property Description() As String
			Get
				Return _Description
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Description", _Description, value)
			End Set
		End Property
		<Persistent("CreatedBy")> _
		Private _CreatedBy As SimpleUser
		<PersistentAlias("_CreatedBy")> _
		Public Property CreatedBy() As SimpleUser
			Get
				Return _CreatedBy
			End Get
			Friend Set(ByVal value As SimpleUser)
				_CreatedBy = value
			End Set
		End Property
	End Class
	Public Class DemoUpdater
		Inherits ModuleUpdater
		Public Sub New(ByVal session As Session, ByVal currentDBVersion As Version)
			MyBase.New(session, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()
			Dim user As New SimpleUser(Session)
			user.UserName = "Sam"
			user.SetPassword("")
			user.Save()
			Dim obj1 As New DemoIssue(Session)
			obj1.Subject = "Issue 3"
			obj1.CreatedBy = Session.FindObject(Of SimpleUser)(Nothing)
			obj1.UpdateModifiedOn()
			obj1.Save()
			Dim obj2 As New DemoIssue(Session)
			obj2.Subject = "Issue 2"
			obj2.CreatedBy = Session.FindObject(Of SimpleUser)(Nothing)
			obj2.UpdateModifiedOn(New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1))
			obj2.Save()
			Dim obj3 As New DemoIssue(Session)
			obj3.Subject = "Issue 1"
			obj3.CreatedBy = Session.FindObject(Of SimpleUser)(Nothing)
			obj3.UpdateModifiedOn(New DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2))
			obj3.Save()
		End Sub
	End Class
End Namespace
