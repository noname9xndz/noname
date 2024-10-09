using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using nona.Models;

public interface ITelegramBotService
{
    Task<bool> SendMessage(ContactModel contact);
}
public class TelegramBotService : ITelegramBotService
{
    private readonly string _botToken = "7805685909:AAHit9xHG7tYZtQKvP_t6iVyWMoD-hWo0ls";
    private readonly string _chatId = "6356283804";
    private readonly ILogger<TelegramBotService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    public TelegramBotService(ILogger<TelegramBotService> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> SendMessage(ContactModel contact)
    {
        var messageText = $"Name: {contact.Name}\nEmail: {contact.Email}\nSubject: {contact.Subject}\nMessage: {contact.Message}";
        var sendMessageUrl = $"https://api.telegram.org/bot{_botToken}/sendMessage";
        var payload = new
        {
            chat_id = _chatId,
            text = messageText
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), System.Text.Encoding.UTF8, "application/json");
        var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.PostAsync(sendMessageUrl, content);
        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError($"Error: HTTP {response.StatusCode}");
            _logger.LogError($"Response Content: {errorContent}");
            return false;
        }
        return true;
    }
}