Imports System
Imports System.Collections.Generic
Imports DevExpress.ExpressApp
Imports System.Reflection
Imports DevExpress.ExpressApp.Xpo
Imports System.Configuration

Namespace WinWebSolution.Module
    Public NotInheritable Partial Class WinWebSolutionModule
        Inherits ModuleBase

        Private Shared provider As XpoDataStoreProxyProvider
        Public Sub New()
            InitializeComponent()
        End Sub
        Public Overrides Sub Setup(ByVal application As XafApplication)
            MyBase.Setup(application)
            AddHandler application.CustomCheckCompatibility, AddressOf application_CustomCheckCompatibility
            AddHandler application.CreateCustomObjectSpaceProvider, AddressOf application_CreateCustomObjectSpaceProvider
        End Sub
        Private Sub application_CreateCustomObjectSpaceProvider(ByVal sender As Object, ByVal e As CreateCustomObjectSpaceProviderEventArgs)
            If provider Is Nothing Then
                provider = New XpoDataStoreProxyProvider()
            End If
            e.ObjectSpaceProvider = New XPObjectSpaceProvider(provider)
        End Sub
        Private Sub application_CustomCheckCompatibility(ByVal sender As Object, ByVal e As CustomCheckCompatibilityEventArgs)
            If provider IsNot Nothing AndAlso (Not provider.IsInitialized) Then
                provider.Initialize(CType(e.ObjectSpaceProvider, XPObjectSpaceProvider).XPDictionary, ConfigurationManager.ConnectionStrings("LegacyDatabaseConnectionString").ConnectionString, ConfigurationManager.ConnectionStrings("TempDatabaseConnectionString").ConnectionString)
            End If
        End Sub
    End Class
End Namespace
