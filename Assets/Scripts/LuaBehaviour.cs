using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LuaInterface;

public class LuaBehaviour : MonoBehaviour {

    //public void AddClick(GameObject go, LuaScriptMgr luaMgr)
    //{
    //    if (go == null) return;      
    //    go.GetComponent<Button>().onClick.AddListener(
    //        delegate()
    //        {
    //            luaMgr.CallLuaFunction("OnBtnClick");
    //            //print("call success");               
    //        } 
    //    );
    //}
    public void AddClick(GameObject go, LuaFunction luafunc, string goName = null)
    {
        if (go == null) return;
        go.GetComponent<Button>().onClick.AddListener(
            delegate()
            {
                if (goName == null)
                {
                    luafunc.Call(go);
                } else
                {
                    GameObject jax = Resources.Load<GameObject>(goName);
                    luafunc.Call(jax);
                }
            }
        );
    }

    public void LoadPrefab()
    {

    }
}
