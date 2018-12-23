using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{
  [SerializeField] float WalkStopRadius = 0.2f;
  [SerializeField] float AttackStopRadius = 5f;

  private ThirdPersonCharacter _character;   // A reference to the ThirdPersonCharacter on the object
  private CameraRaycaster _cameraRaycaster;
  private Vector3 _currentDestination, _clickPoint;

  private bool _isAxesMode = false;

  private void Start()
  {
    _cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
    _character = GetComponent<ThirdPersonCharacter>();
    _currentDestination = transform.position;
  }

  // Fixed update is called in sync with physics
  private void FixedUpdate()
  {
    if (Input.GetKeyDown(KeyCode.G))
    {
      _isAxesMode = !_isAxesMode;
      _currentDestination = transform.position;
    }

    if (_isAxesMode)
      ProcessAxesMovement();
    else
      ProcessMouseMovement();
  }

  private void ProcessAxesMovement()
  {
    // read inputs
    float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");

    // calculate camera relative direction to move:
    var camTransform = Camera.main.transform;
    var camForward = Vector3.Scale(camTransform.forward, new Vector3(1, 0, 1)).normalized;
    var move = v * camForward + h * camTransform.right;

    _character.Move(move, false, false);
  }

  private void ProcessMouseMovement()
  {
    if (Input.GetMouseButton(0))
    {
      print("Cursor raycast hit" + _cameraRaycaster.Hit.collider.gameObject.name.ToString());
      _clickPoint = _cameraRaycaster.Hit.point;
      switch (_cameraRaycaster.LayerHit)
      {
        case Layer.Walkable:
          _currentDestination = ShortDestination(_clickPoint, WalkStopRadius);  // So not set in default case
          break;
        case Layer.Enemy:
          _currentDestination = ShortDestination(_clickPoint, AttackStopRadius);  // So not set in default case
          break;
        case Layer.RaycastEndStop:
          break;
      }
    }

    WalkToDestination();
  }

  private void WalkToDestination()
  {
    var playerToClickTarget = _currentDestination - transform.position;
    if (playerToClickTarget.magnitude >= 0)
      _character.Move(playerToClickTarget, false, false);
    else
      _character.Move(Vector3.zero, false, false);
  }

  private Vector3 ShortDestination(Vector3 destination, float shortening)
  {
    var reductionVector = (destination - transform.position).normalized * shortening;
    return destination - reductionVector;
  }

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.black;
    Gizmos.DrawLine(transform.position, _currentDestination);
    Gizmos.DrawSphere(_currentDestination, 0.1f);
    Gizmos.DrawSphere(_clickPoint, 0.15f);

    Gizmos.color = new Color(255f, 0f, 0f, 0.5f);
    Gizmos.DrawWireSphere(transform.position, AttackStopRadius);
  }
}

