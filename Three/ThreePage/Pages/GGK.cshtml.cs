using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ThreePage.Pages
{
    public class GGKModel : PageModel
    {
        public void OnGet()
        {
        }

        public async Task<JsonResult> OnPostPlayAsync()
        {
            List<int> arr = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            int keyImg = GetRandomEle(arr);
            var retList = new List<int> { GetRandomEle(arr), GetRandomEle(arr), GetRandomEle(arr), GetRandomEle(arr), GetRandomEle(arr), GetRandomEle(arr) };
            return new JsonResult(new { code =  0 , key = keyImg, imgarr = retList,  msg = "祝贺中奖", extral = 1 });

        }
        private int GetRandomEle(List<int> arr)
        {
            Random r = new Random(GetRandomSeed());
            return arr[int.Parse(Math.Floor(r.NextDouble() * arr.Count()).ToString())];
        }
        private int GetRandomSeed()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();//生成字节数组
            return BitConverter.ToInt32(buffer, 0);
        }
    }
}
