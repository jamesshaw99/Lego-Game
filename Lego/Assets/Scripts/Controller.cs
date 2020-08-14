using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    protected Player Player;

    protected const float RotationSpeed = 10;

    public Vector3 CameraPivot;
    public float CameraDistance;
    protected float InputRotationX;
    protected float InputRotationY;

    protected Vector3 CharacterPivot;
    protected Vector3 lookDirection;
    protected Vector3 LastMousePos;

    private void Start()
    {
        Player = FindObjectOfType<Player>();
        LastMousePos = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        var mouseDelta = Input.mousePosition - LastMousePos;

        InputRotationX = InputRotationX + mouseDelta.x * RotationSpeed * Time.deltaTime % 360f;
        InputRotationY = Mathf.Clamp(InputRotationY - mouseDelta.y * RotationSpeed * Time.deltaTime, -88f, 88f);

        var characterForward = Quaternion.AngleAxis(InputRotationX, Vector3.up) * Vector3.forward;
        var characterLeft = Quaternion.AngleAxis(InputRotationX + 90, Vector3.up) * Vector3.forward;

        var runDirection = characterForward * Input.GetAxisRaw("Vertical") + characterLeft * Input.GetAxisRaw("Horizontal"));
        lookDirection = Quaternion.AngleAxis(InputRotationY, characterLeft) * characterForward;

        Player.Input.RunX = runDirection.x;
        Player.Input.RunZ = runDirection.z;
        Player.Input.LookX = lookDirection.x;
        Player.Input.LookZ = lookDirection.z;
        Player.Input.Jump = Input.GetKey(KeyCode.Space);

        CharacterPivot = Quaternion.AngleAxis(InputRotationX, Vector3.up) * CameraPivot;

        LastMousePos = Input.mousePosition;
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = (transform.position + CharacterPivot) - lookDirection * CameraDistance;
        Camera.main.transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);
    }
}
