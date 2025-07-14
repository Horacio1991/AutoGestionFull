using BE;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AutoGestion.Servicios.Pdf
{
    public static class GeneradorFacturaPDF
    {
        public static void Generar(Factura factura, string rutaDestino)
        {
            using var fs = new FileStream(rutaDestino, FileMode.Create, FileAccess.Write);
            var doc = new Document(PageSize.A4, 36, 36, 54, 54);
            PdfWriter.GetInstance(doc, fs);
            doc.Open();

            // Fuentes
            var fTitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
            var fSubtitulo = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
            var fNormal = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fHeaderTab = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

            // 1) Cabecera: nombre de la empresa + logo
            var tablaCab = new PdfPTable(2) { WidthPercentage = 100 };
            tablaCab.SetWidths(new[] { 70f, 30f });

            // Empresa
            var celEmp = new PdfPCell();
            celEmp.Border = Rectangle.NO_BORDER;
            celEmp.AddElement(new Paragraph("AutoGestion S.A.", fTitulo));
            celEmp.AddElement(new Paragraph("Recarte 471, Monte Grande", fNormal));
            celEmp.AddElement(new Paragraph("Tel: (011) 3252-6503", fNormal));
            tablaCab.AddCell(celEmp);

            // Logo
            var celLogo = new PdfPCell { Border = Rectangle.NO_BORDER };
            try
            {
                var logo = Image.GetInstance(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.png"));
                logo.ScaleToFit(100f, 50f);
                celLogo.AddElement(logo);
            }
            catch
            {
                // si no existe logo, dejar espacio en blanco
            }
            tablaCab.AddCell(celLogo);

            doc.Add(tablaCab);
            doc.Add(new Paragraph("\n"));

            // 2) Datos de factura y cliente
            var tablaInfo = new PdfPTable(2) { WidthPercentage = 100 };
            tablaInfo.SetWidths(new[] { 50f, 50f });

            // Izquierda: datos del cliente
            var celCliTitulo = new PdfPCell(new Phrase("FACTURAR A:", fSubtitulo))
            {
                Border = Rectangle.NO_BORDER,
                PaddingBottom = 4f
            };
            tablaInfo.AddCell(celCliTitulo);

            // Derecha: número y fecha
            var celFacInfo = new PdfPCell(new Phrase("FACTURA ELECTRÓNICA", fSubtitulo))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                PaddingBottom = 4f
            };
            tablaInfo.AddCell(celFacInfo);

            var datosCliente = $"{factura.Cliente.Nombre} {factura.Cliente.Apellido}\n" +
                               $"Contacto: {factura.Cliente.Contacto}";
            tablaInfo.AddCell(new PdfPCell(new Phrase(datosCliente, fNormal))
            {
                Border = Rectangle.NO_BORDER,
                PaddingBottom = 8f
            });

            var datosFactura = $"Nº Factura: {factura.ID}\n" +
                               $"Fecha: {factura.Fecha:dd/MM/yyyy}\n" +
                               $"Forma Pago: {factura.FormaPago}";
            tablaInfo.AddCell(new PdfPCell(new Phrase(datosFactura, fNormal))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                PaddingBottom = 8f
            });

            doc.Add(tablaInfo);
            doc.Add(new Paragraph("\n"));

            // 3) Tabla de items (aquí un sólo ítem: el vehículo)
            var tabla = new PdfPTable(4) { WidthPercentage = 100 };
            tabla.SetWidths(new[] { 40f, 20f, 20f, 20f });

            // Headers
            foreach (var h in new[] { "DESCRIPCIÓN", "CANTIDAD", "PRECIO UNIT.", "IMPORTE" })
            {
                tabla.AddCell(new PdfPCell(new Phrase(h, fHeaderTab))
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 4f
                });
            }

            // Fila de datos
            var desc = $"Vehículo: {factura.Vehiculo.Marca} {factura.Vehiculo.Modelo} ({factura.Vehiculo.Dominio})";
            tabla.AddCell(new PdfPCell(new Phrase(desc, fNormal)) { Padding = 4f });
            tabla.AddCell(new PdfPCell(new Phrase("1", fNormal)) { HorizontalAlignment = Element.ALIGN_CENTER, Padding = 4f });
            tabla.AddCell(new PdfPCell(new Phrase($"{factura.Precio:C2}", fNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });
            tabla.AddCell(new PdfPCell(new Phrase($"{factura.Precio:C2}", fNormal)) { HorizontalAlignment = Element.ALIGN_RIGHT, Padding = 4f });

            doc.Add(tabla);
            doc.Add(new Paragraph("\n"));

            // 4) Total
            var tablaTot = new PdfPTable(2) { WidthPercentage = 40, HorizontalAlignment = Element.ALIGN_RIGHT };
            tablaTot.SetWidths(new[] { 50f, 50f });

            tablaTot.AddCell(new PdfPCell(new Phrase("TOTAL A PAGAR:", fSubtitulo))
            {
                Border = Rectangle.NO_BORDER,
                Padding = 4f
            });
            tablaTot.AddCell(new PdfPCell(new Phrase($"{factura.Precio:C2}", fSubtitulo))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_RIGHT,
                Padding = 4f
            });

            doc.Add(tablaTot);
            doc.Add(new Paragraph("\n\n"));

            // 5) Pie de página
            var pie = new Paragraph(
                "Gracias por preferirnos. Para consultas: horacio0291@gmail.com | +54 11 3252-6503",
                FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 9))
            {
                Alignment = Element.ALIGN_CENTER
            };
            doc.Add(pie);

            doc.Close();
        }
    }
}
