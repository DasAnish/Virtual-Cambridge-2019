using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

/// <summary>
/// Add environment mappings to Computer Lab environment
/// </summary>
public class ClWalk : MonoBehaviour {

    PositionSingleton position = PositionSingleton.getInstance();
    Material mainMat;
    int steps = 100;
    public static int CurPos;

    void FixedUpdate() {
        RenderSettings.skybox.SetFloat("_LerpValue",
            Math.Min(1, RenderSettings.skybox.GetFloat("_LerpValue") + 1f / steps));
    }

    private Dictionary<int, Dictionary<int, int>> ClMap;
    // Use this for initialization
    void Start() {
        mainMat = Resources.Load("mats/Main", typeof(Material)) as Material;
        position.clWalk = this;

        ClMap = new Dictionary<int, Dictionary<int, int>>();
      
        for (int i = 0; i < 9; i++) {
            ClMap.Add(i, new Dictionary<int, int>());
        }
        ClMap[0].Add(0, 1);
        ClMap[1].Add(0, 2); ClMap[1].Add(4, 0);
        ClMap[2].Add(0, 3); ClMap[2].Add(4, 1);
        ClMap[3].Add(0, 4); ClMap[3].Add(4, 2);
        ClMap[4].Add(0, 5); ClMap[4].Add(4, 3);
        ClMap[5].Add(0, 6); ClMap[5].Add(4, 4);
        ClMap[6].Add(0, 7); ClMap[6].Add(4, 5);
        ClMap[7].Add(0, 8); ClMap[7].Add(4, 6);
        ClMap[8].Add(4, 7);
        
    }

    public void LoadEnvironmentConfiguration(int i) {
        CurPos = i;
        position.CurPos = i;

        if (ClMap == null) Start();
        //Getting list of all panels
        var panelList = FindObjectsOfTypeAll<Transform>();
        var panels = GameObject.Find("panels");
        var gList = new List<GameObject>();
        for (int j = 0; j < panelList.Count; j++) {
            if (panelList.ElementAt(j).parent && panelList.ElementAt(j).parent.gameObject == panels) {
                gList.Add(panelList.ElementAt(j).gameObject);
            }
        }

        //Getting list of all arrows
        var arrowList = FindObjectsOfTypeAll<Transform>();
        var arrows = GameObject.Find("arrows");
        var aList = new List<GameObject>();
        for (int j = 0; j < arrowList.Count; j++) {
            if (arrowList.ElementAt(j).parent && arrowList.ElementAt(j).parent.gameObject == arrows) {
                aList.Add(arrowList.ElementAt(j).gameObject);
            }
        }
      
        //Acivating correct panels
        for (int j = 0; j < 8; j++) {
            if (ClMap[i].ContainsKey(j)) {
                gList[j].SetActive(true);
            }
            else {
                gList[j].SetActive(false);
            }
        }

        //Acivating correct arrows
        for (int j = 0; j < 8; j++) {
            if (ClMap[i].ContainsKey(j)) {
                aList[j].SetActive(true);
            }
            else {
                aList[j].SetActive(false);
            }
        }
        var objList = FindObjectsOfTypeAll<Transform>();
        //Getting man
        for (int j = 0; j < objList.Count; j++)
        {
            if (objList.ElementAt(j).gameObject.name == "PuntLady")
            {
                objList.ElementAt(j).gameObject.SetActive(true);
            }
            else if (objList.ElementAt(j).gameObject.name == "Reply Text")
            {
                objList.ElementAt(j).gameObject.SetActive(true);
            }
        }


        ChangeMat(i);
    }



    void ChangeMat(int i) {
        
        RenderSettings.skybox = mainMat;
        Material newMat = Resources.Load("mats/matstitch" + i, typeof(Material)) as Material;
        RenderSettings.skybox.SetTexture("_Tex", RenderSettings.skybox.GetTexture("_Tex2"));
        RenderSettings.skybox.SetTexture("_Tex2", newMat.GetTexture("_Tex"));
        RenderSettings.skybox.SetFloat("_LerpValue", 0f);
    }
    // Update is called once per frame
    public void ChangeSkybox(int dir) {
        if (ClMap == null) Start();
        LoadEnvironmentConfiguration(ClMap[CurPos][dir]);
    }

    public static List<T> FindObjectsOfTypeAll<T>() {
        var results = new List<T>();
        for (int i = 0; i < SceneManager.sceneCount; i++) {
            var s = SceneManager.GetSceneAt(i);
            if (s.isLoaded) {
                var allGameObjects = s.GetRootGameObjects();
                for (int j = 0; j < allGameObjects.Length; j++) {
                    var go = allGameObjects[j];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
        }
        return results;
    }
}
