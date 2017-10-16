using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GenSouClick.Helpers;
using System.Drawing;
using System.Threading;

namespace GenSouClick.Enterty
{
    

    enum Place
    {
        UnKown,
        Home,
        Map,
        Hardness,
        Standby,
        ScoreBoard,
        MaoYuQiXiDi
    }

    

    class Game
    {
        protected YeShen yeshen = new YeShen();

        public PlaceEnterty GetCurrPlace()
        {
            
            PlaceAnalysiser pa = new PlaceAnalysiser(yeshen);

            foreach (var x in AllPlaces.Data)
            {
                if (pa.IsHere(x.Value.Attrs))
                    return x.Value;
            }

            return null;
        }

        public bool CheckPlace(string id)
        {
            PlaceAnalysiser pa = new PlaceAnalysiser(yeshen);
            if (pa.IsHere(AllPlaces.Data[id].Attrs))
                return true;
            return false;
        }

        public PlaceEnterty CheckPlaces(params string[] ids)
        {
            PlaceAnalysiser pa = new PlaceAnalysiser(yeshen);
            foreach (var x in ids)
            {
                if (pa.IsHere(AllPlaces.Data[x].Attrs))
                    return AllPlaces.Data[x];
            }
            return null;
        }

        [Obsolete("过时了",true)]
        public bool CheckForward()
        {
            PlaceAnalysiser pa = new PlaceAnalysiser(yeshen);
            Color ral = yeshen.GetPixColor(0.5515, 0.3088);
            if (pa.JudgeColor(ral, Color.FromArgb(55, 8, 57), 12))
                return true;
            return false;
        }

        public void Click_Single(string id)
        {
            PointF pf = AllPoints.Data[id].Point;
            yeshen.ClickPos(pf.X, pf.Y);
        }

        public void Click_Group(string id1, string id2)
        {
            PointF pf = AllPoints.Data[id1].Group[id2].Point;
            yeshen.ClickPos(pf.X, pf.Y);
        }
        public void Click_Group(string id1, int id2)
        {
            if (id1 == "team")
            {
                int a = 1;
            }
            PointF pf = AllPoints.Data[id1].Group[id2.ToString()].Point;
            yeshen.ClickPos(pf.X, pf.Y);
        }

        [Obsolete("过时了", true)]
        public void Click_Center()
        {
            yeshen.ClickPos(0.5, 0.5);
        }
        [Obsolete("过时了", true)]
        public void Click_Taiji()
        {
            yeshen.ClickPos(PosConst.Taiji_X, PosConst.Taiji_Y);
        }

       [Obsolete("过时了", true)]
        public void Click_SubMapMenu(int index)
        {
            yeshen.ClickPos(PosConst.SubMap_X, PosConst.SubMap_Y[index]);
        }
        [Obsolete("过时了", true)]
        public void Click_Hardness(int hard)
        {
            yeshen.ClickPos(PosConst.Hardness_X[hard], PosConst.Hardness_Y);
        }
        [Obsolete("过时了", true)]
        public void Click_Go()
        {
            yeshen.ClickPos(PosConst.Go_X, PosConst.Go_Y);
        }
        [Obsolete("过时了", true)]
        public void Click_Forward()
        {
            yeshen.ClickPos(PosConst.Forward_X, PosConst.Forward_Y);
        }
        [Obsolete("过时了", true)]
        public void Click_Back()
        {
            yeshen.ClickPos(PosConst.Back_X, PosConst.Back_Y);
        }

        public void Click_MapId(string map_key)
        {
            yeshen.ClickPos(AllMaps.Data[map_key].Position.X, AllMaps.Data[map_key].Position.Y);
        }
        [Obsolete("过时了", true)]
        public void Click_Team(int tm)
        {
            yeshen.ClickPos(PosConst.Team_X[tm], PosConst.Team_Y);
        }
        [Obsolete("过时了", true)]
        public void Click_HakuReiJinJya()
        {
            yeshen.ClickPos(0.9, 0.343);
        }

        public string GetCurrPlaceName()
        {
            var x = GetCurrPlace();
            if (x == null)
                return "未知区域";
            return x.Name;
        }

        public bool WaitToPlace( string plc, int ms = 500, int max = 5)
        {
            while (max-- > 0)
            {
                bool pe = CheckPlace(plc);
                Thread.Sleep(ms);
                if (pe == false)
                    continue;
                if (pe == true)
                    return true;
            }
            return false;
        }

        public PlaceEnterty WaitToPlaces(string[] plcs, int ms = 500, int max = 5)
        {
            PlaceEnterty pe = null;
            while (max-- > 0)
            {
                pe = CheckPlaces(plcs);
                Thread.Sleep(ms);
                if (pe == null)
                    continue;
                else
                    break;
            }
            return pe;
        }


    }
}
