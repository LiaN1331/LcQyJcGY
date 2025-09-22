// 代码生成时间: 2025-09-22 23:35:31
// <copyright file="ExcelGenerator.cs" company="YourCompany">
//     Copyright (c) YourCompany. All rights reserved.
// </copyright>

using System;
using System.IO;
using System.Data;
using ClosedXML.Excel;

namespace YourCompany
{
    /// <summary>
    /// A class responsible for generating Excel files.
    /// </summary>
    public class ExcelGenerator
    {
        private readonly string _filePath;

        /// <summary>
        /// Initializes a new instance of the ExcelGenerator class.
        /// </summary>
        /// <param name="filePath">The file path to save the generated Excel file.</param>
        public ExcelGenerator(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        /// <summary>
        /// Generates an Excel file with the provided data.
        /// </summary>
        /// <param name="dataTables">A list of DataTables to be written into the Excel file.</param>
        public void GenerateExcel(IEnumerable<DataTable> dataTables)
        {
            if (dataTables == null) throw new ArgumentNullException(nameof(dataTables));

            using (var workbook = new XLWorkbook())
            {
                foreach (var dataTable in dataTables)
                {
                    // Add a worksheet for each DataTable
                    var worksheet = workbook.Worksheets.Add(dataTable.TableName);

                    // Write the data from the DataTable to the worksheet
                    // Handle potential issues with the DataTable not having a primary key
                    worksheet.Row(1).LoadFromDataTable(dataTable, TableStyles.None);
                }

                // Save the workbook to the specified file path
                try
                {
                    workbook.SaveAs(_filePath);
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("Failed to save the Excel file.", ex);
                }
            }
        }

        /// <summary>
        /// Generates an Excel file with a single DataTable.
        /// </summary>
        /// <param name="dataTable">The DataTable to be written into the Excel file.</param>
        public void GenerateExcel(DataTable dataTable)
        {
            GenerateExcel(new[] { dataTable });
        }
    }
}
