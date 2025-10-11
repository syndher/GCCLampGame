using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float fadeSpeed = 5f; // Light fade speed
    [SerializeField] private Light2D IdleLamp; // Lights when stopped
    [SerializeField] private Light2D MovingLamp; // Lights when moving
    [SerializeField] private float IdleDelay = 0.4f; // Time it takes to turn on IdleLamp
    [SerializeField] private float MovingDelay = 0.3f; // Time it takes to turn off MovingLamp

    [SerializeField] private float zoomedOutSize = 100f; // camera size for full room
    [SerializeField] private float zoomSpeed = 20f; // speed of zoom

    private bool zoomOverride = false;
    private float targetOrthographicSize;

    private float IdleTimer = 0f;

    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        // Camera follow
        transform.position = player.position + new Vector3(0, 0, -5);

        // Handle zoom
        if (zoomOverride)
        {
            Camera.main.orthographicSize = Mathf.MoveTowards(
                Camera.main.orthographicSize,
                targetOrthographicSize,
                zoomSpeed * Time.deltaTime
            );
        }

        
        if (!zoomOverride && Lights.activatedLights >= 9) 
        {
            ZoomOut();
        }

        
        UpdateLighting();
    }

    void UpdateLighting()
    {
        bool _moving = playerController.moving;

        if (_moving)
            IdleTimer = 0f;
        else
            IdleTimer += Time.deltaTime;

        float targetIdle = (IdleTimer >= IdleDelay) ? 1f : 0f;
        float targetMoving = (IdleTimer >= MovingDelay) ? 0f : 1f;

        IdleLamp.intensity = Mathf.MoveTowards(IdleLamp.intensity, targetIdle, fadeSpeed * Time.deltaTime);
        MovingLamp.intensity = Mathf.MoveTowards(MovingLamp.intensity, targetMoving, fadeSpeed * Time.deltaTime);
    }

    private void ZoomOut()
    {
        zoomOverride = true;
        targetOrthographicSize = zoomedOutSize;
    }
}
