using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Amazon.Lex.Model;

namespace dotnetLexChatBot.Data
{
    public interface IAWSLexService : IDisposable
    {
        string PostContentToLex(string messageToSend);

        Task<PostTextResponse> SendTextMsgToLex(string messageToSend,Dictionary<string,string> lexSessionAttributes);

        Task<PostTextResponse> SendTextMsgToLex(string messageToSend);

        Task<Stream> SendAudioMsgToLex(Stream audioToSend);

    }
}
