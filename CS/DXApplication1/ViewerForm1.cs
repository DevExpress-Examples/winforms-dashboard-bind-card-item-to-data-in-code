using DevExpress.DashboardCommon;
using DevExpress.DataAccess.Excel;
using DevExpress.XtraEditors;

namespace DXApplication1
{
    public partial class ViewerForm1 : XtraForm
    {
        public ViewerForm1()
        {
            InitializeComponent();
            
            dashboardViewer.Dashboard = new Dashboard();

            // Creates a data source and adds it to the dashboard data source collection.
            DashboardExcelDataSource excelDataSource = new DashboardExcelDataSource();
            excelDataSource = CreateExcelDataSource();
            dashboardViewer.Dashboard.DataSources.Add(excelDataSource);

            // Creates a card dashboard item with the specified data source 
            // and adds it to the Items collection to display within the dashboard.
            CardDashboardItem cards = CreateCards(excelDataSource);
            dashboardViewer.Dashboard.Items.Add(cards);

            // Reloads data in the data sources.
            dashboardViewer.ReloadData();
        }
        private CardDashboardItem CreateCards(DashboardExcelDataSource dataSource)
        {
            // Creates a card dashboard item and specifies its data source.
            CardDashboardItem cards = new CardDashboardItem();
            cards.DataSource = dataSource;

            // Creates the Card object with measures that provide data for calculating actual and target
            // values, and then adds this object to the Cards collection of the card dashboard item.
            Card card = new Card();
            card.ActualValue = new Measure("RevenueQTD (Sum)");
            card.TargetValue = new Measure("RevenueQTDTarget (Sum)");
            cards.Cards.Add(card);

            // Specifies dimensions that provides data for a card dashboard item series.
            cards.SeriesDimensions.Add(new Dimension("Category"));
            cards.SeriesDimensions.Add(new Dimension("Product"));
            cards.InteractivityOptions.IsDrillDownEnabled = true;

            return cards;
        }
        public DashboardExcelDataSource CreateExcelDataSource()
        {
            // Generates the Excel Data Source.
            DashboardExcelDataSource excelDataSource = new DashboardExcelDataSource();
            excelDataSource.FileName = @"Data\Sales.xlsx";
            ExcelWorksheetSettings worksheetSettings = new ExcelWorksheetSettings("Sheet1", "A1:I4166");
            excelDataSource.SourceOptions = new ExcelSourceOptions(worksheetSettings);
            excelDataSource.Fill();

            return excelDataSource;
        }
    }
    
}
