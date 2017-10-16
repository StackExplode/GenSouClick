using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GenSouClick.BLL;
using GenSouClick.Helpers;
using System.Windows.Forms;
using System.Windows.Forms;
using System.Threading;

namespace GenSouClick.Enterty
{
    class LuaExporter
    {
        private Game _Gm;
        private ShuaLua _Shua;
        private LuaFramework _lua;
        public LuaExporter(string path)
        {
            _lua = new LuaFramework();
            _lua.ExecuteFile(path);
        }

        public void RegistFunc(Game gm, ShuaLua shua)
        {
            _Gm = gm;
            _Shua = shua;
            _lua.BindLuaApiClass(this);
        }

        public void DoSetup()
        {
            _lua.ExecuteFunc("Setup");
        }

        public void DoLoop()
        {
            _lua.ExecuteFunc("Loop");
        }

        public void DoEndup(bool succ,string msg)
        {
            _lua.ExecuteFunc("Finalize",new object[]{succ,msg});
        }

        [LuaFunction("BreakLoop")]
        public void BreakLoop()
        {
            _Shua.BreakLoop();
        }

        [LuaFunction("ShowMessageBox")]
        public void ShowMessageBox(string s)
        {
            _Shua.ShowMsg(s);
        }

        [LuaFunction("LogInfo")]
        public void LogInfo(string s)
        {
            _Shua.WriteLog(s, 1);
        }

        [LuaFunction("LogWarn")]
        public void LogWarn(string s)
        {
            _Shua.WriteLog(s, 2);
        }

        [LuaFunction("LogError")]
        public void LogError(string s)
        {
            _Shua.WriteLog(s, 3);
        }

        [LuaFunction("LogDebug")]
        public void LogDebug(string s)
        {
            _Shua.WriteLog(s, 4);
        }

        [LuaFunction("SetLabel")]
        public void SetLabel(int i)
        {
            _Shua.SetLable(i);
        }

        [LuaFunction("Delay")]
        public void Delay(int ms)
        {
            Thread.Sleep(ms);
        }

        [LuaFunction("SetError")]
        public void SetError(string s)
        {
            _Shua.SetError(s);
        }

        [LuaFunction("GetCurrentPlace")]
        public string GetCurrentPlace()
        {
            PlaceEnterty pe = _Gm.GetCurrPlace();
            if (pe != null)
                return pe.Key;
            else
                return null;
        }

        [LuaFunction("GetCurrentName")]
        public string GetCurrentName()
        {
            return _Gm.GetCurrPlaceName();
        }

        [LuaFunction("ClickSingle")]
        public void ClickSingle(string key)
        {
            try
            {
                _Gm.Click_Single(key);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("ClickSingle错误，无法找到指定点击位置！");
            }
            
        }

        [LuaFunction("ClickGroup")]
        public void ClickGroup(string key1, string key2)
        {
            try
            {_Gm.Click_Group(key1, key2);}
            catch (KeyNotFoundException ex)
            {
                throw new Exception("ClickGroup错误，无法找到指定点击位置！");
            }
        }

        [LuaFunction("ClickMap")]
        public void ClickMap(string id)
        {
            try
            {
                _Gm.Click_MapId(id);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("ClickMap错误，无法找到指定地图！");
            }
        }

        [LuaFunction("CheckPlace")]
        public bool CheckPlace(string key)
        {
            try
            {
                return _Gm.CheckPlace(key);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("CheckPlace错误，无法找到指定地点！");
            }
        }

        [LuaFunction("CheckPlaces")]
        public string CheckPlaces(LuaInterface.LuaTable strs)
        {
            try
            {
                List<string> lst = new List<string>();
                foreach (var x in strs.Values)
                {
                    lst.Add(x.ToString());
                }
                PlaceEnterty pe = _Gm.CheckPlaces(lst.ToArray());
                if (pe == null)
                    return null;
                return pe.Key;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("CheckPlaces，错误无法找到指定地点！");
            }
        }

        [LuaFunction("WaitForPlace")]
        public bool WaitForPlace(string plc,int tm=500,int max=5)
        {
            try
            {
                return _Gm.WaitToPlace(plc, tm, max);
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("WaitForPlace，错误无法找到指定地点！");
            }
        }

        [LuaFunction("WaitForPlaces")]
        public string WaitForPlaces(LuaInterface.LuaTable tbl, int ms = 500, int max = 5)
        {
            try
            {
                List<string> lst = new List<string>();
                foreach (var x in tbl.Values)
                {
                    lst.Add(x.ToString());
                }
                PlaceEnterty pe = _Gm.WaitToPlaces(lst.ToArray(), ms, max);
                if (pe == null)
                    return null;
                return pe.Key;
            }
            catch (KeyNotFoundException ex)
            {
                throw new Exception("WaitForPlaces，错误无法找到指定地点！");
            }
        }
    }
}
