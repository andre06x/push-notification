using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.IsisMtt.Ocsp;
using PushNotification.Models;
using System.Security.Cryptography.X509Certificates;
using UglyToad.PdfPig;


namespace PushNotification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Assinar : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetInscricao()
        {


            try
            {
                string inFile = System.IO.Path.Combine("./Controllers/input.pdf");
                string outFile = System.IO.Path.Combine("./Controllers/output.pdf");
                using (Document document = new Document(inFile))
                {
                    using (PdfFileSignature signature = new PdfFileSignature(document))
                    {
                        PKCS7 pkcs = new PKCS7("./Controllers/certificate.pfx", "P@ssw0rd!");
                        signature.Sign(1, true, new System.Drawing.Rectangle(300, 100, 400, 200), pkcs);
                        signature.Save(outFile);
                    }
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao assinar PDF: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AssinarViaPDF")]
        public async Task<IActionResult> PostPDF(IFormFile file, string certPassword)
        {
            try
            {
                using (var inFile = file.OpenReadStream())
                {
                    using (Document document = new Document(inFile))
                    {
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            using (PdfFileSignature signature = new PdfFileSignature(document))
                            {
                                PKCS7 pkcs = new PKCS7("./Controllers/certificate.pfx", "P@ssw0rd!");
                                signature.Sign(1, true, new System.Drawing.Rectangle(300, 100, 400, 200), pkcs);
                                signature.Save(outputStream);
                            }
                            byte[] signedPdfBytes = outputStream.ToArray();
                            return File(signedPdfBytes, "application/pdf", "output_signed.pdf");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao assinar PDF: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AssinarViaCertificado")]
        public async Task<IActionResult> POSTCertificado(IFormFile file, string certPassword)
        {
            string pdf = System.IO.Path.Combine("./Controllers/input.pdf");

            try
            {
                using (var inFile = file.OpenReadStream())
                {
                    using (Document document = new Document(pdf))
                    {
                        using (MemoryStream outputStream = new MemoryStream())
                        {
                            using (PdfFileSignature signature = new PdfFileSignature(document))
                            {
                                PKCS7 pkcs = new PKCS7(inFile, "P@ssw0rd!");
                                signature.Sign(1, true, new System.Drawing.Rectangle(300, 100, 400, 200), pkcs);
                                signature.Save(outputStream);
                            }
                            byte[] signedPdfBytes = outputStream.ToArray();
                            return File(signedPdfBytes, "application/pdf", "output_signed.pdf");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao assinar PDF: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
    }



