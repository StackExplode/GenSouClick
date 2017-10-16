using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenSouClick.Helpers
{
    class HelpItem
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public HelpItem(string n, string u)
        {
            Name = n;
            Url = u;
        }
    }
    static class HelpUrls
    {
        public static readonly HelpItem[] AllHelps =
        {
            new HelpItem("地图掉率","http://shouyou.gamersky.com/gl/201602/714220.shtml"),
            new HelpItem("稀有材料","http://www.qqxzb.com/gl/41235.html"),
            new HelpItem("装备属性和材料","http://www.yoyou.com/game/hxx/105413.html"),
            new HelpItem("二级装备","http://www.yoyou.com/game/hxx/107517.html"),
            new HelpItem("人物攻略","http://shouyou.gamersky.com/gl/201603/722310.shtml"),
            new HelpItem("游戏贴吧","http://tieba.baidu.com/f?kw=%B4%F3%BC%D2%B5%C4%BB%C3%CF%EB%CF%E7&fr=ala0&tpl=5")
        };
    }
}
