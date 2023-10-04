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
        public List<List<InlineKeyboardButton>> CreateButton(List<Search> films )
        {
            
            int count = 0;
            var rows = new List<InlineKeyboardButton>();
            var ListOfKeyboardButton = new List<List<InlineKeyboardButton>>();
            
            
            
            if(films.Count <= 5)
            {
                rows = new List<InlineKeyboardButton>();
                for (int i = 0; i < films.Count; i++) 
                {
                    rows.Add(InlineKeyboardButton.WithCallbackData($"{count + 1}", $"{films[count].imdbID}"));
                    count++;
                }
                ListOfKeyboardButton.Add(rows);
            }
            else
            {
                rows = new List<InlineKeyboardButton>();
                for(int i = 0;i <= films.Count; i++)
                {
                    rows.Add(InlineKeyboardButton.WithCallbackData($"{count + 1}", $"{films[count].imdbID}"));
                    count++;

                }
                ListOfKeyboardButton.Add(rows);
                rows = new List<InlineKeyboardButton>();
                for (int i = 0; i <= films.Count; i++)
                {
                    rows.Add(InlineKeyboardButton.WithCallbackData($"{count + 1}", $"{films[count].imdbID}"));
                    count++;

                }
                ListOfKeyboardButton.Add(rows);


            }
            ListOfKeyboardButton.Add(new List<InlineKeyboardButton>() 
            {
                InlineKeyboardButton.WithCallbackData($"-", $"{}")
            });
        }
    }
}
