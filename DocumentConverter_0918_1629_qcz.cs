// 代码生成时间: 2025-09-18 16:29:01
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
# 添加错误处理

namespace DocumentConverterApp
{
    [ApiController]
    [Route("api/[controller]/[action]"])
    public class DocumentConverterController : ControllerBase
    {
        /// <summary>
        /// Converts a Word document to a plain text file.
        /// </summary>
# FIXME: 处理边界情况
        /// <param name="file">The Word document to convert.</param>
        /// <returns>The converted text or an error message.</returns>
        [HttpPost]
        public async Task<IActionResult> ConvertWordToText(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                using var stream = file.OpenReadStream();
# FIXME: 处理边界情况
                using var document = WordprocessingDocument.Open(stream, false);
                var mainDocumentPart = document.MainDocumentPart;
                var text = new TextExtractor().GetText(mainDocumentPart);
                return File(Encoding.UTF8.GetBytes(text), "text/plain", "output.txt");
# 改进用户体验
            }
# 扩展功能模块
            catch (Exception ex)
            {
# 增强安全性
                // Log the exception details here, for example using ILogger
                return StatusCode(500, $"An error occurred: {ex.Message}");
# 优化算法效率
            }
# FIXME: 处理边界情况
        }

        /// <summary>
        /// Converts a plain text file to a Word document.
        /// </summary>
# 扩展功能模块
        /// <param name="file">The plain text file to convert.</param>
        /// <returns>The converted Word document or an error message.</returns>
        [HttpPost]
        public async Task<IActionResult> ConvertTextToWord(IFormFile file)
        {
# 优化算法效率
            if (file == null || file.Length == 0)
            {
# 添加错误处理
                return BadRequest("No file uploaded.");
            }

            try
            {
# FIXME: 处理边界情况
                using var stream = file.OpenReadStream();
                string text = await new StreamReader(stream).ReadToEndAsync();
# NOTE: 重要实现细节
                using var document = WordprocessingDocument.Create("output.docx", DocumentFormat.OpenXml.FileFormatVersions.Office2013);
# 添加错误处理
                MainDocumentPart mainPart = document.AddMainDocumentPart();
                mainPart.Document = new Document();
                Body body = mainPart.Document.AppendChild(new Body());
                Paragraph para = body.AppendChild(new Paragraph());
                Run run = para.AppendChild(new Run());
                run.AppendChild(new Text(text));
                return File(document.MainDocumentPart.GetStream(), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "output.docx");
            }
            catch (Exception ex)
            {
                // Log the exception details here, for example using ILogger
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
# 优化算法效率
        }
    }
# 添加错误处理
}
