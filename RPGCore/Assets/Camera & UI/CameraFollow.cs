using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
  private Transform _followPoint;
  // Use this for initialization
  void Start() 
  {
    _followPoint = GameObject.FindGameObjectWithTag("Player").transform;
  }

  private void LateUpdate()
  {
    transform.position = _followPoint.position;
  }
}
