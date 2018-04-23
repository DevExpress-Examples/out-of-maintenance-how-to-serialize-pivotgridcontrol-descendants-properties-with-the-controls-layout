Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraPivotGrid
Imports System.IO
Imports DevExpress.Utils
Imports System.Collections
Imports DevExpress.XtraPivotGrid.Data
Imports DevExpress.XtraEditors
Imports DevExpress.XtraEditors.Filtering
Imports DevExpress.Skins
Imports DevExpress.Utils.Drawing
Imports System.Diagnostics
Imports DevExpress.XtraPivotGrid.ViewInfo
Imports DevExpress.Utils.Serializing
Imports DevExpress.Utils.Serializing.Helpers

Namespace WindowsFormsSample
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			pivotGridControl1.Objects.Add(New Class1() With {.String = "test1"})
			pivotGridControl1.Objects.Add(New Class1() With {.String = "test2"})
			pivotGridControl1.Objects.Add(New Class1() With {.String = "test3"})
			pivotGridControl1.Object.String = "test4"
			pivotGridControl1.SaveLayoutToXml("test.xml")
			pivotGridControl1.Objects.Clear()
			pivotGridControl1.Object.String = Nothing
			pivotGridControl1.RestoreLayoutFromXml("test.xml")
		End Sub
	End Class
	Public Class MyPivotGrid
		Inherits PivotGridControl
		Public ReadOnly Property Objects() As List(Of Class1)
			Get
				Return Data.Objects
			End Get
		End Property
		Public ReadOnly Property [Object]() As Class1
			Get
				Return Data.Object
			End Get
		End Property
		Protected Overrides Function CreateData() As PivotGridViewInfoData
			Return New MyPivotGridViewInfoData(Me)
		End Function
		Public Shadows ReadOnly Property Data() As MyPivotGridViewInfoData
			Get
				Return TryCast(MyBase.Data, MyPivotGridViewInfoData)
			End Get
		End Property
	End Class
	Public Class MyPivotGridViewInfoData
		Inherits PivotGridViewInfoData
		Implements IXtraSupportDeserializeCollectionItem
		Public Sub New(ByVal control As IViewInfoControl)
			MyBase.New(control)
			_Objects = New List(Of Class1)()
		End Sub
		Private _Objects As List(Of Class1)
		<XtraSerializableProperty(XtraSerializationVisibility.Collection, True)> _
		Public ReadOnly Property Objects() As List(Of Class1)
			Get
				Return _Objects
			End Get
		End Property
		Private _Object As New Class1()
		<XtraSerializableProperty(XtraSerializationVisibility.Content)> _
		Public ReadOnly Property [Object]() As Class1
			Get
				Return _Object
			End Get
		End Property

		#Region "IXtraSupportDeserializeCollectionItem Members"

		Private Function CreateCollectionItem(ByVal propertyName As String, ByVal e As XtraItemEventArgs) As Object Implements IXtraSupportDeserializeCollectionItem.CreateCollectionItem
			If propertyName = "Objects" Then
				Dim class1 As New Class1()
				Objects.Add(class1)
				Return class1
			Else
				Return Nothing
			End If
		End Function

		Private Sub SetIndexCollectionItem(ByVal propertyName As String, ByVal e As XtraSetItemIndexEventArgs) Implements IXtraSupportDeserializeCollectionItem.SetIndexCollectionItem

		End Sub

		#End Region
	End Class
	Public Class Class1
		Private _String As String
		<XtraSerializableProperty> _
		Public Property [String]() As [String]
			Get
				Return _String
			End Get
			Set(ByVal value As String)
				_String = value
			End Set
		End Property
	End Class
End Namespace
