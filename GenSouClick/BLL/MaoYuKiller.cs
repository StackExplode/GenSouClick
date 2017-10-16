using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using GenSouClick.Helpers;
using GenSouClick.Enterty;

namespace GenSouClick.BLL
{
    
    class MaoYuKiller : ShuaBase
    {
        private readonly string[] _HardName = { "easy", "normal", "hard", "lunatic" };
        private int hard = 1;
        private string map = "";
        private int team = 0;
        private int goout = 0;
        private int submap = 0;

        public MaoYuKiller(int i,string m,int sub,int h,int t,int o) : base(i) 
        {
            hard = h;
            map = m;
            team = t;
            goout = o;
            submap = sub;
        }

        protected override void Mission()
        {

            Game gm = new Game();
            bool fail = false;
            string err = "";
            base.DoOnStart();
            while (Running && Count++ < Total)
            {
                bool succ = false;
                int round = 0;
                if (!gm.CheckPlace("main"))
                {
                    fail = true;
                    err = "没有从主界面开始！";
                    break;
                }
                Sleep(500);
                gm.Click_Single("taiji");
                succ = gm.WaitToPlace("map");
                if (!succ)
                {
                    fail = true;
                    err = "进入大地图失败！";
                    break;
                }
                /***************/
                gm.Click_MapId(map);

                Sleep(500);
                gm.Click_Group("team", team + 1);
                Sleep(50);
                gm.Click_Group("submap", submap + 1);

                /***************/
                succ = gm.WaitToPlace( "hardness", 200);
                if (!succ)
                {
                    fail = true;
                    err = "进入难度选择界面失败！";
                    break;
                }
                gm.Click_Group("hardness", _HardName[hard]);
                Sleep(100);
                gm.Click_Single("go");
                succ = gm.WaitToPlace( "standby");
                if (!succ)
                {
                    fail = true;
                    err = "进入准备出战界面失败！";
                    break;
                }
                do
                {
                    succ = gm.WaitToPlace("forward",200,20);
                    if (!succ)
                        break;
                    gm.Click_Single("forward");
                    round++;
                    succ = gm.WaitToPlace("scorebord", 1000, 999);
                    if (!succ)
                        throw new Exception("你TM打不完了？");
                    gm.Click_Single("center");

                    if (goout != 0 && round >= goout)
                    {
                        succ = gm.WaitToPlace("forward", 200, 5);
                        if (!succ)
                            break;
                        gm.Click_Single("back");
                        break;
                    }
                    PlaceEnterty pe = gm.WaitToPlaces( new string[] { "map", "forward" }, 200, 20);
                    if (pe.Key == "map")
                        break;
                } while (true);

                gm.Click_MapId("hakurei");
                succ = gm.WaitToPlace( "main", 200, 14);

                base.DoOnOneTurn();

                Sleep(200);
            }
            Running = false;
            base.DoOnFinished(fail, err);

        }

      
    }
}
