using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace GenSouClick.Enterty
{
    class MapEnterty
    {
        public MapEnterty()
        {
            SubMaps = new List<SubMapEnterty>();
        }
        public PointF Position { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public List<SubMapEnterty> SubMaps { get; set; }

        public string NameDetail
        {
            get 
            {
                string rt = Name;
                if(SubMaps.Count < 1)
                    return rt;
                rt += "(";
                foreach (SubMapEnterty sub in SubMaps)
                {
                    rt += sub.Name + ",";
                }
                rt = rt.Remove(rt.Length - 1);
                rt += ")";
                return rt;
            }
        }
    }

    class SubMapEnterty
    {
        public SubMapEnterty(int id,string name)
        {
            ID = id;
            Name = name;
        }
        public int ID { get; set; }
        public string Name { get; set; }
    }

    static class AllMaps
    {
        public static Dictionary<string, MapEnterty> Data { get; private set; }

        public static void Init_AllMaps()
        {
            Data = new Dictionary<string, MapEnterty>();
            XmlDocument xd = new XmlDocument();
            xd.Load("./Data/map.xml");
            XmlNode xn = xd.SelectSingleNode("mapinfo");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xx in xnl)
            {
                MapEnterty me = new MapEnterty();
                me.Key = xx.Attributes["key"].Value.ToString();
                me.Name = xx.Attributes["name"].Value.ToString();
                float x = Convert.ToSingle(xx.Attributes["x"].Value);
                float y = Convert.ToSingle(xx.Attributes["y"].Value);
                me.Position = new PointF(x, y);
                foreach (XmlNode xx2 in xx.ChildNodes)
                {
                    int id = Convert.ToInt16(xx2.Attributes["id"].Value);
                    string name = xx2.Attributes["name"].Value.ToString();
                    me.SubMaps.Add(new SubMapEnterty(id,name));
                }
                Data.Add(me.Key, me);
            }
        }

    }
}
