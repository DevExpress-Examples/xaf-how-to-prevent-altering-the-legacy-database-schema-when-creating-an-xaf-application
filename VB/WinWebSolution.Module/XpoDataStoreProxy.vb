Imports DevExpress.Xpo.DB
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Xpo.Helpers

Namespace WinWebSolution.Module

    Public Class XpoDataStoreProxy
        Implements IDataStore, ICommandChannel

        Private legacyDataLayer As SimpleDataLayer

        Private legacyDataStore As IDataStore

        Private tempDataLayer As SimpleDataLayer

        Private tempDataStore As IDataStore

        Private tempDatabaseTables As String() = New String() {"ModuleInfo", "XPObjectType"}

        Private Function IsTempDatabaseTable(ByVal tableName As String) As Boolean
            If Not String.IsNullOrEmpty(tableName) Then
                For Each currentTableName As String In tempDatabaseTables
                    If tableName.EndsWith(currentTableName) Then
                        Return True
                    End If
                Next
            End If

            Return False
        End Function

        Public Sub Initialize(ByVal dictionary As XPDictionary, ByVal legacyConnectionString As String, ByVal tempConnectionString As String)
            Dim legacyDictionary As ReflectionDictionary = New ReflectionDictionary()
            Dim tempDictionary As ReflectionDictionary = New ReflectionDictionary()
            For Each ci As XPClassInfo In dictionary.Classes
                If Not IsTempDatabaseTable(ci.TableName) Then
                    legacyDictionary.QueryClassInfo(ci.ClassType)
                Else
                    tempDictionary.QueryClassInfo(ci.ClassType)
                End If
            Next

            legacyDataStore = XpoDefault.GetConnectionProvider(legacyConnectionString, AutoCreateOption.DatabaseAndSchema)
            legacyDataLayer = New SimpleDataLayer(legacyDictionary, legacyDataStore)
            tempDataStore = XpoDefault.GetConnectionProvider(tempConnectionString, AutoCreateOption.DatabaseAndSchema)
            tempDataLayer = New SimpleDataLayer(tempDictionary, tempDataStore)
        End Sub

        Public ReadOnly Property AutoCreateOption As AutoCreateOption Implements IDataStore.AutoCreateOption
            Get
                Return AutoCreateOption.DatabaseAndSchema
            End Get
        End Property

        Public Function ModifyData(ParamArray dmlStatements As ModificationStatement()) As ModificationResult Implements IDataStore.ModifyData
            Dim legacyChanges As List(Of ModificationStatement) = New List(Of ModificationStatement)(dmlStatements.Length)
            Dim tempChanges As List(Of ModificationStatement) = New List(Of ModificationStatement)(dmlStatements.Length)
            For Each stm As ModificationStatement In dmlStatements
                If IsTempDatabaseTable(stm.TableName) Then
                    tempChanges.Add(stm)
                Else
                    legacyChanges.Add(stm)
                End If
            Next

            Dim resultSet As List(Of ParameterValue) = New List(Of ParameterValue)()
            If legacyChanges.Count > 0 Then
                resultSet.AddRange(legacyDataLayer.ModifyData(legacyChanges.ToArray()).Identities)
            End If

            If tempChanges.Count > 0 Then
                resultSet.AddRange(tempDataLayer.ModifyData(tempChanges.ToArray()).Identities)
            End If

            Return New ModificationResult(resultSet)
        End Function

        Public Function SelectData(ParamArray selects As SelectStatement()) As SelectedData Implements IDataStore.SelectData
            Dim isExternals = selects.[Select](Function(stmt) IsTempDatabaseTable(stmt.TableName)).ToList()
            Dim mainSelects As List(Of SelectStatement) = New List(Of SelectStatement)(selects.Length)
            Dim externalSelects As List(Of SelectStatement) = New List(Of SelectStatement)(selects.Length)
            Dim i As Integer = 0
            While i < isExternals.Count
                Call(If(isExternals(i), externalSelects, mainSelects)).Add(selects(i))
                System.Threading.Interlocked.Increment(i)
            End While

            Dim externalResults =(If(externalSelects.Count = 0, Enumerable.Empty(Of SelectStatementResult)(), tempDataLayer.SelectData(externalSelects.ToArray()).ResultSet)).GetEnumerator()
            Dim mainResults =(If(mainSelects.Count = 0, Enumerable.Empty(Of SelectStatementResult)(), legacyDataLayer.SelectData(mainSelects.ToArray()).ResultSet)).GetEnumerator()
            Dim results As SelectStatementResult() = New SelectStatementResult(isExternals.Count - 1) {}
            Dim i As Integer = 0
            While i < results.Length
                Dim enumerator = If(isExternals(i), externalResults, mainResults)
                enumerator.MoveNext()
                results(i) = enumerator.Current
                System.Threading.Interlocked.Increment(i)
            End While

            Return New SelectedData(results)
        End Function

        Public Function UpdateSchema(ByVal dontCreateIfFirstTableNotExist As Boolean, ParamArray tables As DBTable()) As UpdateSchemaResult Implements IDataStore.UpdateSchema
            Dim db1Tables As List(Of DBTable) = New List(Of DBTable)()
            Dim db2Tables As List(Of DBTable) = New List(Of DBTable)()
            For Each table As DBTable In tables
                If Not IsTempDatabaseTable(table.Name) Then
                    db1Tables.Add(table)
                Else
                    db2Tables.Add(table)
                End If
            Next

            legacyDataStore.UpdateSchema(False, db1Tables.ToArray())
            tempDataStore.UpdateSchema(False, db2Tables.ToArray())
            Return UpdateSchemaResult.SchemaExists
        End Function

        Public Function [Do](ByVal command As String, ByVal args As Object) As Object Implements ICommandChannel.[Do]
            Return CType(legacyDataLayer, ICommandChannel).Do(command, args)
        End Function
    End Class
End Namespace
