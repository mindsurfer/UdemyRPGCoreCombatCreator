﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour 
{
  private CameraRaycaster _cameraRayCaster;

  // Use this for initialization
  void Start() 
  {
    _cameraRayCaster = GetComponent<CameraRaycaster>();
  }
  
  // Update is called once per frame
  void Update() 
  {
    Debug.Log(_cameraRayCaster.LayerHit);
  }
}
