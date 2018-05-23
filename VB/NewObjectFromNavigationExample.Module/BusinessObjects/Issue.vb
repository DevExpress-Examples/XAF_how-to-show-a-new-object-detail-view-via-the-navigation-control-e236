Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo
Imports DevExpress.Persistent.Base

Namespace NewObjectFromNavigationExample.Module.BusinessObjects
	<DefaultClassOptions, ImageName("BO_List")> _
	Public Class Issue
		Inherits BaseObject
		Public Sub New(ByVal session As Session)
			MyBase.New(session)
		End Sub
		Private subject_Renamed As String
		Public Property Subject() As String
			Get
				Return subject_Renamed
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Subject", subject_Renamed, value)
			End Set
		End Property
		Private description_Renamed As String
		<Size(SizeAttribute.Unlimited)> _
		Public Property Description() As String
			Get
				Return description_Renamed
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Description", description_Renamed, value)
			End Set
		End Property
	End Class
End Namespace
