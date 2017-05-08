using UnityEngine;
using System.Collections;
using LuaInterface;
using System.IO;

public class ToLuaTest : MonoBehaviour
{
    private LuaScriptMgr luaMgr;
    //private LuaState luaState;
    //public TextAsset txtAsset;
    //public GameObject btnList;
    private string filePath;
    private GameObject intro;
    private string message;

    public GameObject cube;


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
      
    }

    void Start()
    {
        //StartCoroutine(ReadUI());
        CheckExtractResource();
    }

    void Update()
    {
        if (luaMgr != null)
        {
            luaMgr.Update();
        }
    }

    void LateUpdate()
    {
        if (luaMgr != null)
        {
            luaMgr.LateUpate();
        }
    }

    void FixedUpdate()
    {
        if (luaMgr != null)
        {
            luaMgr.FixedUpdate();
        }
    }

    /// <summary>
    /// 释放资源
    /// </summary>
    public void CheckExtractResource()
    {
        string path = Application.persistentDataPath + "/Game/";
        bool isExists = Directory.Exists(path) &&
          Directory.Exists(path + "lua/") && File.Exists(path + "files.txt");
        if (isExists)
        {
            luaMgr = new LuaScriptMgr();
            luaMgr.DoFile("Game/TestLua");
            luaMgr.Start();
            print("-----------文件已经解压过了-----------");
            return;   //文件已经解压过了，自己可添加检查文件列表逻辑
        }
        StartCoroutine(OnExtractResource());    //启动释放协成 
    }

    public void LoadAssetBundle()
    {
        if (intro == null)
        {
            StartCoroutine(ReadUI());
        } else
        {
            intro.SetActive(true);
        }
    }

    public void StartLuaUpdate()
    {
        luaMgr.CallLuaFunction("SetGoObject", cube, this.gameObject);
    }

    /// <summary>
    /// 加载Assetbundle
    /// </summary>
    /// <returns></returns>
    
    IEnumerator ReadUI()
    {
        string path = filePath + "intro";

        WWW www = new WWW(path);

        yield return www;

        AssetBundle bundle = www.assetBundle;

        GameObject go = bundle.LoadAsset<GameObject>("Intro");

        if (go != null)
        {
            //GameObject intro = Instantiate<GameObject>(go);
            intro = Instantiate<GameObject>(go);
            intro.AddComponent<LuaBehaviour>();
            intro.transform.parent = this.transform;
            intro.transform.localPosition = Vector3.zero;
            intro.transform.localScale = Vector3.one;
            luaMgr.CallLuaFunction("ButtonPanel.OnIntroClick", intro);


        } else
        {
            Debug.LogError("text == null");
        }
    }

    /// <summary>
    /// 游戏开始前解压ToLua包
    /// </summary>
    /// <returns></returns>
    IEnumerator OnExtractResource()
    {
        string dataPath = Application.persistentDataPath + "/Game/";
        string resPath = Application.streamingAssetsPath + "/";

        if (Directory.Exists(dataPath)) Directory.Delete(dataPath, true);
        Directory.CreateDirectory(dataPath);

        string infile = resPath + "files.txt";
        string outfile = dataPath + "files.txt";
        if (File.Exists(outfile)) File.Delete(outfile);

        message = "正在解包文件:>files.txt";
        print("-----------正在解包文件-----------");
        Debug.Log(infile);
        Debug.Log(outfile);
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW www = new WWW(infile);
            yield return www;

            if (www.isDone)
            {
                File.WriteAllBytes(outfile, www.bytes);
            }
            yield return 0;
        } else File.Copy(infile, outfile, true);
        yield return new WaitForEndOfFrame();

        //释放所有文件到数据目录
        string[] files = File.ReadAllLines(outfile);
        foreach (var file in files)
        {
            string[] fs = file.Split('|');
            infile = resPath + fs[0];  //
            outfile = dataPath + fs[0];

            message = "正在解包文件:>" + fs[0];
            Debug.Log("正在解包文件:>" + infile);

            string dir = Path.GetDirectoryName(outfile);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            if (Application.platform == RuntimePlatform.Android)
            {
                WWW www = new WWW(infile);
                yield return www;

                if (www.isDone)
                {
                    File.WriteAllBytes(outfile, www.bytes);
                }
                yield return 0;
            } else
            {
                if (File.Exists(outfile))
                {
                    File.Delete(outfile);
                }
                File.Copy(infile, outfile, true);
            }
            yield return new WaitForEndOfFrame();
        }
        message = "解包完成!!!";
        Debug.Log(message);
        yield return new WaitForSeconds(0.1f);

        luaMgr = new LuaScriptMgr();
        luaMgr.DoFile("Game/TestLua");
        luaMgr.Start();

        message = string.Empty;
        //释放完成，开始启动更新资源
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 30, 960, 50), message);        
    }
}