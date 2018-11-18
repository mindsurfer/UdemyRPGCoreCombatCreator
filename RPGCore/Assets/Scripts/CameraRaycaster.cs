﻿using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
  public Layer[] layerPriorities = {
        Layer.Enemy,
        Layer.Walkable
    };

  [SerializeField] float DistanceToBackground = 100f;
  Camera viewCamera;

  RaycastHit m_hit;
  public RaycastHit Hit
  {
    get { return m_hit; }
  }

  Layer m_layerHit;
  public Layer LayerHit
  {
    get { return m_layerHit; }
  }

  void Start() // TODO Awake?
  {
    viewCamera = Camera.main;
  }

  void Update()
  {
    // Look for and return priority layer hit
    foreach (Layer layer in layerPriorities)
    {
      var hit = RaycastForLayer(layer);
      if (hit.HasValue)
      {
        m_hit = hit.Value;
        m_layerHit = layer;
        return;
      }
    }

    // Otherwise return background hit
    m_hit.distance = DistanceToBackground;
    m_layerHit = Layer.RaycastEndStop;
  }

  RaycastHit? RaycastForLayer(Layer layer)
  {
    int layerMask = 1 << (int)layer; // See Unity docs for mask formation
    Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);

    RaycastHit hit; // used as an out parameter
    bool hasHit = Physics.Raycast(ray, out hit, DistanceToBackground, layerMask);
    if (hasHit)
    {
      return hit;
    }
    return null;
  }
}
