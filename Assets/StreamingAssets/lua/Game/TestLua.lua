--luanet.load_assembly("UnityEngine")
--GameObject=luanet.import_type("UnityEngine.GameObject");  
--PrimitiveType=luanet.import_type("UnityEngine.PrimitiveType"); 

require "Game/Demo"

ButtonPanel = {};
local this = ButtonPanel;

local transform;
local gameObject;

local goObject;
local xPosition;
local yPosition;

local message;

function OnButtonClick(obj)
	gameObject=obj;
	transform=gameObject.transform;
	this.btnContent= transform:FindChild("BtnContent").gameObject;

	message=gameObject:GetComponent("LuaBehaviour");	

	--this.btnContent:GetComponent("Button").onClick:AddListener(this.OnBtnClick);
	message:AddClick(this.btnContent,this.OnBtnClick);
end

function ButtonPanel.OnBtnClick(go)
	boxGameObject=GameObject.CreatePrimitive(PrimitiveType.Cube);
	boxGameObject.name="ageBox100";
	print("call success");
end

function ButtonPanel.OnIntroClick(obj)
	gameObject=obj;
	transform=gameObject.transform;
	this.btnClose= transform:FindChild("BtnClose").gameObject;
	this.btnPreview=transform:FindChild("BtnPreview").gameObject	

	message=gameObject:GetComponent("LuaBehaviour");	
	message:AddClick(this.btnPreview,this.OnPreviewClick,"JaxImage")
	
	this.btnClose:GetComponent("Button").onClick:AddListener(this.OnCloseClick);	
			
end


function SetGoObject(cube,uiGameObject)
goObject=cube;
-- ui=uiGameObject;
-- this.btnRight=ui.Find("BtnRight").gameObject;
-- this.btnLeft=ui.Find("BtnLeft").gameObject;
-- this.btnRight:GetComponent("Button").onClick:AddListener(this.OnRightClick);	
-- this.btnLeft:GetComponent("Button").onClick:AddListener(this.OnLeftClick);

 xPosition = goObject.transform.position.x ;
 yPosition= goObject.transform.position.y ;	
	
uiGameObject.Find("BtnRight"):GetComponent("Button").onClick:AddListener(this.OnRightClick);	
uiGameObject.Find("BtnLeft"):GetComponent("Button").onClick:AddListener(this.OnLeftClick);
uiGameObject.Find("BtnUp"):GetComponent("Button").onClick:AddListener(this.OnUpClick);	
uiGameObject.Find("BtnDown"):GetComponent("Button").onClick:AddListener(this.OnDownClick);
UpdateBeat:Add(UpdateData, self)	
print("set value success")

end

function ButtonPanel.OnRightClick()
	xPosition=xPosition+0.2;
	goObject.transform.position = Vector3.New(xPosition,yPosition,0)	
end
function ButtonPanel.OnLeftClick()
	xPosition=xPosition-0.2;
	goObject.transform.position = Vector3.New(xPosition,yPosition,0)
end
function ButtonPanel.OnUpClick()
	yPosition=yPosition+0.2;
	goObject.transform.position = Vector3.New(xPosition,yPosition,0)
end
function ButtonPanel.OnDownClick()
	yPosition=yPosition-0.2;
	goObject.transform.position = Vector3.New(xPosition,yPosition,0)
end

function ButtonPanel.OnCloseClick(go)
	gameObject:SetActive(false)	
	DemoCall();
	print("close")	
end
function UpdateData()			
	print("---------每帧执行一次");	
	local Input = UnityEngine.Input;
	local horizontal = Input.GetAxis("Horizontal");
	local verticla = Input.GetAxis("Vertical");
	
	local x = goObject.transform.position.x + horizontal
	local y = goObject.transform.position.y + verticla
	goObject.transform.position = Vector3.New(x,y,0)


end

function ButtonPanel.OnPreviewClick(prefab)
	local go1 = GameObject.Instantiate(prefab);
	go1.transform:SetParent(message.transform);
	go1.transform.localPosition=Vector3.zero;
end