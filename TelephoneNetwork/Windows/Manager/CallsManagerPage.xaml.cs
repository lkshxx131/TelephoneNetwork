using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using TelephoneNetwork.EF;
using System.Data;

namespace TelephoneNetwork.Windows.Manager
{
    /// <summary>
    /// Логика взаимодействия для CallsManagerPage.xaml
    /// </summary>
    public partial class CallsManagerPage : Page
    {
        List<CallsView> callsViews = new List<CallsView>(EntEF.Context.CallsView.ToList());
        public CallsManagerPage()
        {
            InitializeComponent();
            lvCalls.ItemsSource = callsViews;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Content = null;
        }

        
       

        private void btnExportCalls_Click(object sender, RoutedEventArgs e)
        {
            //ExCalls exCalls = new ExCalls();
            //exCalls.ExportDataSetToExel("");
        }
    }
    public  class  ExCalls
        {
            static void CallsPage(string[] args)
    {
        ExCalls exCalls = new ExCalls();

        DataTable callsTable = new DataTable("Calls");
        callsTable.Columns.Add("NumberIn");
        callsTable.Columns.Add("NumberOut");
        callsTable.Columns.Add("DurationInMinute");
        callsTable.Columns.Add("CallsDate");
        callsTable.Rows.Add("89172244581", "89178922440", "42", "15.02.2022");


        DataSet dataSet = new DataSet("Сводка звонков");
        dataSet.Tables.Add(callsTable);

        exCalls.ExportDataSetToExel(dataSet);
    }

    public void ExportDataSetToExel(DataSet dataSet)
    {
        Excel.Application excelApp = new Excel.Application();
        excelApp.Visible = true;
        Excel.Workbook excelWorkBook = excelApp.Workbooks.Add(1);

        foreach (DataTable table in dataSet.Tables)
        {
            Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
            excelWorkSheet.Name = table.TableName;


            for (int i = 1; i < table.Columns.Count + 1; i++)
            {
                excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
            }

            for (int j = 0; j < table.Rows.Count; j++)
            {
                for (int k = 0; k < table.Columns.Count; k++)
                {
                    excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                }
            }
        }
    }
}
}
