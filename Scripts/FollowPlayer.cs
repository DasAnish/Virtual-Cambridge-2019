using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FollowPlayer : MonoBehaviour{

    public Transform player;
    public Vector3 offset;
    private double radius=5.0;
    private double offSet=-Math.PI/3    ;

    void Update() {
        double angle = getAngle();
        changePosition(-angle+offSet);
        changeAngles();
    }

    void changePosition(double angle) {
        double x = player.transform.position.x - radius * Math.Cos(angle);
        double z = player.transform.position.z - radius * Math.Sin(angle);
        transform.position = new Vector3((float)x, transform.position.y, (float)z);
    }

    void changeAngles() {
        Vector3 playersAngles = transform.position;
        Vector3 otherAngles = player.transform.position;
        playersAngles.y = 0f;
        otherAngles.y = 0f;

        transform.rotation = Quaternion.LookRotation(otherAngles - playersAngles);
    }

    double getAngle() {
        double angle = player.rotation.eulerAngles.y;
        // angle = angle * Math.PI / 180;
        if (angle < 180) angle = angle * Math.PI / 180f;
        else angle = (angle-360)*Math.PI / 180f;
        return angle;
    }



    // private double previousAngle, ladyAngle, angleTurned;
    // private enum Direction {Left, Right, Standing}
    // private Direction direction, ladyPosition;

    // void Start() {
    //     double angle = getAngle();
    //     ladyAngle = angle - Math.PI/4;
    //     changePosition(ladyAngle);
    //     changeAngles();
    //     ladyPosition = Direction.Left;
    //     //
    //     angleTurned = -Math.PI/4;
    // }


    // void Update() {
    //     double angle = getAngle();
    //     getDirection(angle, previousAngle);
    //     angleTurned +=(direction == Direction.Standing)? 0: getAngleTurned(angle, previousAngle);
    //     ladyPosition = (angleTurned>0) ? Direction.Right : Direction.Left;
    //     Debug.Log(angleTurned + "   " + direction + "    " +ladyPosition + "    " + ladyAngle + "   " + angle);
    //     previousAngle = angle;
    //     if (direction == Direction.Left && ladyPosition == Direction.Right && angleTurned < -Math.PI/4) {
    //         ladyAngle = angle + Math.PI/4;
    //         angleTurned = +Math.PI/4;
    //     } else if (direction == Direction.Right && ladyPosition == Direction.Left && angleTurned > Math.PI/4) {
    //         ladyAngle = angle - Math.PI/4;
    //         angleTurned = -Math.PI/4;
    //     }
    //     changePosition(ladyAngle);
    //     changeAngles();
    // }

    // void getDirection(double curAngle, double prevAngle) {
    //     if (curAngle*prevAngle<0) {
    //         //boundary condition
    //         if (curAngle>prevAngle) direction = Direction.Left;
    //         else direction = Direction.Right;
    //     } else {
    //         //the general case
    //         if (curAngle > prevAngle) direction = Direction.Right;
    //         else if (curAngle < prevAngle) direction = Direction.Left;
    //         else direction = Direction.Standing;
    //     }
    // }

    // double getAngleTurned(double curAngle, double prevAngle) {
    //     if (curAngle * prevAngle < 0) {
    //         //the boundary condition
    //         if (curAngle<0) curAngle = 2*Math.PI + curAngle;
    //         if (prevAngle<0) prevAngle = 2*Math.PI + prevAngle;
    //     }
    //     return curAngle - prevAngle;
    // }


    
}
