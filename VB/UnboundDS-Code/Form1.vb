Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.Windows.Forms
Imports DevExpress.Data

Namespace UnboundDS_Code
    Partial Public Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            InitializeComponent()
            gridView1.OptionsBehavior.AutoPopulateColumns = False
            CreateDS()
            CreateGridColumns()
            gridControl1.UseEmbeddedNavigator = True

        End Sub

        Public Sub CreateDS()
            Dim unboundDS As New UnboundSource()
            Dim unboundSourceProperty1 As New UnboundSourceProperty() With {.DisplayName = "ID", .Name = "Int", .PropertyType = GetType(Integer)}
            Dim unboundSourceProperty2 As New UnboundSourceProperty() With {.DisplayName = "Day of Week", .Name = "String", .PropertyType = GetType(String)}
            unboundDS.Properties.AddRange(New DevExpress.Data.UnboundSourceProperty() { unboundSourceProperty1, unboundSourceProperty2})
            AddHandler unboundDS.ValueNeeded, AddressOf unboundDS_ValueNeeded
            AddHandler unboundDS.ValuePushed, AddressOf unboundDS_ValuePushed
            unboundDS.SetRowCount(100000)
            gridControl1.DataSource = unboundDS
        End Sub

        Private Sub unboundDS_ValuePushed(ByVal sender As Object, ByVal e As UnboundSourceValuePushedEventArgs)
            Dim defaultValue = GetDefaultData(e.RowIndex, e.PropertyName)
            Dim cellKey = New CellKey(e.RowIndex, e.PropertyName)
            If Object.Equals(defaultValue, e.Value) Then
                Me.Differences.Remove(cellKey)
            Else
                Me.Differences(cellKey) = e.Value
            End If
        End Sub

        Private Sub unboundDS_ValueNeeded(ByVal sender As Object, ByVal e As UnboundSourceValueNeededEventArgs)
            If Me.Differences.Count > 0 Then
                Dim rv As Object = Nothing
                If Me.Differences.TryGetValue(New CellKey(e.RowIndex, e.PropertyName), rv) Then
                    e.Value = rv
                    Return
                End If
            End If
            e.Value = GetDefaultData(e.RowIndex, e.PropertyName)
            GetHashCode()
        End Sub


        Public Sub CreateGridColumns()
            gridView1.Columns.AddVisible("Int")
            gridView1.Columns.AddVisible("String")
        End Sub

        Private Structure CellKey
            Implements IEquatable(Of CellKey)
            Private rowIndex_Renamed As Integer
            Private propertyName_Renamed As String
            Public ReadOnly Property RowIndex() As Integer
                Get
                    Return rowIndex_Renamed
                End Get
            End Property
            Public ReadOnly Property PropertyName() As String
                Get
                    Return propertyName_Renamed
                End Get
            End Property
            Public Sub New(ByVal rowIndex As Integer, ByVal propertyName As String)
                Me.rowIndex_Renamed = rowIndex
                Me.propertyName_Renamed = propertyName
            End Sub
            Public Overloads Function Equals(ByVal other As CellKey) As Boolean Implements IEquatable(Of CellKey).Equals
                Return Me.RowIndex = other.RowIndex AndAlso Me.PropertyName = other.PropertyName
            End Function
            Public Overrides Function GetHashCode() As Integer
                Return RowIndex * 257 + Convert.ToInt32((If(String.IsNullOrEmpty(Me.PropertyName), 0, Me.PropertyName.Chars(0))))
            End Function
            Public Overrides Function Equals(ByVal obj As Object) As Boolean
                If TypeOf obj Is CellKey Then
                    Return Equals(DirectCast(obj, CellKey))
                Else
                    Return False
                End If
            End Function
        End Structure

        Private ReadOnly DefaultStringData() As String = CultureInfo.CurrentCulture.DateTimeFormat.DayNames
        Private ReadOnly Differences As New Dictionary(Of CellKey, Object)()
        Private Function GetDefaultData(ByVal rowIndex As Integer, ByVal propertyName As String) As Object
            Select Case propertyName
                Case "String"
                    Return DefaultStringData(rowIndex Mod DefaultStringData.Length)
                Case "Int"
                    Return rowIndex + 1
                Case Else
                    Return Nothing
            End Select
        End Function
    End Class
End Namespace
