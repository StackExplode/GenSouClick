using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace GenSouClick.Enterty
{
    class PlaceEnterty
    {
        public PlaceEnterty()
        {
            Attrs = new List<PlaceAttr>();
        }
        public string Key { get; set; }
        public string Name { get; set; }
        public List<PlaceAttr> Attrs { get; set; }
    }

    class PlaceAttr
    {
        public PlaceAttr(float x, float y, int r, int g, int b,int tor = 10)
        {
            Position = new PointF(x, y);
            AttrColor = Color.FromArgb(r, g, b);
            Tolerate = tor;
        }
        public PointF Position { get; set; }
        public Color AttrColor { get; set; }
        public int Tolerate { get; set; }
    }

    static class AllPlaces
    {
        public static Dictionary<string, PlaceEnterty> Data;

        public static void Init_Place()
        {
            Data = new Dictionary<string, PlaceEnterty>();
            XmlDocument xd = new XmlDocument();
            xd.Load("./Data/place.xml");
            XmlNode xn = xd.SelectSingleNode("placeinfo");
            XmlNodeList xnl = xn.ChildNodes;
            foreach (XmlNode xx in xnl)
            {
                PlaceEnterty me = new PlaceEnterty();
                me.Key = xx.Attributes["key"].Value.ToString();
                me.Name = xx.Attributes["name"].Value.ToString();
                              
                foreach (XmlNode xx2 in xx.ChildNodes)
                {
                    float x = Convert.ToSingle(xx2.Attributes["x"].Value);
                    float y = Convert.ToSingle(xx2.Attributes["y"].Value);
                    int rrr = Convert.ToInt32(xx2.Attributes["r"].Value);
                    int ggg = Convert.ToInt32(xx2.Attributes["g"].Value);
                    int bbb = Convert.ToInt32(xx2.Attributes["b"].Value);
                    int tor = Convert.ToInt32(xx2.Attributes["tor"].Value);
                    me.Attrs.Add(new PlaceAttr(x, y, rrr, ggg, bbb, tor));
                }
                Data.Add(me.Key, me);
            }
        }
    }
}
