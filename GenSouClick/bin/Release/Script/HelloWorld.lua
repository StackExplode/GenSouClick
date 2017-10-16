--开刷前的配置代码
function Setup()
	local a = 10
	ClickSingle("saizen") --领取赛钱
	Delay(500) --延迟1秒
	ClickSingle("main_blank") --点击空白处
	LogInfo("我开始刷图了！我是信息")  --记录信息
	LogWarn("警告：这只是一个演示Demo！") --记录警告
	LogDebug("这是一条调试记录 a="..a)	--记录调试
end

local rest = 5
--循环代码
function Loop()


	local rt = Fun1();	--执行函数1
	if(rt[1] == false) then	--如果执行函数1出错
		SetError(rt[2])	--设置返回的错误信息
		BreakLoop(); --退出大循环
		return;	
	end
	
	Fun2("我是测试函数2") --执行测试函数2
	
	if(rest <= 0)
	then
		BreakLoop()	--退出大循环
		return
	end
	rest = rest - 1
	SetLabel(rest)	--设置“剩余次数”标签

end

--结束收尾代码
function Finalize(_iserr,_errinfo)	--_iserr=是否出错 _errinfo=错误信息
	if(not _iserr) then
		ShowMessageBox("刷完了！")
		LogError("错误：你没有和作者交易！")		
	else
		LogError(_errinfo)  --记录错误
	end
end


function Fun1()
	succ = CheckPlace("main")	--检查当前界面是否为主界面
	if(not succ) then
		
		return {false,"没有从主界面开始！"}
	end
	Delay(500);	--延时500毫秒
	ClickSingle("taiji");	--点击退治按钮
	succ = WaitForPlace("map")	--等待到达大地图界面
	if(not succ) then
		return {false,"进入大地图失败！"}
	end
	ClickMap("hakurei")	--点击大地图“博丽神社”
	WaitForPlace("main")	--等待回到主界面

	return {true};
end

function Fun2(a)
	local i = 10
	while i>0 do
		LogDebug(a)
		Delay(200)
		i = i - 1
	end
	
end