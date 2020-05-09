﻿using System.Net;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace LeesStore.Controllers
{
    [Route("api/[controller]")]
    public class ProductFilesController : AbpController
    {
        [HttpPost]
        [Route("{filename}.xlsx")]
        [ProducesResponseType(typeof(FileContentResult), (int)HttpStatusCode.OK)]
        public ActionResult Download(string fileName)
        {
            var fileMemoryStream = GenerateReportAndWriteToMemoryStream();
            return File(fileMemoryStream,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName + ".xlsx");
        }

        private byte[] GenerateReportAndWriteToMemoryStream()
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Data");
                worksheet.Cells[1, 1].Value = "Hello World";
                return package.GetAsByteArray();
            }
        }
    }
}
