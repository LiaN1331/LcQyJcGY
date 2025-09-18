// 代码生成时间: 2025-09-19 00:44:37
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using ChartJSCore;
using ChartJSCore.Line;

// Controller for interactive chart generator
[RoutePrefix("charts")]
public class InteractiveChartController : Controller
{
    // GET: /charts/generate
    [HttpGet]
    [Route("generate")]
    public ActionResult GenerateChart()
    {
        ViewBag.Title = "Interactive Chart Generator";
        return View();
    }

    // POST: /charts/generate
    [HttpPost]
    [Route("generate")]
    public ActionResult GenerateChart(HttpPostedFileBase file)
    {
        try
        {
            if (file == null || file.ContentLength == 0)
            {
                ModelState.AddModelError("file", "Please upload a file.");
                return View();
            }

            // Assuming the uploaded file is a JSON file containing chart data
            var fileStream = file.InputStream;
            using (var reader = new StreamReader(fileStream))
            {
                var jsonData = reader.ReadToEnd();
                var chartData = JsonConvert.DeserializeObject<List<ChartData>>(jsonData);

                if (chartData == null)
                {
                    ModelState.AddModelError("file", "Invalid chart data format.");
                    return View();
                }

                // Create a new LineChart and configure it
                var chart = new LineChart();
                chart.Labels.AddRange(chartData.Labels);
                chart.Datasets.Clear();
                chart.Datasets.Add(new LineDataset
                {
                    Label = "Sales",
                    Fill = false,
                    BackgroundColor = "rgba(255, 99, 132, 0.2)",
                    BorderColor = "rgb(255, 99, 132)",
                    Data = chartData.Points
                });

                // Convert the chart to a base64 encoded string
                var chartImage = chart.ToBase64();
                var imageUrl = $"data:image/png;base64,{chartImage}";

                // Return the chart image as a result
                ViewBag.ImageUrl = imageUrl;
                return View();
            }
        }
        catch (Exception ex)
        {
            // Log the exception and return an error message
            ModelState.AddModelError("