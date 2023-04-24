using OfficeOpenXml;
using Spire.Xls;
using System.Data;
using System.Globalization;

namespace XMLToPDF
{
    internal class Program
    {

        static void Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var newReport = new ExcelPackage();

            ExcelWorksheet worksheet;

            using (var report = new ExcelPackage(@"C:\Users\kostr\OneDrive\Робочий стіл\baseTemplate.xlsx"))
            {
                worksheet = newReport.Workbook.Worksheets.Add("newTemplate", report.Workbook.Worksheets[0]);
            }

            var model = MarkReportPrintDto.Instanse;

            // Report block

            //table 1
            worksheet.Cells["C4"].Value = model.Specialization;
            worksheet.Cells["C5"].Value = model.StudyProgram;
            worksheet.Cells["C6"].Value = model.AnnualYear;
            //table 2
            worksheet.Cells["H5"].Value = model.Semester;
            worksheet.Cells["H6"].Value = model.Course;
            worksheet.Cells["H7"].Value = model.GroupName;
            //table 3

            // block date
            worksheet.Cells["D10"].Value = model.Date.ToString("M", CultureInfo.GetCultureInfo("uk-UA"));
            worksheet.Cells["F10"].Value = model.Date.Year + " року";


            worksheet.Cells["E9"].Value = model.Id;
            worksheet.Cells["C11"].Value = model.SubjectComponentTitle;
            worksheet.Cells["D13"].Value = model.EmployeeSubjectName;
            worksheet.Cells["D14"].Value = model.VeryfiEmployeeSubjectName;

            var decan = model.UniversityDecan;
            worksheet.Cells["A49"].Value = $"Директор інституту: {decan} ____________________";

            var employeeSubject = model.EmployeeSubjectName;
            worksheet.Cells["A62"].Value = $"Екзаменатор (викладач): {employeeSubject} ____________________";

            //Mark block
            int cell = 18;
            worksheet.Cells["D18"].Value = model.StudyPlanId;

            foreach (var item in model.Marks)
            {
                worksheet.Cells["D" + cell].Value = model.StudyPlanId;
                worksheet.Cells["B" + cell].Value = item.FullName;
                worksheet.Cells["E" + cell].Value = item.Value;
                ++cell;
            }


            newReport.SaveAs(@"C:\Users\kostr\OneDrive\Робочий стіл\LeshaPussy.xlsx");

            // pdf part

            Workbook convertPdf = new Workbook();

            convertPdf.LoadFromFile(newReport.File.FullName);

            convertPdf.ConverterSetting.SheetFitToPage = true;

            convertPdf.SaveToFile("TestConvertXmlTo3.pdf", FileFormat.PDF);

            Console.WriteLine("Done!");
        }
    }
}