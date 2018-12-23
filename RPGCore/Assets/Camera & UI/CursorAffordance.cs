using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]
public class CursorAffordance : MonoBehaviour 
{
  [SerializeField] Texture2D WalkCursor;
  [SerializeField] Texture2D AttackCursor;
  [SerializeField] Texture2D QuestionCursor;

  [SerializeField] Vector2 CursorHotspot = new Vector2(0, 0);

  private CameraRaycaster _cameraRayCaster;

  // Use this for initialization
  void Start() 
  {
    _cameraRayCaster = GetComponentInChildren<CameraRaycaster>();
    _cameraRayCaster.OnLayerChanged += CursorAffordance_OnLayerChanged;
  }
  
  private void CursorAffordance_OnLayerChanged(Layer newLayer) 
  {
    switch (newLayer)
    {
      case Layer.Walkable: Cursor.SetCursor(WalkCursor, CursorHotspot, CursorMode.Auto); break;
      case Layer.Enemy: Cursor.SetCursor(AttackCursor, CursorHotspot, CursorMode.Auto); break;
      case Layer.RaycastEndStop: Cursor.SetCursor(QuestionCursor, CursorHotspot, CursorMode.Auto); break;
      default: Cursor.SetCursor(WalkCursor, CursorHotspot, CursorMode.Auto); break;
    }
  }
}
