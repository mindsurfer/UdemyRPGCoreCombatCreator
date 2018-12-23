using UnityEngine;

public class CameraRaycaster : MonoBehaviour
{
  public Layer[] layerPriorities = 
  {
    Layer.Enemy,
    Layer.Walkable
  };

  [SerializeField] float DistanceToBackground = 100f;
  Camera viewCamera;

  RaycastHit _raycastHit;
  public RaycastHit Hit
  {
    get { return _raycastHit; }
  }

  Layer _layerHit;
  public Layer LayerHit
  {
    get { return _layerHit; }
  }

  public delegate void LayerChangedHandler(Layer layer);
  public event LayerChangedHandler OnLayerChanged;

  void Start()
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
        var layerHasChanged = layer != _layerHit;
        _raycastHit = hit.Value;
        _layerHit = layer;

        if (layerHasChanged && OnLayerChanged != null)
          OnLayerChanged(_layerHit);

        return;
      }
    }

    // Otherwise return background hit
    _raycastHit.distance = DistanceToBackground;
    _layerHit = Layer.RaycastEndStop;
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
