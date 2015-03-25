using System;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;

namespace HotelCheckIn_BackSystem.DataService.Common
{
    public class NPOIHelper
    {
        private string[] titles;
        private DataTable data;
        public NPOIHelper(string[] titles, DataTable data)
        {
            this.titles = titles;
            this.data = data;
        }

        public Stream ToExcel()
        {
            int rowIndex = 0;

            //创建workbook
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("验证码数据");
            IRow row = sheet.CreateRow(rowIndex);
            row.Height = 200 * 3;

            //表头样式
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.LEFT;//居中对齐
            //表头单元格背景色
            style.FillForegroundColor = HSSFColor.LIGHT_GREEN.index;
            style.FillPattern = FillPatternType.SOLID_FOREGROUND;
            //表头单元格边框
            style.BorderTop = CellBorderType.THIN;
            style.TopBorderColor = HSSFColor.BLACK.index;
            style.BorderRight = CellBorderType.THIN;
            style.RightBorderColor = HSSFColor.BLACK.index;
            style.BorderBottom = CellBorderType.THIN;
            style.BottomBorderColor = HSSFColor.BLACK.index;
            style.BorderLeft = CellBorderType.THIN;
            style.LeftBorderColor = HSSFColor.BLACK.index;
            style.VerticalAlignment = VerticalAlignment.CENTER;
            //表头字体设置
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12;//字号
            font.Boldweight = 600;//加粗
            //font.Color = HSSFColor.WHITE.index;//颜色
            style.SetFont(font);

            //数据样式
            ICellStyle datastyle = workbook.CreateCellStyle();
            datastyle.Alignment = HorizontalAlignment.LEFT;//左对齐
            //数据单元格的边框
            datastyle.BorderTop = CellBorderType.THIN;
            datastyle.TopBorderColor = HSSFColor.BLACK.index;
            datastyle.BorderRight = CellBorderType.THIN;
            datastyle.RightBorderColor = HSSFColor.BLACK.index;
            datastyle.BorderBottom = CellBorderType.THIN;
            datastyle.BottomBorderColor = HSSFColor.BLACK.index;
            datastyle.BorderLeft = CellBorderType.THIN;
            datastyle.LeftBorderColor = HSSFColor.BLACK.index;
            //数据的字体
            IFont datafont = workbook.CreateFont();
            datafont.FontHeightInPoints = 11;//字号
            datastyle.SetFont(datafont);
            //设置列宽
            sheet.SetColumnWidth(0, 20 * 256);

            sheet.DisplayGridlines = true ;

            try
            {
                //表头数据
                for (int i = 0; i < titles.Length; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(titles[i]);
                    cell.CellStyle = style;
                }
                for (int k = 0; k < data.Rows.Count; k++)
                {
                    row = sheet.CreateRow(k + 1);
                    row.Height = 200 * 2;
                    for (int j = 0; j < data.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(data.Rows[k][j].ToString());
                        cell.CellStyle = datastyle;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
                sheet = null;
                row = null;
            }
            return ms;
        }

        public static Stream ToExcel2Sheet(string[][] title, DataTable data1,DataTable data2)
        {

            int rowIndex = 0;

            //创建workbook
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet1 = workbook.CreateSheet("订单信息");
            #region sheet1
            IRow row = sheet1.CreateRow(rowIndex);
            row.Height = 200 * 3;

            //表头样式
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.LEFT;//居中对齐
            //表头单元格背景色
            style.FillForegroundColor = HSSFColor.LIGHT_GREEN.index;
            style.FillPattern = FillPatternType.SOLID_FOREGROUND;
            //表头单元格边框
            style.BorderTop = CellBorderType.THIN;
            style.TopBorderColor = HSSFColor.BLACK.index;
            style.BorderRight = CellBorderType.THIN;
            style.RightBorderColor = HSSFColor.BLACK.index;
            style.BorderBottom = CellBorderType.THIN;
            style.BottomBorderColor = HSSFColor.BLACK.index;
            style.BorderLeft = CellBorderType.THIN;
            style.LeftBorderColor = HSSFColor.BLACK.index;
            style.VerticalAlignment = VerticalAlignment.CENTER;
            //表头字体设置
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12;//字号
            font.Boldweight = 600;//加粗
            //font.Color = HSSFColor.WHITE.index;//颜色
            style.SetFont(font);

            //数据样式
            ICellStyle datastyle = workbook.CreateCellStyle();
            datastyle.Alignment = HorizontalAlignment.LEFT;//左对齐
            //数据单元格的边框
            datastyle.BorderTop = CellBorderType.THIN;
            datastyle.TopBorderColor = HSSFColor.BLACK.index;
            datastyle.BorderRight = CellBorderType.THIN;
            datastyle.RightBorderColor = HSSFColor.BLACK.index;
            datastyle.BorderBottom = CellBorderType.THIN;
            datastyle.BottomBorderColor = HSSFColor.BLACK.index;
            datastyle.BorderLeft = CellBorderType.THIN;
            datastyle.LeftBorderColor = HSSFColor.BLACK.index;
            //数据的字体
            IFont datafont = workbook.CreateFont();
            datafont.FontHeightInPoints = 11;//字号
            datastyle.SetFont(datafont);
            //设置列宽
            sheet1.SetColumnWidth(0, 20 * 256);

            sheet1.DisplayGridlines = false;
            #endregion

            #region sheet2

            ISheet sheet2 = workbook.CreateSheet("客人信息");
            int rowIndex2 = 0;
            IRow row2 = sheet2.CreateRow(rowIndex2);
            row2.Height = 200 * 3;

            //表头样式
            ICellStyle style2 = workbook.CreateCellStyle();
            style2.Alignment = HorizontalAlignment.LEFT;//居中对齐
            //表头单元格背景色
            style2.FillForegroundColor = HSSFColor.LIGHT_GREEN.index;
            style2.FillPattern = FillPatternType.SOLID_FOREGROUND;
            //表头单元格边框
            style2.BorderTop = CellBorderType.THIN;
            style2.TopBorderColor = HSSFColor.BLACK.index;
            style2.BorderRight = CellBorderType.THIN;
            style2.RightBorderColor = HSSFColor.BLACK.index;
            style2.BorderBottom = CellBorderType.THIN;
            style2.BottomBorderColor = HSSFColor.BLACK.index;
            style2.BorderLeft = CellBorderType.THIN;
            style2.LeftBorderColor = HSSFColor.BLACK.index;
            style2.VerticalAlignment = VerticalAlignment.CENTER;
            //表头字体设置
            IFont font2 = workbook.CreateFont();
            font2.FontHeightInPoints = 12;//字号
            font2.Boldweight = 600;//加粗
            style2.SetFont(font2);

            //数据样式
            ICellStyle datastyle2 = workbook.CreateCellStyle();
            datastyle2.Alignment = HorizontalAlignment.LEFT;//左对齐
            //数据单元格的边框
            datastyle2.BorderTop = CellBorderType.THIN;
            datastyle2.TopBorderColor = HSSFColor.BLACK.index;
            datastyle2.BorderRight = CellBorderType.THIN;
            datastyle2.RightBorderColor = HSSFColor.BLACK.index;
            datastyle2.BorderBottom = CellBorderType.THIN;
            datastyle2.BottomBorderColor = HSSFColor.BLACK.index;
            datastyle2.BorderLeft = CellBorderType.THIN;
            datastyle2.LeftBorderColor = HSSFColor.BLACK.index;
            //数据的字体
            IFont datafont2 = workbook.CreateFont();
            datafont2.FontHeightInPoints = 11;//字号
            datastyle2.SetFont(datafont2);
            //设置列宽
            sheet2.SetColumnWidth(0, 20 * 256);

            sheet2.DisplayGridlines = false;
            #endregion

            try
            {
                //表头数据
                string[] titles = title[0];
                for (int i = 0; i < titles.Length; i++)
                {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(titles[i]);
                    cell.CellStyle = style;
                }
                for (int k = 0; k < data1.Rows.Count; k++)
                {
                    row = sheet1.CreateRow(k + 1);
                    row.Height = 200 * 2;
                    for (int j = 0; j < data1.Columns.Count; j++)
                    {
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(data1.Rows[k][j].ToString());
                        cell.CellStyle = datastyle;
                    }
                }

                //表头数据
                string[] titles2 = title[1];
                for (int i = 0; i < titles2.Length; i++)
                {
                    ICell cell = row2.CreateCell(i);
                    cell.SetCellValue(titles2[i]);
                    cell.CellStyle = style2;
                }
                for (int k = 0; k < data2.Rows.Count; k++)
                {
                    row2 = sheet2.CreateRow(k + 1);
                    row2.Height = 200 * 2;
                    for (int j = 0; j < data2.Columns.Count; j++)
                    {
                        ICell cell = row2.CreateCell(j);
                        cell.SetCellValue(data2.Rows[k][j].ToString());
                        cell.CellStyle = datastyle2;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
                sheet1 = null;
                row = null;
            }
            return ms;
        }
    }
}