Imports DevExpress.DashboardCommon
Imports DevExpress.DataAccess.Excel
Imports DevExpress.XtraEditors

Namespace DXApplication1
	Partial Public Class ViewerForm1
		Inherits XtraForm

		Public Sub New()
			InitializeComponent()

			dashboardViewer.Dashboard = New Dashboard()

			' Creates a data source and adds it to the dashboard data source collection.
			Dim excelDataSource As New DashboardExcelDataSource()
			excelDataSource = CreateExcelDataSource()
			dashboardViewer.Dashboard.DataSources.Add(excelDataSource)

			' Creates a card dashboard item with the specified data source 
			' and adds it to the Items collection to display within the dashboard.
			Dim cards As CardDashboardItem = CreateCards(excelDataSource)
			dashboardViewer.Dashboard.Items.Add(cards)

			' Reloads data in the data sources.
			dashboardViewer.ReloadData()
		End Sub
		Private Function CreateCards(ByVal dataSource As DashboardExcelDataSource) As CardDashboardItem
			' Creates a card dashboard item and specifies its data source.
			Dim cards As New CardDashboardItem()
			cards.DataSource = dataSource

			' Creates the Card object with measures that provide data for calculating actual and target
			' values, and then adds this object to the Cards collection of the card dashboard item.
			Dim card As New Card()
			card.ActualValue = New Measure("RevenueQTD (Sum)")
			card.TargetValue = New Measure("RevenueQTDTarget (Sum)")
			cards.Cards.Add(card)

			' Specifies dimensions that provides data for a card dashboard item series.
			cards.SeriesDimensions.Add(New Dimension("Category"))
			cards.SeriesDimensions.Add(New Dimension("Product"))
			cards.InteractivityOptions.IsDrillDownEnabled = True

			Return cards
		End Function
		Public Function CreateExcelDataSource() As DashboardExcelDataSource
			' Generates the Excel Data Source.
			Dim excelDataSource As New DashboardExcelDataSource()
			excelDataSource.FileName = "Data\Sales.xlsx"
			Dim worksheetSettings As New ExcelWorksheetSettings("Sheet1", "A1:I4166")
			excelDataSource.SourceOptions = New ExcelSourceOptions(worksheetSettings)
			excelDataSource.Fill()

			Return excelDataSource
		End Function
	End Class

End Namespace
