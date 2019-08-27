using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
using System.Linq;

/// <summary>
/// Add environment mappings to Webbs Court environment
/// </summary>
public class WebbsWalk : MonoBehaviour {

    private Dictionary<int, Dictionary<int, int>> EnvMap;

    PositionSingleton position = PositionSingleton.getInstance();
    Material mainMat;
    int steps = 250;
    static int CurPos;


    // Called every frame
    void FixedUpdate () {
        // if (position.walk == PositionSingleton.Walk.WebbsBack) steps = 250;
        // else steps = 250;
        RenderSettings.skybox.SetFloat("_LerpValue",
            Math.Min(1, RenderSettings.skybox.GetFloat("_LerpValue") + 1f/steps)); 
    }

    // Use this for initialization
    void Start () {
        mainMat = Resources.Load("mats/Main", typeof(Material)) as Material;
        position.webbsWalk = this;

        EnvMap = new Dictionary<int, Dictionary<int, int>>();
        for (int i = 1; i < 18; i++) {
            EnvMap.Add(i, new Dictionary<int, int>());
        }
        for(int i=179; i < 360; i++) {
            EnvMap.Add(i, new Dictionary<int, int>());
        }
        EnvMap[1].Add(5, 2); EnvMap[1].Add(7, 3); EnvMap[1].Add(6, 5);
        EnvMap[2].Add(0, 1); EnvMap[2].Add(4, 10); EnvMap[2].Add(6, 5); EnvMap[2].Add(5, 6);
        EnvMap[3].Add(2,1); EnvMap[3].Add(3,2); EnvMap[3].Add(4, 10); EnvMap[3].Add(6, 5); EnvMap[3].Add(7, 4);
        EnvMap[4].Add(2, 3); EnvMap[4].Add(3, 1); EnvMap[4].Add(5, 5);
        EnvMap[5].Add(0, 4); EnvMap[5].Add(1, 3); EnvMap[5].Add(2, 1); EnvMap[5].Add(3, 10); EnvMap[5].Add(4, 7);
        EnvMap[6].Add(2, 10); EnvMap[6].Add(3, 9); EnvMap[6].Add(6, 8); EnvMap[6].Add(7, 7);
        EnvMap[7].Add(0, 5); EnvMap[7].Add(3, 6); EnvMap[7].Add(4, 8);
        EnvMap[8].Add(2, 9); EnvMap[8].Add(6, 7); EnvMap[8].Add(0, 6); EnvMap[8].Add(1, 10);
        EnvMap[9].Add(1, 11); EnvMap[9].Add(6, 10); EnvMap[9].Add(4, 8); EnvMap[9].Add(3, 6);
        EnvMap[10].Add(1, 2); EnvMap[10].Add(4, 9); EnvMap[10].Add(5, 6); EnvMap[10].Add(6, 8);
        EnvMap[11].Add(7, 9); EnvMap[11].Add(4, 12);
        EnvMap[12].Add(6, 11); EnvMap[12].Add(4, 13);
        EnvMap[13].Add(1, 12); EnvMap[13].Add(3, 14); EnvMap[13].Add(4, 16); EnvMap[13].Add(5, 15);
        EnvMap[14].Add(0, 13); EnvMap[14].Add(6, 16);
        EnvMap[15].Add(2, 13); EnvMap[15].Add(6, 16);
        EnvMap[16].Add(0, 15); EnvMap[16].Add(6, 17); EnvMap[16].Add(2, 14);
        EnvMap[17].Add(2, 16); EnvMap[17].Add(0, 8);

        EnvMap[179].Add(0, 180);
        EnvMap[202].Add(4, 201);    

        for (int i = 180; i < 202; i++) {
            if(i == 197) {
                EnvMap[i].Add(6, i + 1);
                EnvMap[i].Add(4, i - 1);
                // continue;
            } else {
                EnvMap[i].Add(0, i + 1);
                EnvMap[i].Add(4, i - 1);
            }
        }

        //This defines the start point of the game, change to name of initial mat (make sure to drag initial mat to unity editor)
        if (RenderSettings.skybox.ToString().Equals("mat179"+ " (UnityEngine.Material)")) {
            CurPos = 179;
            LoadEnvironmentConfiguration(179);
        }
    }

    

    public void LoadEnvironmentConfiguration(int i) {
        CurPos = i;
        position.CurPos = i;

        if (EnvMap == null) Start();

        //Getting list of all objects
        var objList = FindObjectsOfTypeAll<Transform>();

        //Getting list of all panels
        var panels = GameObject.Find("panels");
        var gList = new List<GameObject>();
        for(int j = 0; j < objList.Count; j++) {
            if (objList.ElementAt(j).parent && objList.ElementAt(j).parent.gameObject == panels) {
                gList.Add(objList.ElementAt(j).gameObject);
            }
        }

        //Getting list of all arrows
        var arrows = GameObject.Find("arrows");
        var aList = new List<GameObject>();
        for (int j = 0; j < objList.Count; j++) {
            if (objList.ElementAt(j).parent && objList.ElementAt(j).parent.gameObject == arrows) {
                aList.Add(objList.ElementAt(j).gameObject);
            }
        }
        
        //Acivating correct panels
        for (int j = 0; j < 8; j++) {
            if (EnvMap[i].ContainsKey(j)) {
                gList[j].SetActive(true);
            }
            else {
                gList[j].SetActive(false);
            }
        }

        //Acivating correct arrows
        for (int j = 0; j < 8; j++) {
            if (EnvMap[i].ContainsKey(j)) {
                aList[j].SetActive(true);
            }
            else {
                aList[j].SetActive(false);
            }
        }



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

            if (objList.ElementAt(j).gameObject.name == "Boxes")
            {
                if (i == 197)
                    objList.ElementAt(j).gameObject.SetActive(true);
                else objList.ElementAt(j).gameObject.SetActive(false);
            }
        }

        ChangeMat(i);
    }
    


    void ChangeMat(int i) {
        RenderSettings.skybox = mainMat;
        Material newMat = Resources.Load("mats/mat" + i, typeof(Material)) as Material;
        RenderSettings.skybox.SetTexture("_Tex", RenderSettings.skybox.GetTexture("_Tex2"));
        RenderSettings.skybox.SetTexture("_Tex2", newMat.GetTexture("_Tex"));
        RenderSettings.skybox.SetFloat("_LerpValue", 0f);
        
    }

	// Update is called once per frame
	public void ChangeSkybox (int dir) {
        if (EnvMap == null) Start();
        // Debug.Log(CurPos + " " + dir + " " + EnvMap[CurPos][dir]);
        LoadEnvironmentConfiguration(EnvMap[CurPos][dir]);
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
