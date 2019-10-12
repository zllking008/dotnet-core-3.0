using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SignalRDemo.Services;

namespace SignalRDemo
{
    //身份认证
    //[Authorize]
    public class CountHub:Hub
    {
        private readonly CountService _countService;

        public CountHub(CountService countService)
        {
            _countService = countService;
        }

        public async Task GetLatestCount(string random)
        {
            //var user = Context.User.Identity.Name;
            int count;
            do
            {
                count = _countService.GetLatestCount();
                Thread.Sleep(1000);
                await Clients.All.SendAsync("ReceiveUpdate", count);
            } while (count<10);

            await Clients.All.SendAsync("Finished");
        }

        public override async Task OnConnectedAsync()
        {

            //var connectionId = Context.ConnectionId;//获得当前连接hub的唯一标识
            //var client = Clients.Client(connectionId);//获得当前的客户端

            //await client.SendAsync("someFunc",new { });//对connectionId上的客户端调用方法
            //await Clients.AllExcept(connectionId).SendAsync("someFunc", new { });//除了connectionId的客户端，其他客户端调用someFunc方法

            //await Groups.AddToGroupAsync(connectionId, "group1");//分组，向group1这个组里面添加客户端
            //await Groups.RemoveFromGroupAsync(connectionId, "group1");//移除分组

            //await Clients.Groups("group1").SendAsync("someFunc");//调用一个组里面的客户端的方法
        }
    }
}
