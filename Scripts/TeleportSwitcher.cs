using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used when clicked on the teleport sphere above to change environments
/// Initial environments are Kings Walk, Webbs court and the Computer Lab
/// </summary>
public class TeleportSwitcher : MonoBehaviour {

    //bool first = false;
    PositionSingleton position = PositionSingleton.getInstance();

    void Start()
    {
        position.webbsWalk = new WebbsWalk();
        position.clWalk = new ClWalk();
    }


    public void Switch() {
        if(position.walk == PositionSingleton.Walk.WebbsBack){
            position.walk = PositionSingleton.Walk.ComputerLab;
            //position.CurPos = 0;
            //ClWalk walk = new ClWalk();
            position.clWalk.LoadEnvironmentConfiguration(0);
        } else if (position.walk == PositionSingleton.Walk.WebbsFront){
            position.walk = PositionSingleton.Walk.WebbsBack;
            //position.CurPos = 179;
            //WebbsWalk walk = new WebbsWalk();
            position.webbsWalk.LoadEnvironmentConfiguration(179);
        } else if (position.walk == PositionSingleton.Walk.ComputerLab){
            position.walk = PositionSingleton.Walk.WebbsFront;
            //position.CurPos = 1;
            //WebbsWalk walk = new WebbsWalk();
            position.webbsWalk.LoadEnvironmentConfiguration(1);
        }
    }

    public void ChangeSkybox() {

        int dir = Convert.ToInt32(gameObject.name);
        //Debug.Log(gameObject);

        if (position.walk == PositionSingleton.Walk.ComputerLab) {
            //var c = new ClWalk();
            position.clWalk.ChangeSkybox(dir);
            
        }
        else {
            //var s = new WebbsWalk();
            position.webbsWalk.ChangeSkybox(dir);
        }
    }
}
