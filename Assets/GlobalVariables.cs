using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class GlobalVariables: MonoBehaviour {
      public static bool followPlayer;
      public static bool hasFrisbee;
      public static bool gaveFrisbeeToDoggo;
    public static bool playerIsNearSibling;
    public static GameObject sibling;

    void Start() {
          sibling = GameObject.Find("Sibling");

         // sibling behavior
         followPlayer = false;
         playerIsNearSibling = false;

         // player accomplishments 
         hasFrisbee = false;
         gaveFrisbeeToDoggo = false;
      }
 }
