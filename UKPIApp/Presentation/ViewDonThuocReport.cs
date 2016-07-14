using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UKPI.ValueObject;

namespace UKPI.Presentation
{
    public partial class ViewDonThuocReport : Form
    {
        private ThongTinKhamBenhReport reportdt;
        private IHTMLDocument2 doc;
        public ViewDonThuocReport(ThongTinKhamBenhReport reportdt)
        {
            InitializeComponent();
            this.reportdt = reportdt;
            string html = BuildHtml(reportdt);
         //   setBrowserHtml(html);
            DisplayHtml(html);
        }
        public void SetThongTinKhamBenhReport(ThongTinKhamBenhReport reportdt)
        {
            this.reportdt = reportdt;
        }
        private void DisplayHtml(string html)
        {
            webBrowser.DocumentText = "";
            
            doc = webBrowser.Document.DomDocument as IHTMLDocument2;
            doc.defaultCharset = "UTF-8";
            doc.designMode = "On";
            doc.clear();
            doc.writeln(html);
            doc.close();
           // doc.designMode = "Off";
            /*
            webBrowser.Navigate("about:blank");

            if (webBrowser.Document != null)
            {

                webBrowser.Document.Write(string.Empty);

            }

            webBrowser.DocumentText = html;
             */

        }
      
        string BuildHtml(ThongTinKhamBenhReport  reportdt)
        {
            /*
            <!DOCTYPE html>
            <html>
            <body>

            <table border="1" style="width:100%">
              <tr>
                <td>Jill</td>
                <td>Smith</td>		
                <td>50</td>
              </tr>
              <tr>
                <td>Eve</td>
                <td>Jackson</td>		
                <td>94</td>
              </tr>
              <tr>
                <td>John</td>
                <td>Doe</td>		
                <td>80</td>
              </tr>
            </table>

            </body>
            </html>
             */
            StringBuilder html = new StringBuilder();
            string htmlHeader = "<!DOCTYPE html> " + "\n<html>" + "\n<body>";
            string headerText = "<h1 align=\"" + "center" + "\""+ ">ĐƠN THUỐC</h1>";
            string htmlTableHeader = "<table>";
            string htmlTableFooter = "</table>";
            string htmlTableRow1 = "<tr> <td>{0}</td> <td>{1}</td> <td>{2}</td> <td>{3}</td> <td>{4}</td> <td>{5}</td></tr>";
            string htmlTableRow2 = "<tr> <td>{0}</td> <td>{1}</td> <td>{2}</td> <td>{3}</td> </tr>";
            string htmlTableRow3 = "<tr> <td>{0}</td> <td>{1}</td> </tr>";
            string htmlTableRowDetailTenThuoc = "<tr> <td>{0}/{1}</td> </tr>";
            string htmlTableRowDetailHamLuong = "<tr> <td>{0}</td> </tr>";
            string htmlFooter = "\n</body>" + "\n</html>";

            if (reportdt == null)
                return "hello";
            html.AppendLine(htmlHeader);
            html.AppendLine(headerText);
            html.AppendLine(htmlTableHeader);
            html.AppendLine(string.Format(htmlTableRow1, "Họ Tên Người Bệnh:", reportdt.Header.BenhNhan,"Tuổi:",reportdt.Header.Tuoi,"Nam/Nữ:",reportdt.Header.GioiTinh));
            html.AppendLine(string.Format(htmlTableRow2, "Địa Chỉ:", reportdt.Header.DiaChi, "ĐT:", reportdt.Header.DienThoai));
            html.AppendLine(string.Format(htmlTableRow3, "Chuẩn Đoán:", reportdt.Header.ChuanDoan));
            html.AppendLine(htmlTableFooter);
            html.AppendLine("<p><b>Chỉ Định Dùng Thuốc:</b></p>");
            html.AppendLine(htmlTableHeader);
            StringBuilder donThuoc = new StringBuilder();
            for(int i = 1;i<=reportdt.Details.Count;i++)
            {
                donThuoc.AppendLine(string.Format(htmlTableRowDetailTenThuoc, i.ToString(), reportdt.Details[i-1].TenThuoc));
                donThuoc.AppendLine(string.Format(htmlTableRowDetailHamLuong, reportdt.Details[i - 1].HamLuong));
            }
            html.AppendLine(donThuoc.ToString());
            html.AppendLine(htmlTableFooter);
            html.AppendLine(htmlFooter);
            return html.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            mshtml.IHTMLDocument2 doc = (mshtml.IHTMLDocument2)webBrowser.Document.DomDocument;
            doc.execCommand("Print", true, 0);
        }
    }
}
