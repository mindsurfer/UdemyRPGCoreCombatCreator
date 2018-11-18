using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
  [SerializeField] private Transform FollowPoint;

  // Use this for initialization
  void Start() 
  {
    
  }

  private void LateUpdate()
  {
    transform.position = FollowPoint.position;
  }
}
