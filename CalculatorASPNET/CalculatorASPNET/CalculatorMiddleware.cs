﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Calculator
{
	public class CalculatorMiddleware
	{
		private readonly RequestDelegate next;
		private readonly string parameterName;

		public CalculatorMiddleware(RequestDelegate next, string parameterName)
		{
			this.next = next;
			this.parameterName = parameterName;
		}
		
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				var a = Calculator.Calculate(context.Request.Query[parameterName]);
				context.Response.Headers.Add("calculator_result", a);
				if (a == Calculator.ErrorString)
					context.Response.StatusCode = 400;
				next.Invoke(context);
			}
			catch (Exception e)
			{
				context.Response.StatusCode = 500;
				Console.WriteLine(e.GetType() + ":" + e.Message + "\n" + e.StackTrace);
			}
		}
	}
}