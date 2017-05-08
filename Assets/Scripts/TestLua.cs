using UnityEngine;
using System.Collections;
using System.IO;

public class TestLua : MonoBehaviour {

	
	void Start () {
       
        LuaScriptMgr luamgr = new LuaScriptMgr();
        luamgr.Start();
        luamgr.DoFile("Game/Demo");
        //luamgr.CallLuaFunction("NewGameObject");
	}
	

	void Update () {
	
	}
}
