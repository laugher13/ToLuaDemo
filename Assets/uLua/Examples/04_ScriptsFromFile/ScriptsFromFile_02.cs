﻿using UnityEngine;
using System.Collections;
using LuaInterface;
using UnityEngine.UI;

public class ScriptsFromFile_02 : MonoBehaviour
{
    // Use this for initialization
    public Text txtDisplay;
    void Start()
    {
        //只是展示如何加载文件。不是推荐这么做
        LuaState l = new LuaState();
        string path = Application.dataPath + "/uLua/Examples/04_ScriptsFromFile/ScriptsFromFile02.lua";        
        l.DoFile(path);    
    }

    // Update is called once per frame
    void Update()
    {

    }
}
