using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using dotnetLexChatBot.Models;
using dotnetLexChatBot.Data;
using System.Net.Http;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace dotnetLexChatBot.Controllers
{
    public class HelloChatBotController : Controller
    {  
        //Collection of ChatBot Messages
        static List<ChatBotMessage> botMessages = new List<ChatBotMessage>();
        static Dictionary<string, string> lexSessionData = new Dictionary<string, string>();
        //private readonly AWSOptions _awsOptions;
        private readonly IAWSLexService awsLexSvc;

        public HelloChatBotController(IAWSLexService awsLexService)
        {
            awsLexSvc = awsLexService;
            
        }
      

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(botMessages);
        }

       
        public IActionResult TestChat()
        {
            return View(botMessages);
        }

        public IActionResult ClearBot()
        {
            botMessages.RemoveRange(0, botMessages.Count);
            awsLexSvc.Dispose();
            return View("TestChat", botMessages);
        }

        [HttpGet]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GetChatMessage(string userMessage)
        {
            botMessages.Add(new ChatBotMessage()
            { MsgType = MessageType.UserMessage, ChatMessage = userMessage });

            await postUserData();
            var lexResponse = await awsLexSvc.SendTextMsgToLex(userMessage, lexSessionData);
            
            lexSessionData = lexResponse.SessionAttributes;
            botMessages.Add(new ChatBotMessage()
            { MsgType = MessageType.LexMessage, ChatMessage = lexResponse.Message });


            // botMessages.Add(new ChatBotMessage()
            // { MsgType = MessageType.LexMessage, ChatMessage = "Testing Return" });

            return View("TestChat",botMessages);
        }

        public async Task<IActionResult> postUserData()
        {
            //testing
            return await Task.Run(()=> TestChat());
        }
        
    }

}