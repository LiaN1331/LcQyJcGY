// 代码生成时间: 2025-09-17 21:53:56
using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace ApiResponseFormatter
{
    // ApiResponseFormatter 是一个用于格式化API响应的工具类
    public static class ApiResponseFormatter
    {
        private static readonly ActionContext ActionContext = new ActionContext();

        // 用于创建成功的API响应
        public static IActionResult CreateSuccessResponse<T>(T data) where T : class
        {
            return CreateResponse(HttpStatusCode.OK, data);
        }

        // 用于创建错误的API响应
        public static IActionResult CreateErrorResponse<T>(T data) where T : class
        {
            return CreateResponse(HttpStatusCode.BadRequest, data);
        }

        // 通用响应创建方法
        private static IActionResult CreateResponse<T>(HttpStatusCode statusCode, T data) where T : class
        {
            var response = new
            {
                success = statusCode == HttpStatusCode.OK,
                statusCode = (int)statusCode,
                data = data,
                message = statusCode == HttpStatusCode.OK ? "Success" : "Error"
            };

            return new ObjectResult(response) { StatusCode = (int)statusCode };
        }

        // 用于创建通用的错误消息响应
        public static IActionResult CreateGenericErrorResponse(string errorMessage)
        {
            var response = new
            {
                success = false,
                statusCode = (int)HttpStatusCode.BadRequest,
                message = errorMessage
            };

            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.BadRequest };
        }
    }
}
