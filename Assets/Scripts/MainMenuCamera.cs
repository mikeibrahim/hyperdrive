using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour {
    // Speed it goes around.
     public int rotateSpeed = -5;
         
 
     void Update () {
         // Rotate around the (z) axis.
         transform.Rotate(rotateSpeed * Time.deltaTime, rotateSpeed / 2 * Time.deltaTime, 0);
 
     }
}
