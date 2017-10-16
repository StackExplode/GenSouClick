using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace GenSouClick.Enterty
{
    class PointEnterty
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public PointF Point { get; set; }
        public Dictionary<string, PointEnterty> Group { get; set; }
        public PointEnterty(string key, string name, bool isgrp)
        {
            Key = key;
            Name = name;
            IsGroup = isgrp;
            if (isgrp)
                Group = new Dictionary<string, PointEnterty>();
        }
        public void AppendChildren(string key, string name,PointF pt)
        {
            PointEnterty pe = new PointEnterty(key, name, false);
            pe.Point = pt;
            Group.Add(key, pe);
        }
    }

    static class AllPoints
    {
        public static Dictionary<string, PointEnterty> Data;

        public static void Init_Points()
        {
            Data = new Dictionary<string, PointEnterty>();
            XmlDocument xd = new XmlDocument();
            xd.Load("./Data/point.xml");
            XmlNode xn = xd.SelectSingleNode("pointinfo");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xx in xnl)
            {
                PointEnterty pe;
                bool isgrp = false;
                if (xx.Name == "group")
                    isgrp = true;
                string kkk = xx.Attributes["key"].Value.ToString();
                string nnn = xx.Attributes["name"].Value.ToString();            
                pe = new PointEnterty(kkk, nnn, isgrp);
                if (!isgrp)
                {
                    float x = Convert.ToSingle(xx.Attributes["x"].Value);
                    float y = Convert.ToSingle(xx.Attributes["y"].Value);
                    pe.Point = new PointF(x, y);
                }
                else
                {
                    pe.Point = new PointF(0.5F, 0.5F);
                    foreach (XmlNode xx2 in xx)
                    {
                        string kkk2 = xx2.Attributes["key"].Value.ToString();
                        string nnn2 = xx2.Attributes["name"].Value.ToString();
                        float x = Convert.ToSingle(xx2.Attributes["x"].Value);
                        float y = Convert.ToSingle(xx2.Attributes["y"].Value);
                        PointF pf = new PointF(x, y);
                        pe.AppendChildren(kkk2, nnn2, pf);
                    }
                }
                Data.Add(pe.Key, pe);
            }
        }

    }
}
