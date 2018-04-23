Imports System
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Xpo

Namespace WinWebSolution.Module
    Public Class XpoDataStoreProxyProvider
        Implements IXpoDataStoreProvider

        Private proxy As XpoDataStoreProxy
        Public Sub New()
            proxy = New XpoDataStoreProxy()
        End Sub
        Public Function CreateUpdatingStore(ByVal allowUpdateSchema As Boolean, <System.Runtime.InteropServices.Out()> ByRef disposableObjects() As IDisposable) As DevExpress.Xpo.DB.IDataStore Implements IXpoDataStoreProvider.CreateUpdatingStore
            disposableObjects = Nothing
            Return proxy
        End Function
        Public Function CreateWorkingStore(<System.Runtime.InteropServices.Out()> ByRef disposableObjects() As IDisposable) As DevExpress.Xpo.DB.IDataStore Implements IXpoDataStoreProvider.CreateWorkingStore

            disposableObjects = Nothing
            Return proxy
        End Function
        Public Function CreateSchemaCheckingStore(<System.Runtime.InteropServices.Out()> ByRef disposableObjects() As IDisposable) As DevExpress.Xpo.DB.IDataStore Implements IXpoDataStoreProvider.CreateSchemaCheckingStore
            disposableObjects = Nothing
            Return proxy
        End Function
        Public ReadOnly Property XPDictionary() As XPDictionary
            Get
                Return Nothing
            End Get
        End Property
        Public ReadOnly Property ConnectionString() As String Implements IXpoDataStoreProvider.ConnectionString
            Get
                Return Nothing
            End Get
        End Property
        Private privateIsInitialized As Boolean
        Public Property IsInitialized() As Boolean
            Get
                Return privateIsInitialized
            End Get
            Private Set(ByVal value As Boolean)
                privateIsInitialized = value
            End Set
        End Property
        Public Sub Initialize(ByVal dictionary As XPDictionary, ByVal legacyConnectionString As String, ByVal tempConnectionString As String)
            proxy.Initialize(dictionary, legacyConnectionString, tempConnectionString)
            IsInitialized = True
        End Sub
    End Class
End Namespace
