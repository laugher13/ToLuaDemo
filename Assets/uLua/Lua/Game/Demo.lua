function DemoCall()
	-- local num=0;
	-- num=num+10;
	-- print("--------------"..num)
	myFunc();

end

  function CoFunc()
    print('Coroutine started')
    local i = 0
    for i = 0, 10, 1 do                                
        coroutine.wait(1)
		print("-------"..i.."-------")		
    end
    print('Coroutine ended')
end

 function myFunc()
    coroutine.start(CoFunc)
	UpdateBeat:Add(UpdateCall, self)
end

function UpdateCall()
	local Input = UnityEngine.Input;
	if Input.GetKeyDown(KeyCode.Space) then
	
	end
end