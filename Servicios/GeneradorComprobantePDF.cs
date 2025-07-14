using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AutoGestion.Servicios.Pdf
{
    public static class GeneradorComprobantePDF
    {
        // Genera un comprobante de entrega en formato PDF .

        public static void Generar(Venta venta, string rutaDestino)
        {
            // 1) Crear documento y escritor
            using var fs = new FileStream(rutaDestino, FileMode.Create, FileAccess.Write);
            var doc = new Document(PageSize.A4, 36, 36, 54, 54);
            PdfWriter.GetInstance(doc, fs);
            doc.Open();

            // 2) Fuentes
            var fuenteTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            var fuenteSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTablaHeader = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

            // 3) Cabecera: logo + nombre de la empresa
            var tablaCabecera = new PdfPTable(2) { WidthPercentage = 100 };
            tablaCabecera.SetWidths(new[] { 70f, 30f });

            // 3.1) Empresa
            var celdaEmpresa = new PdfPCell();
            celdaEmpresa.Border = Rectangle.NO_BORDER;
            celdaEmpresa.AddElement(new Paragraph("AutoGestion S.A.", fuenteTitulo));
            celdaEmpresa.AddElement(new Paragraph("Recarte 471, Monte Grande", fuenteNormal));
            celdaEmpresa.AddElement(new Paragraph("Tel: (011) 3252-6503", fuenteNormal));
            tablaCabecera.AddCell(celdaEmpresa);

            // 3.2) Logo
            var celdaLogo = new PdfPCell();
            celdaLogo.Border = Rectangle.NO_BORDER;
            try
            {
                var logo = Image.GetInstance(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.png"));
                logo.ScaleToFit(100f, 50f);
                celdaLogo.AddElement(logo);
            }
            catch
            {
                // si no hay logo, dejamos la celda vacía
            }
            tablaCabecera.AddCell(celdaLogo);

            doc.Add(tablaCabecera);
            doc.Add(new Paragraph("\n"));

            // 4) Datos del comprobante y fecha
            var tablaInfo = new PdfPTable(2) { WidthPercentage = 100 };
            tablaInfo.SetWidths(new[] { 50f, 50f });

            // 4.1) Datos del cliente
            var celdaCliente = new PdfPCell(new Phrase("PARA:", fuenteSubtitulo))
            {
                Border = Rectangle.NO_BORDER,
                PaddingBottom = 4f
            };
            tablaInfo.AddCell(celdaCliente);

            // 4.2) Datos del comprobante
            var celdaComp = new PdfPCell(new Phrase("COMPROBANTE DE ENTREGA", fuenteSubtitulo))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                PaddingBottom = 4f
            };
            tablaInfo.AddCell(celdaComp);

            // 4.3) Cliente: nombre y contacto
            var infoCliente = $"{venta.Cliente.Nombre} {venta.Cliente.Apellido}\nContacto: {venta.Cliente.Contacto}";
            var celdaDatosCli = new PdfPCell(new Phrase(infoCliente, fuenteNormal))
            {
                Border = Rectangle.NO_BORDER,
                PaddingBottom = 8f
            };
            tablaInfo.AddCell(celdaDatosCli);

            // 4.4) Fecha entrega
            var infoComp = $"Fecha entrega: {venta.Pago.FechaPago:dd/MM/yyyy}";
            var celdaDatosComp = new PdfPCell(new Phrase(infoComp, fuenteNormal))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                PaddingBottom = 8f
            };
            tablaInfo.AddCell(celdaDatosComp);

            doc.Add(tablaInfo);
            doc.Add(new Paragraph("\n"));

            // 5) Tabla de detalles
            var tablaItems = new PdfPTable(4) { WidthPercentage = 100 };
            tablaItems.SetWidths(new[] { 40f, 20f, 20f, 20f });

            // Header
            foreach (var header in new[] { "DESCRIPCIÓN", "CANTIDAD", "PRECIO", "IMPORTE" })
            {
                var celda = new PdfPCell(new Phrase(header, fuenteTablaHeader))
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 4f
                };
                tablaItems.AddCell(celda);
            }

            // Fila con datos
            var descripcion = $"Entrega de {venta.Vehiculo.Marca} {venta.Vehiculo.Modelo} ({venta.Vehiculo.Dominio})";
            tablaItems.AddCell(new PdfPCell(new Phrase(descripcion, fuenteNormal)) { Padding = 4f });
            tablaItems.AddCell(new PdfPCell(new Phrase("1", fuenteNormal)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4f });
            tablaItems.AddCell(new PdfPCell(new Phrase($"{venta.Pago.Monto:C2}", fuenteNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });
            tablaItems.AddCell(new PdfPCell(new Phrase($"{venta.Pago.Monto:C2}", fuenteNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });

            doc.Add(tablaItems);
            doc.Add(new Paragraph("\n"));

            // 6) Total
            var tablaTotal = new PdfPTable(2) { WidthPercentage = 40, HorizontalAlignment = Element.ALIGN_RIGHT };
            tablaTotal.SetWidths(new[] { 50f, 50f });

            tablaTotal.AddCell(new PdfPCell(new Phrase("TOTAL:", fuenteSubtitulo))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 4f
            });
            tablaTotal.AddCell(new PdfPCell(new Phrase($"{venta.Pago.Monto:C2}", fuenteSubtitulo))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 4f
            });

            doc.Add(tablaTotal);
            doc.Add(new Paragraph("\n\n"));

            // 7) Pie de página con nota o datos de contacto
            var pie = new Paragraph(
                "Gracias por su compra. Para consultas, llámenos al (011) 15-3252-6503 o escriba a horacio0291@gmail.com",
                FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(pie);

            doc.Close();
        }
    }
}
