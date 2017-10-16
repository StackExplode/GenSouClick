using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using GenSouClick.Enterty;
using System.Windows.Forms;

namespace GenSouClick.BLL
{
    class ShuaLua:ShuaBase
    {
        private bool _Looping = true;
        private LuaExporter _LEX;
        private string _Err = "";
        private bool _Fail = false;
        private Action<string,int> _logger;
        public Action<string> ShowMsg;
        public Action<int> SetLable;
        public ShuaLua(string luapath, Action<string, int> log)
            : base(99)
        {
            try
            {
                _logger = log;
                _LEX = new LuaExporter(luapath);     
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, 3);
                DoOnFinished(true, "初始化错误！");
            }
            
        }
        protected override void Mission()
        {
            try
            {
                Game gm = new Game();
                _LEX.RegistFunc(gm, this);
                _LEX.DoSetup();
                base.DoOnStart();
                while (Running && _Looping)
                {
                    _LEX.DoLoop();

                    base.DoOnOneTurn();

                    Sleep(200);
                }
                Running = false;
                _LEX.DoEndup(_Fail, _Err);
                base.DoOnFinished(_Fail, _Err);
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message, 3);
                DoOnFinished(true, "执行脚本过程中出错！");
            }
        }

        public void WriteLog(string s, int lv)
        {
            if(_logger !=null)
                _logger(s,lv);
        }

        public void SetError(string s)
        {
            _Fail = true;
            _Err = s;
        }

        public void BreakLoop()
        {
            _Looping = false;
        }
    }
}
