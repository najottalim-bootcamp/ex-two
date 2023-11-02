using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBotApp.Farxodbek;
using TelegramBotApp.models;


namespace TelegramBotApp.Service
{
    public class ButtonService
    {
        public List<List<InlineKeyboardButton>> CreateButton(Root root)
        {
            var films = root.Search;
            var searchkey = root.SearchKey;
            var page = root.pageIndex;
            var ListOfKeyboardButton = new List<List<InlineKeyboardButton>>();
            
            
            
            if(films.Count <= 5)
            {
                var rows = new List<InlineKeyboardButton>();
                for (int i = 0; i < films.Count; i++) 
                {
                    rows.Add(InlineKeyboardButton.WithCallbackData($"{i + 1}", $"{films[i].imdbID}"));
                    
                }
                ListOfKeyboardButton.Add(rows);
            }
            else
            {
                var rows = new List<InlineKeyboardButton>();
                for(int i = 0;i <= films.Count-5; i++)
                {
                    rows.Add(InlineKeyboardButton.WithCallbackData($"{i + 1}", $"{films[i].imdbID}"));
                    

                }
                ListOfKeyboardButton.Add(rows);
                rows = new List<InlineKeyboardButton>();
                for (int i = 5; i < films.Count; i++)
                {
                    rows.Add(InlineKeyboardButton.WithCallbackData($"{i + 1}", $"{films[i].imdbID}"));
                }
                ListOfKeyboardButton.Add(rows);


            }
            ListOfKeyboardButton.Add(new List<InlineKeyboardButton>() 
            {
                InlineKeyboardButton.WithCallbackData($"←", $"page={page-1} {searchkey}"),
                InlineKeyboardButton.WithCallbackData($"→", $"page={page+1} {searchkey}")

            });
            return ListOfKeyboardButton;
        }
    }
}
