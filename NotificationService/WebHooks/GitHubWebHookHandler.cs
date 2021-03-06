﻿using Microsoft.AspNet.WebHooks;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace NotificationService.WebHooks
{
    public class GitHubWebHookHandler : WebHookHandler
    {
        public GitHubWebHookHandler()
        {
            this.Receiver = GitHubWebHookReceiver.ReceiverName;
        }
        public override Task ExecuteAsync(string generator, WebHookHandlerContext context)
        {
            // For more information about GitHub WebHook payloads, please see
            // 'https://developer.github.com/webhooks/'
            JObject entry = context.GetDataOrDefault<JObject>();
            return Task.FromResult(true);
        }
    }
}