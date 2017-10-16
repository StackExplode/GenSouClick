using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Reflection;
using LuaInterface;

namespace GenSouClick.Helpers
{
    /// <summary>  
    /// Lua函数描述特性类  
    /// </summary>  
    public class LuaFunction : Attribute
    {
        private String FunctionName;

        public LuaFunction(String strFuncName)
        {
            FunctionName = strFuncName;
        }

        public String getFuncName()
        {
            return FunctionName;
        }
    }

    /// <summary>  
    /// Lua引擎  
    /// </summary>  
    class LuaFramework
    {
        private Lua pLuaVM = new Lua();//lua虚拟机  

        /// <summary>  
        /// 注册lua函数  
        /// </summary>  
        /// <param name="pLuaAPIClass">lua函数类</param>  
        public void BindLuaApiClass(Object pLuaAPIClass)
        {
            foreach (MethodInfo mInfo in pLuaAPIClass.GetType().GetMethods())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr is LuaFunction)
                    {
                        string LuaFunctionName = (attr as LuaFunction).getFuncName();
                        pLuaVM.RegisterFunction(LuaFunctionName, pLuaAPIClass, mInfo);
                    }   
                }
            }
        }

        /// <summary>  
        /// 执行lua脚本文件  
        /// </summary>  
        /// <param name="luaFileName">脚本文件名</param>  
        public void ExecuteFile(string luaFileName)
        {
            try
            {
                pLuaVM.DoFile(luaFileName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>  
        /// 执行lua脚本  
        /// </summary>  
        /// <param name="luaCommand">lua指令</param>  
        public void ExecuteString(string luaCommand)
        {
            try
            {
                pLuaVM.DoString(luaCommand);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 执行Lua函数
        /// </summary>
        /// <param name="fun">函数名</param>
        /// <param name="pa">参数列表</param>
        /// <returns></returns>
        public object ExecuteFunc(string fun,params object[] pa)
        {
            LuaInterface.LuaFunction fff = pLuaVM.GetFunction(fun);
            object[] obj = fff.Call(pa);
            if (obj != null)
                return obj[0];
            else
                return null;
        }
    }
}