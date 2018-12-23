using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
  [SerializeField] float MaxHP = 100f;

  private float _currentHP = 100f;

  public float HealthAsPercentage
  {
    get { return _currentHP / MaxHP; }
  }
}
