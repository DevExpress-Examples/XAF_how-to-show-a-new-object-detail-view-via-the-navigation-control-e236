Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Xpo
Imports System.ComponentModel
Imports DevExpress.ExpressApp.Web
Imports DevExpress.ExpressApp.Security

Namespace ShowNewObjectExample.Web
    Partial Public Class ShowNewObjectExampleAspNetApplication
        Inherits WebApplication

        Protected Overrides Sub CreateDefaultObjectSpaceProvider(ByVal args As CreateCustomObjectSpaceProviderEventArgs)
            args.ObjectSpaceProvider = New XPObjectSpaceProviderThreadSafe(args.ConnectionString, args.Connection)
        End Sub
        Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
        Private module2 As DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
        Private module3 As ShowNewObjectExample.Module.ShowNewObjectExampleModule
        'private ShowNewObjectExample.Module.Web.ShowNewObjectExampleAspNetModule module4;
        Private securityModule1 As DevExpress.ExpressApp.Security.SecurityModule
        Private module6 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
        Private securitySimple2 As DevExpress.ExpressApp.Security.SecuritySimple
        Private authenticationStandard1 As DevExpress.ExpressApp.Security.AuthenticationStandard
        Private module5 As DevExpress.ExpressApp.Validation.ValidationModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub ShowNewObjectExampleAspNetApplication_DatabaseVersionMismatch(ByVal sender As Object, ByVal e As DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs) Handles MyBase.DatabaseVersionMismatch
            e.Updater.Update()
            e.Handled = True
        End Sub

        Private Sub InitializeComponent()
            Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule
            Me.module2 = New DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule
            Me.module3 = New ShowNewObjectExample.[Module].ShowNewObjectExampleModule
            Me.module5 = New DevExpress.ExpressApp.Validation.ValidationModule
            Me.module6 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
            Me.securityModule1 = New DevExpress.ExpressApp.Security.SecurityModule
            Me.securitySimple2 = New DevExpress.ExpressApp.Security.SecuritySimple
            Me.authenticationStandard1 = New DevExpress.ExpressApp.Security.AuthenticationStandard
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'module5
            '
            Me.module5.AllowValidationDetailsAccess = True
            '
            'securitySimple2
            '
            Me.securitySimple2.Authentication = Me.authenticationStandard1
            Me.securitySimple2.UserType = GetType(DevExpress.Persistent.BaseImpl.SimpleUser)
            '
            'authenticationStandard1
            '
            Me.authenticationStandard1.LogonParametersType = GetType(DevExpress.ExpressApp.Security.AuthenticationStandardLogonParameters)
            '
            'ShowNewObjectExampleAspNetApplication
            '
            Me.ApplicationName = "ShowNewObjectExample"
            Me.Modules.Add(Me.module1)
            Me.Modules.Add(Me.module2)
            Me.Modules.Add(Me.module6)
            Me.Modules.Add(Me.module3)
            Me.Modules.Add(Me.module5)
            Me.Modules.Add(Me.securityModule1)
            Me.Security = Me.securitySimple2
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub

        Private Sub ShowNewObjectExampleAspNetApplication_LastLogonParametersRead(ByVal sender As System.Object, ByVal e As DevExpress.ExpressApp.LastLogonParametersReadEventArgs) Handles MyBase.LastLogonParametersRead
            ' Just to read demo user name for logon.
            Dim logonParameters As AuthenticationStandardLogonParameters = TryCast(e.LogonObject, AuthenticationStandardLogonParameters)
            If logonParameters IsNot Nothing Then
                If String.IsNullOrEmpty(logonParameters.UserName) Then
                    logonParameters.UserName = "Sam"
                End If
            End If
        End Sub
    End Class
End Namespace
