using UnityEngine;
using System.Collections;
using LuaInterface;
using UnityEngine.UI;

public class GameStart : MonoBehaviour {

    private LuaScriptMgr luaMgr;
    LuaState luaState;

    public GameObject go;
    public TextAsset txtAsset;
    private Button btn1;
    private Button btn2;
    private Button btnText;
    private Text txtDisplay;
    string filePath;
    private Button button1;
    public GameObject btn;

    private Coroutine coroutine;

    void Awake()
    {
        //不同平台下StreamingAssets的路径是不同的，这里需要注意一下。	
#if UNITY_ANDROID
        filePath = Application.streamingAssetsPath + "/ABs/";
#elif UNITY_IPHONE
        filePath = "file://" + Application.streamingAssetsPath + "/ABs/";
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        filePath = "file://" + Application.streamingAssetsPath + "/ABs/";
#endif
        //filePath = Application.streamingAssetsPath + "/LuaText.txt";
        btn1 = this.transform.FindChild("Btn1").GetComponent<Button>();
        btn2 = this.transform.FindChild("Btn2").GetComponent<Button>();
        button1 = this.transform.FindChild("Button1").GetComponent<Button>();
        btnText = this.transform.FindChild("BtnText").GetComponent<Button>();
        txtDisplay = this.transform.FindChild("Text").GetComponent<Text>();

        btn1.onClick.AddListener(delegate
        {
            OnButton1Click(); 
            //StartCoroutine(ReadTest());
        });
        btn2.onClick.AddListener(OnButton2Click);
        button1.onClick.AddListener(InitButton);
        btnText.onClick.AddListener(delegate
        {
            OnTextClick();
        });        

    }
    
	void Start () {

        luaMgr = new LuaScriptMgr();
        luaMgr.DoString(txtAsset.text);
        luaMgr.Start();
        luaState = new LuaState();
        print("init success");       
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //string path = "jar:file://" + Application.streamingAssetsPath + "/LuaTest.lua";
        //    string path = Application.streamingAssetsPath + "/LuaTest.lua";
        //    LuaScriptMgr lua = new LuaScriptMgr();
        //    lua.DoString(txtAsset.text);  
        //    //lua.DoFile(path);
        //    LuaFunction lf = lua.GetLuaFunction("Add");
        //    object[] result = lf.Call(2);
        //    txtDisplay.text = result[0].ToString();
        //    lua.Start();
          
        //    //luaState.DoFile(path);
        //    //LuaFunction lf = luaState.GetFunction("Add");
        //    //object[] result = lf.Call(2);
        //    //txtDisplay.text = result[0].ToString();
           
        //}

        if (Input.GetKeyDown(KeyCode.A))
        {
           
            //luaMgr.CallLuaFunction("OnButtonClick", btn);   
            //LuaFunction luafunc = luaMgr.GetLuaFunction("doClick");        
            AddClick(btn, luaMgr);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnButton1Click();
            print("----press space----");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnButton2Click();
            print("----press space----");
        }
    }

    void InitButton()
    {
        AddClick(btn, luaMgr);
    }

    public void AddClick(GameObject go, LuaScriptMgr luafunc)
    {
        if (go == null) return;
        go.GetComponent<Button>().onClick.AddListener(
            delegate()
            {
                //luafunc.Call(go);
                luafunc.CallLuaFunction("NewGameObject");
            }
        );
    }

    //public void AddClick(GameObject go, LuaFunction luafunc)
    //{
    //    if (go == null) return;
    //    go.GetComponent<Button>().onClick.AddListener(
    //        delegate()
    //        {
    //            //luafunc.Call(go);
    //            luafunc.Call("doClick");                
    //        }
    //    );
    //}

    private void OnButton1Click()
    {
        string path = Application.streamingAssetsPath + "/LuaTest.lua";

        //LuaScriptMgr lua = new LuaScriptMgr();
        //lua.DoString(txtAsset.text);
        //lua.CallLuaFunction("HideGameObject", go);

        luaMgr.DoString(txtAsset.text);
        luaMgr.CallLuaFunction("HideGameObject", go);     
        

        //object[] result = lf.Call(2);      
        //txtDisplay.text = result[0].ToString();
        //print(result[0]);
        //print("path:" + filePath);
    }
    private void OnButton2Click()
    {
        luaMgr.DoString(txtAsset.text);
        luaMgr.CallLuaFunction("ShowGameObject", go);          
    }
    private void OnTextClick()
    {
        txtDisplay.text = filePath;
        StartCoroutine(ReadTest());
    }

    IEnumerator ReadTest()
     {
         string path = filePath + "luatest";
 
         WWW www = new WWW(path);
 
         yield return www;
 
         AssetBundle bundle = www.assetBundle;

         TextAsset txt = bundle.LoadAsset<TextAsset>("LuaTest");

         if (txt != null)
         {
             //print(txt.text);
             //LuaState lu = new LuaState();
             //luaState.DoString(txt.text);
             //LuaFunction lf = luaState.GetFunction("luaTest");
             //object[] result = lf.Call(2);
             //txtDisplay.text = result[0].ToString();
             //print(result[0]);

             luaMgr.DoString(txt.text);
             LuaFunction lf = luaMgr.GetLuaFunction("luaTest");
             object[] result = lf.Call(2);
             txtDisplay.text = result[0].ToString();
             print(result[0]);           

         } else
         {
             Debug.LogError("text == null");             
         }

        
     }
  
}
