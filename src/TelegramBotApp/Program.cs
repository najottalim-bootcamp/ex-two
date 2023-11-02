﻿

internal class Program
{
    private static async Task Main(string[] args)
    {
        FilmService film = new FilmService();
        await film.GetFilmAsync("tt0386140");
        await film.GetFilmListAsync("Zorro");
        return;
        var botClient = new TelegramBotClient("{YOUR_ACCESS_TOKEN_HERE}");

        using CancellationTokenSource cts = new();

        // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
        ReceiverOptions receiverOptions = new()
        {
         
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };

        botClient.StartReceiving(
            updateHandler: HandleUpdateAsync,
            pollingErrorHandler: HandlePollingErrorAsync,
            receiverOptions: receiverOptions,
            cancellationToken: cts.Token
        );

        var me = await botClient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();

        // Send cancellation request to stop bot
        cts.Cancel();

        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            var handler = update.Type switch
            {
                UpdateType.Message => HandleMessageAsync(botClient, update.Message, cancellationToken),
                UpdateType.Unknown => throw new NotImplementedException(),
                UpdateType.InlineQuery => throw new NotImplementedException(),
                UpdateType.ChosenInlineResult => throw new NotImplementedException(),
                UpdateType.CallbackQuery => HandleCallbackQueryAsync(botClient, update.CallbackQuery, cancellationToken),
                UpdateType.EditedMessage => throw new NotImplementedException(),
                UpdateType.ChannelPost => throw new NotImplementedException(),
                UpdateType.EditedChannelPost => throw new NotImplementedException(),
                UpdateType.ShippingQuery => throw new NotImplementedException(),
                UpdateType.PreCheckoutQuery => throw new NotImplementedException(),
                UpdateType.Poll => HandlePollAsync(botClient, update.Poll, cancellationToken),
                UpdateType.PollAnswer => HandlePollAnswerAsync(botClient, update.PollAnswer, cancellationToken),
                UpdateType.MyChatMember => throw new NotImplementedException(),
                UpdateType.ChatMember => throw new NotImplementedException(),
                UpdateType.ChatJoinRequest => throw new NotImplementedException()

            };

            try
            {
                await handler;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;

        }
    }

    private static Task HandlePollAnswerAsync(ITelegramBotClient botClient, PollAnswer? pollAnswer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static Task HandlePollAsync(ITelegramBotClient botClient, Poll? poll, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static Task HandleCallbackQueryAsync(ITelegramBotClient botClient, CallbackQuery? callbackQuery, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    private static async Task HandleMessageAsync(ITelegramBotClient botClient, Message? updmessage, CancellationToken cancellationToken)
    {
        // Only process Message updates: https://core.telegram.org/bots/api#message
        if (updmessage is not { } message)
            return;
        // Only process text messages
        if (message.Text is not { } messageText)
            return;

        var chatId = message.Chat.Id;
        if(messageText =="/start")
        {
            Message sentMessge = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Kino nomini kiriting:",
                cancellationToken: cancellationToken
                );
        }
        else
        {
            BotService.SendFilmsAsync(botClient, message, cancellationToken);
        }
        Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

        // Echo received message text
        Message sentMessage = await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: "You said:\n" + messageText,
            cancellationToken: cancellationToken);
    }
}