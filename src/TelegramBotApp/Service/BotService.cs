
namespace TelegramBotApp.Service
{
    public class BotService
    {
        public static async Task SendFilmsAsync(ITelegramBotClient botClient, Message? message, CancellationToken cancellationToken)
        {
            FilmService filmService = new FilmService();
            var filmList = await filmService.GetFilmListAsync(message.Text);
            var replyMarkup = new InlineKeyboardMarkup(InlineKeyboardButton.WithCallbackData("text"));
            await botClient.SendTextMessageAsync(
                chatId: message.From.Id,
                text: $"Search:{filmList.SearchKey} page-{filmList.pageIndex}",
                cancellationToken: cancellationToken);
        }
    }
}
