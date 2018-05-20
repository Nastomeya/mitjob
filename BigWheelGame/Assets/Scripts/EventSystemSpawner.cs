﻿using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemSpawner : MonoBehaviour 
{
 void Start()
 {
     EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();
     if (sceneEventSystem == null)
     {
         GameObject eventSystem = new GameObject("EventSystem");
         eventSystem.AddComponent<EventSystem>();
         eventSystem.AddComponent<StandaloneInputModule>();
     }
 }
}
 