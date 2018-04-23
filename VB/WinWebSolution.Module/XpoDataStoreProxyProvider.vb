Imports DevExpress.ExpressApp
Imports DevExpress.Xpo.Metadata
Imports DevExpress.ExpressApp.Xpo

Namespace WinWebSolution.Module
	Public Class XpoDataStoreProxyProvider
		Implements IXpoDataStoreProvider
		Private proxy As XpoDataStoreProxy
		Public Sub New()
			proxy = New XpoDataStoreProxy()
		End Sub
		Public ReadOnly Property ConnectionString() As String Implements IXpoDataStoreProvider.ConnectionString
			Get
				Return ""
			End Get
		End Property
		Public Function CreateUpdatingStore(<System.Runtime.InteropServices.Out()> ByRef disposableObjects() As IDisposable) As DevExpress.Xpo.DB.IDataStore Implements IXpoDataStoreProvider.CreateUpdatingStore
			disposableObjects = Nothing
			Return proxy
		End Function
		Public Function CreateWorkingStore(<System.Runtime.InteropServices.Out()> ByRef disposableObjects() As IDisposable) As DevExpress.Xpo.DB.IDataStore Implements IXpoDataStoreProvider.CreateWorkingStore
			disposableObjects = Nothing
			Return proxy
		End Function
		Public ReadOnly Property XPDictionary() As XPDictionary
			Get
				Return Nothing
			End Get
		End Property
'INSTANT VB NOTE: The variable isInitialized was renamed since Visual Basic does not allow class members with the same name:
		Private isInitialized_Renamed As Boolean
		Public ReadOnly Property IsInitialized() As Boolean
			Get
				Return isInitialized_Renamed
			End Get
		End Property
		Public Sub Initialize(ByVal dictionary As XPDictionary, ByVal legacyConnectionString As String, ByVal tempConnectionString As String)
			proxy.Initialize(dictionary, legacyConnectionString, tempConnectionString)
			isInitialized_Renamed = True
		End Sub
	End Class
End Namespace
