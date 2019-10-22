//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Data;
//using OfficeOpenXml;
//using System.IO;

//namespace NonProfitCRM.Models
//{
//    public static class ExcelPackageExtns
//    {
//        public static DataTable ToDataTable(this ExcelPackage package)
//        {
//            ExcelWorksheet worksheet = package.Workbook.WorkSheets.First();
//            DataTable dt = new DataTable();
//            foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
//            {
//                dt.Columns.Add(firstRowCell.Text);
//            }
//            for (var rownumber = 2; rownumber <= worksheet.Dimension.End.Row; rownumber++)
//            {
//                var row = worksheet.Cells[rownumber, 1, rownumber, worksheet.Dimension.End.Column];
//                var newrow = dt.NewRow();
//                foreach (var cell in row)
//                {
//                    newrow[cell.Start.Column - 1] = cell.Text;
//                }
//                dt.Rows.Add(newrow);
//            }
//            return dt;
//        }
//    }
//}
