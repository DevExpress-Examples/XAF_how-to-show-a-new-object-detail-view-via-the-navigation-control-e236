Imports Microsoft.VisualBasic
Imports DevExpress.ExpressApp.Model
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
		<PersistentAlias("_ModifiedOn"), ModelDefault("EditMask", "G"), ModelDefault("DisplayFormat", "{0:G}")> _
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
'    public class DemoUpdater : ModuleUpdater {
'        public DemoUpdater(Session session, Version currentDBVersion) : base(session, currentDBVersion) { }
'        public override void UpdateDatabaseAfterUpdateSchema() {
'            base.UpdateDatabaseAfterUpdateSchema();
'            SimpleUser user = new SimpleUser(Session);
'            user.UserName = "Sam";
'            user.SetPassword("");
'            user.Save();
'            DemoIssue obj1 = new DemoIssue(Session);
'            obj1.Subject = "Issue 3";
'            obj1.CreatedBy = Session.FindObject<SimpleUser>(null);
'            obj1.UpdateModifiedOn();
'            obj1.Save();
'            DemoIssue obj2 = new DemoIssue(Session);
'            obj2.Subject = "Issue 2";
'            obj2.CreatedBy = Session.FindObject<SimpleUser>(null);
'            obj2.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1));
'            obj2.Save();
'            DemoIssue obj3 = new DemoIssue(Session);
'            obj3.Subject = "Issue 1";
'            obj3.CreatedBy = Session.FindObject<SimpleUser>(null);
'            obj3.UpdateModifiedOn(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2));
'            obj3.Save();
'        }
'    }
End Namespace
