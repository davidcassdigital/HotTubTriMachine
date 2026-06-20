using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace HotTubTriMachine.Api
{
    public static class ContactFunction
    {
        [Function("Contact")]
        // include "options" so preflight requests are accepted
        public static async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", "options")] HttpRequestData req,
            FunctionContext context)
        {
            var logger = context.GetLogger("Contact");

            // Handle CORS preflight
            if (string.Equals(req.Method, "OPTIONS", StringComparison.OrdinalIgnoreCase))
            {
                var preflight = req.CreateResponse(HttpStatusCode.NoContent);
                AddCorsHeaders(preflight);
                return preflight;
            }

            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var model = JsonSerializer.Deserialize<ContactMessage>(requestBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (model is null)
                {
                    var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                    AddCorsHeaders(bad);
                    await bad.WriteStringAsync("Invalid request payload.");
                    return bad;
                }

                // Validate
                var validationContext = new ValidationContext(model);
                var validationResults = new List<ValidationResult>();
                if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
                {
                    var bad = req.CreateResponse(HttpStatusCode.BadRequest);
                    AddCorsHeaders(bad);
                    await bad.WriteStringAsync(JsonSerializer.Serialize(validationResults.Select(r => r.ErrorMessage)));
                    return bad;
                }

                model.ReceivedAt = DateTime.UtcNow;

                // Send email via SendGrid if configured
                var sendGridKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var sendGridTo = Environment.GetEnvironmentVariable("CONTACT_TO") ?? "contact@hottubtrimachine.au";
                var sendGridFrom = Environment.GetEnvironmentVariable("CONTACT_FROM") ?? "emails@davidcassdigital.au";

                if (string.IsNullOrEmpty(sendGridKey))
                {
                    logger.LogWarning("SENDGRID_API_KEY not configured. Skipping send.");
                }
                else
                {
                    try
                    {
                        var client = new SendGridClient(sendGridKey);
                        var msg = new SendGridMessage()
                        {
                            From = new EmailAddress(sendGridFrom, "Hot Tub Tri Machine"),
                            ReplyTo = new EmailAddress(model.Email, model.Name),                            
                            Subject = $"{model.Category} from {model.Name}",
                            PlainTextContent = $"Name: {model.Name}\nEmail: {model.Email}\n\n{model.Message}"
                        };
                        msg.AddTo(new EmailAddress(sendGridTo));
                        var resp = await client.SendEmailAsync(msg);
                        logger.LogInformation("SendGrid response status: {statusCode}", resp.StatusCode);
                    
                        if (!resp.IsSuccessStatusCode)
                        {
                            var body = await resp.Body.ReadAsStringAsync();
                            logger.LogError("SendGrid send failed with status {statusCode}: {responseBody}", resp.StatusCode, body);
                            var err = req.CreateResponse(HttpStatusCode.InternalServerError);
                            AddCorsHeaders(err);
                            await err.WriteStringAsync("Failed to send message.");
                            return err;
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "SendGrid send failed.");
                        var err = req.CreateResponse(HttpStatusCode.InternalServerError);
                        AddCorsHeaders(err);
                        await err.WriteStringAsync("Failed to send message.");
                        return err;
                    }
                }

                var ok = req.CreateResponse(HttpStatusCode.OK);
                AddCorsHeaders(ok);
                await ok.WriteAsJsonAsync(new { success = true });
                return ok;
            }
            catch (Exception ex)
            {
                var resp = req.CreateResponse(HttpStatusCode.InternalServerError);
                AddCorsHeaders(resp);
                await resp.WriteStringAsync($"Unexpected error: {ex.Message}");
                context.GetLogger("Contact").LogError(ex, "Unexpected error in Contact function.");
                return resp;
            }
        }

        private static void AddCorsHeaders(HttpResponseData response)
        {
            // For development you can allow any origin; tighten for production.
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "POST,OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
        }
    }

    public class ContactMessage
    {
        [Required] public string? Name { get; set; }
        [Required, EmailAddress] public string? Email { get; set; }
        [Required] public string? Category { get; set; }
        [Required] public string? Message { get; set; }
        public DateTime ReceivedAt { get; set; }
    }
}