using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using GenSouClick.Helpers;
using GenSouClick.Enterty;

namespace GenSouClick.BLL
{
    abstract class ShuaBase
    {
        Thread th;


        public bool Running { get; protected set; }
        public event Action<bool, string> onFinished;
        public event Action onStart;
        public event Action onOneTurnFinished;
        public int Total { get; set; }
        public int Count { get; set; }

        public ShuaBase(int total = 99)
        {
            th = new Thread(new ThreadStart(Mission));
            Total = total;
        }

        protected void DoOnStart()
        {
            if(onStart != null)
                onStart();
        }

        protected void DoOnOneTurn()
        {
            if (onOneTurnFinished != null)
                onOneTurnFinished();
        }

        protected void DoOnFinished(bool b, string s)
        {
            if (onFinished != null)
                onFinished(b, s);
        }

        

        [Obsolete("过时了",true)]
        protected bool WaitForwardButton(Game gm)
        {
            int max = 20,ms=200;
            while (max-- > 0)
            {
                Thread.Sleep(ms);
                if (gm.CheckPlace("forward"))
                    return true;
            }
            return false;
        }

        protected void Sleep(int ms)
        {
            Thread.Sleep(ms);
        }

        protected abstract void Mission();

        public void Start()
        {
            if (!Running)
            {
                th.Start();
                Running = true;
            }
            else
                throw new Exception("已经开始任务！");
        }

        public void Stop()
        {
            if (Running)
                Running = false;
            else
                throw new Exception("任务尚未开始！");
        }

        public void Abort()
        {
            th.Abort();

        }


    }
}
