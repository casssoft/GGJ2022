using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class GlobalVariables: MonoBehaviour {
      public static bool followPlayer;
    public static bool playerIsNearSibling;
    public static GameObject sibling;

    void Start() {
         followPlayer = false;
        playerIsNearSibling = false;
        sibling = GameObject.Find("Sibling");
      }
 }
