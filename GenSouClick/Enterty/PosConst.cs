using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenSouClick.Enterty
{
    internal static class PosConst
    {

        //退治按钮
        public const double Taiji_X = 0.78;
        public const double Taiji_Y = 0.47;

        //选择小地图
        public const double SubMap_X = 0.8;
        public static readonly double[] SubMap_Y = { 0.177, 0.354, 0.533, 0.713 };

        //难度选择
        public const double Hardness_Y = 0.7;
        public static readonly double[] Hardness_X = { 0.31, 0.44, 0.57, 0.68 };

        //队伍选择
        public static readonly double[] Team_X = { 0.2, 0.32, 0.434, 0.55 };
        public const double Team_Y = 0.117;

        //出击按钮
        public const double Go_X = 0.49;
        public const double Go_Y = 0.83;

        //前进按钮
        public const double Forward_X = 0.68;
        public const double Forward_Y = 0.32;

        //撤退按钮
        public const double Back_X = 0.68;
        public const double Back_Y = 0.64;

        //大地图兽道
        public const double Monst_X = 0.837;
        public const double Monst_Y = 0.5866;

        //大地图玄武之泽
        public const double XuanWu_X = 0.2776;
        public const double XuanWu_Y = 0.55;

        //大地图X坐标
        public static readonly double[] MapPos_X =
        {
            0.837,  //0兽道
            0.2776, //1玄武之泽
            0.51,   //2魔法之森
            0.577,  //3人间之里
            0.524,  //4雾之湖
            0.305,  //5妖怪之山
            0.278,  //6守矢神社
            0.394,  //7迷途之家
            0.52,   //8红魔馆
            0.84,   //9竹林
            0.822,  //10白玉楼
            0.71,   //11命莲寺
            0.193,  //12黑暗风穴
            0.5     //13万圣节

        };

        //大地图Y坐标
        public static readonly double[] MapPos_Y =
        {
            0.5866, //0兽道 
            0.55,   //1玄武之泽
            0.684,  //2魔法之森
            0.55,   //3人间之里
            0.43,   //4雾之湖
            0.348,  //5妖怪之山
            0.123,  //6守矢神社
            0.221,  //7迷途之家
            0.31,   //8红魔馆
            0.881,  //9竹林
            0.193,  //10白玉楼
            0.47,   //11命莲寺
            0.233,  //12黑暗风穴
            0.87    //13万圣节
        };
    }
}
