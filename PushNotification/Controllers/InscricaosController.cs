using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PushNotification.Configuracao;
using PushNotification.DTO;
using PushNotification.Models;
using WebPush;

namespace PushNotification.Controllers
{
    [Route("api/PushNotification/[controller]")]
    [ApiController]
    public class InscricaosController : ControllerBase
    {
        private readonly Contexto _context;
        private readonly WebPushClient _webPushClient;

        public InscricaosController(WebPushClient webPushClient, Contexto context)
        {
            _context = context;
            _webPushClient = webPushClient;
        }

        // GET: api/Inscricaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inscricao>>> GetInscricao()
        {
            if (_context.Inscricao == null)
            {
                return NotFound();
            }
            return await _context.Inscricao.ToListAsync();
        }

        // GET: api/Inscricaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inscricao>> GetInscricao(int id)
        {
            if (_context.Inscricao == null)
            {
                return NotFound();
            }
            var inscricao = await _context.Inscricao.FindAsync(id);

            if (inscricao == null)
            {
                return NotFound();
            }

            return inscricao;
        }

        // PUT: api/Inscricaos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscricao(int id, Inscricao inscricao)
        {
            if (id != inscricao.id)
            {
                return BadRequest();
            }

            _context.Entry(inscricao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InscricaoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Inscricaos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inscricao>> PostInscricao(Inscricao inscricao)
        {
            if (_context.Inscricao == null)
            {
                return Problem("Entity set 'Contexto.Inscricao'  is null.");
            }

            var inscricaoExiste = await _context.Inscricao.FirstOrDefaultAsync(x => x.aparelho == inscricao.aparelho);

            if (inscricaoExiste != null && inscricaoExiste.id != null && inscricaoExiste.endpoint == inscricao.endpoint && inscricaoExiste.auth == inscricao.auth && inscricaoExiste.p26dh == inscricao.p26dh)
            {
                return NotFound("Tudo igual");
            }

            if (inscricaoExiste != null)
            {
                if (inscricaoExiste?.id != null && inscricaoExiste?.endpoint != inscricao.endpoint || inscricaoExiste?.auth != inscricao.auth || inscricaoExiste?.p26dh != inscricao.p26dh)
                {
                    inscricaoExiste.updatedAt = DateTime.UtcNow;
                    inscricaoExiste.p26dh = inscricao.p26dh;
                    inscricaoExiste.endpoint = inscricao.endpoint;
                    inscricaoExiste.auth = inscricao.auth;

                    await _context.SaveChangesAsync();
                    return Ok("Objeto atualziado com sucesso.");
                }
            }


            inscricao.CriarData();
            _context.Inscricao.Add(inscricao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInscricao", new { id = inscricao.id }, inscricao);
        }

        // DELETE: api/Inscricaos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInscricao(int id)
        {
            if (_context.Inscricao == null)
            {
                return NotFound();
            }
            var inscricao = await _context.Inscricao.FindAsync(id);
            if (inscricao == null)
            {
                return NotFound();
            }

            _context.Inscricao.Remove(inscricao);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Enviar Push escolhidos")]
        public async Task<IActionResult> SendNotificationPush([FromBody] BuscaNotificacao busca)
        {
            try
            {
                var query = _context.Inscricao.AsQueryable();

                if (busca.aparelho_id.HasValue)
                {
                    query = query.Where(i => i.aparelho == busca.aparelho_id);
                }

                if (busca.usuario_id.HasValue)
                {
                    query = query.Where(i => i.usuario_id == busca.usuario_id);
                }

                if (busca.inscricao_id.HasValue)
                {
                    query = query.Where(i => i.id == busca.inscricao_id);
                }

                // Adicione mais condições conforme necessário

                var inscricao = await query.FirstOrDefaultAsync();

                string publicKey = "BGhDoF1dnSuETZAp-Duns1g0I_kuhHHD2ysOBi_0HldmBtRjgANPxjxhDWVqMnghIDK--Zuf2rO2NPxX8ggiESs";
                string privateKey = "O8XdWwT_aECgFk2LABrJE0y3z6BiSLkkdYuZx9UXGRw";

                var vapidDetails = new VapidDetails("mailto:seu_email@example.com", publicKey, privateKey);

                var notificationData = new
                {
                    title = busca.title,
                    body = busca.body,
                    icon = busca.icon
                };

                string payload = JsonSerializer.Serialize(notificationData);


                if (busca.inscricao_id != null || busca.aparelho_id != null)
                {
                    var pushSubscription = new PushSubscription(inscricao.endpoint, inscricao.p26dh, inscricao.auth);
                    await _webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                }

                else
                {
                    var inscricoesDoUsuario = await _context.Inscricao
                        .Where(i => i.usuario_id == busca.usuario_id)
                        .ToListAsync();

                    foreach (var inscricaos in inscricoesDoUsuario)
                    {
                        var pushSubscription = new PushSubscription(inscricaos.endpoint, inscricaos.p26dh, inscricaos.auth);
                        await _webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                    }
                }

                return Ok("Notificação push enviada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao enviar a notificação push: {ex.Message}");
            }
        }

        [HttpPost("Enviar Push para todos")]
        public async Task<IActionResult> SendNotificationPushTodos([FromBody] PushTodos busca)
        {
            try
            {

                string publicKey = "BGhDoF1dnSuETZAp-Duns1g0I_kuhHHD2ysOBi_0HldmBtRjgANPxjxhDWVqMnghIDK--Zuf2rO2NPxX8ggiESs";
                string privateKey = "O8XdWwT_aECgFk2LABrJE0y3z6BiSLkkdYuZx9UXGRw";

                var vapidDetails = new VapidDetails("mailto:seu_email@example.com", publicKey, privateKey);

                var notificationData = new
                {
                    title = busca.title,
                    body = busca.body,
                    icon = busca.icon
                };

                string payload = JsonSerializer.Serialize(notificationData);

                var inscricoesDoUsuario = await _context.Inscricao.ToListAsync();
                foreach (var inscricao in inscricoesDoUsuario)
                {
                    var pushSubscription = new PushSubscription(inscricao.endpoint, inscricao.p26dh, inscricao.auth);

                    try
                    {
                        await _webPushClient.SendNotificationAsync(pushSubscription, payload, vapidDetails);
                    }
                    catch (WebPushException ex)
                    {

                        Console.WriteLine($"Erro ao enviar notificação: {inscricao.id} {ex.Message}, Inscrição: {inscricao.id}, Endpoint: {inscricao.endpoint}");

                        if (ex.Message == "Subscription no longer valid") {
                            Console.WriteLine("Token expirado, removendo endpoint");
                            await DeleteByEndpointAsync(inscricao.endpoint);
                        }


                    }
                }



                return Ok("Notificação push enviada com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao enviar a notificação push: {ex.Message}");
            }
        }

        private bool InscricaoExists(int id)
        {
            return (_context.Inscricao?.Any(e => e.id == id)).GetValueOrDefault();
        }

        [HttpDelete("DeleteByEndpoint/{endpoint}")]
        public async Task DeleteByEndpointAsync(string endpoint)
        {
            try
            {
                var inscricao = await _context.Inscricao
    .FirstOrDefaultAsync(i => i.endpoint == endpoint);

                if (inscricao != null)
                {
                    _context.Inscricao.Remove(inscricao);
                    await _context.SaveChangesAsync();
                }
            }catch(Exception ex)
            {
                Console.WriteLine($"Erro ao deletar: {ex.Message} Endpoint:{endpoint}");
            }

        }
    }
}
