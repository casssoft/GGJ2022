using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class GlobalVariables: MonoBehaviour {
      public static bool followPlayer;
      public static bool siblingSaved;
      
      void Start() {
         followPlayer = false;
         siblingSaved = false;
      }
 }
