using CafeManagement.Models.Order;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Font;
using iText.IO.Font;

using System.Text;
using iText.IO.Font.Constants;
using CafeManagement.Interfaces.Services;

namespace CafeManagement.Services.Store
{
    public class ExportBillService:IExportBillService
    {

        public byte[] GenerateInvoicePdf(Order order)
        {
            using var ms = new MemoryStream();
            var writer = new PdfWriter(ms);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            var titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            var title = new Paragraph("HÓA ĐƠN THANH TOÁN")
                .SetFont(titleFont)
                .SetFontSize(16)
                .SetTextAlignment(TextAlignment.CENTER);
            document.Add(title);
            document.Add(new Paragraph("\n"));

            var infoFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            document.Add(new Paragraph($"Số đơn hàng: {order.No}").SetFont(infoFont));
            document.Add(new Paragraph($"Ngày tạo: {order.createdAt:dd/MM/yyyy}").SetFont(infoFont));
            document.Add(new Paragraph($"Khách hàng: {(order.Customer != null ? order.Customer.Name : "Không có")}").SetFont(infoFont));
            document.Add(new Paragraph("\n"));

            Table table = new Table(new float[] { 1, 4, 2, 3 }); 
            table.SetWidth(UnitValue.CreatePercentValue(100));

            // **Header bảng**
            var headerFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            table.AddHeaderCell(new Cell().Add(new Paragraph("STT").SetFont(headerFont)).SetTextAlignment(TextAlignment.CENTER));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Sản phẩm").SetFont(headerFont)).SetTextAlignment(TextAlignment.CENTER));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Số lượng").SetFont(headerFont)).SetTextAlignment(TextAlignment.CENTER));
            table.AddHeaderCell(new Cell().Add(new Paragraph("Giá").SetFont(headerFont)).SetTextAlignment(TextAlignment.CENTER));


            int index = 1;
            foreach (var detail in order.Details)
            {
                table.AddCell(new Cell().Add(new Paragraph(index.ToString()).SetFont(infoFont)).SetTextAlignment(TextAlignment.CENTER));
                table.AddCell(new Cell().Add(new Paragraph(detail.Product.Name).SetFont(infoFont)).SetTextAlignment(TextAlignment.LEFT));
                table.AddCell(new Cell().Add(new Paragraph(detail.Quantity.ToString()).SetFont(infoFont)).SetTextAlignment(TextAlignment.CENTER));
                table.AddCell(new Cell().Add(new Paragraph($"{detail.Product.Price:C}").SetFont(infoFont)).SetTextAlignment(TextAlignment.RIGHT));
                index++;
            }

            document.Add(table);
            document.Add(new Paragraph("\n"));


            document.Add(new Paragraph($"Tổng cộng: {order.Price:C}").SetFont(infoFont));
            if (order.Promotion != null)
            {
                document.Add(new Paragraph($"Khuyến mãi: {order.Promotion.Name} - Giảm {order.Promotion.Discount}%").SetFont(infoFont));
            }
            document.Add(new Paragraph($"Trạng thái đơn hàng: {order.OrderStatus}").SetFont(infoFont));

            document.Close();
            return ms.ToArray();
        }
    }
}
