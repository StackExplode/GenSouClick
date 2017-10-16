local aaa = 100 --总共刷100次
local succ
--开刷前的配置代码
function Setup()
	LogInfo("开始刷")  --记录信息
	SetLabel(aaa);	--设置“剩余次数”标签值
	--ClickSingle("saizen");
	--Delay(200);
	
end

--循环代码
function Loop()
	

	local rt = MyMission();
	if(rt[1] == false) then
		SetError(rt[2])	--设置返回的错误信息
		BreakLoop(); --退出大循环
		return;	
	end
	
	if(aaa <= 0)
	then
		BreakLoop();
		return
	end
	aaa = aaa - 1;
	SetLabel(aaa);
end

--结束收尾代码
function Finalize(_iserr,_errinfo)	--_iserr=是否出错 _errinfo=错误信息
	if(not _iserr) then
		LogError("刷完了！");
		ShowMessageBox("刷完了！");
	else
		LogError(_errinfo);  --记录错误
	end
end

function MyMission()
	succ = CheckPlace("main")	--检查当前界面
	if(not succ) then
		
		return {false,"没有从主界面开始！"};
	end
	Delay(500);
	ClickSingle("taiji");	--点击退治
	succ = WaitForPlace("map")	--等待到达大地图界面
	if(not succ) then
		return {false,"进入大地图失败！"}
	end
	ClickMap("gengo")	--选择大地图“玄武之湖”
	Delay(500)
	ClickGroup("team",3)	--选择队伍3
	Delay(160)
	ClickGroup("submap",1)	--选择子地图1
	succ = WaitForPlace("hardness",200)
	if(not succ) then
		return {false,"进入难度选择界面失败！"}
	end
	ClickGroup("hardness", "normal");
	Delay(100)
	ClickSingle("go");
	succ = WaitForPlace("standby")
	if(not succ) then
		return {false,"进入难度选择界面失败！"}
	end
	while true do
		succ = WaitForPlace("forward",200,20);	--等待到达前进界面，轮训周期200，最多检查20次
		if(not succ) then
            break;
    	end
    	ClickSingle("forward")
    	WaitForPlace("scorebord", 500, 999);	--等待计分板界面出现
    	ClickSingle("center")
    	local plc = WaitForPlaces({"map","forward"},200,25)	--等待多个场景之一出现
    	if(plc == "map") then
	    	break;
    	end
	end
	ClickMap("hakurei")
	WaitForPlace("main",200,20)
	
	
	
	return {true,""};
end