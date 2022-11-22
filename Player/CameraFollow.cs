
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public static CameraFollow cam;
    private Camera cam_;
    
    public float sensitivity = 3;
    [HideInInspector]
    public float mouseX, mouseY;
    public float clampSpineUp = 80;
    public float clampSpineDown = -80;
    public Transform player;
    public Transform Camera;

    public float clampUp = 0.0f;
    public float clampDown = 0.0f;


    float xRotation = 0f;

    bool _isInversed;

    public bool isDebug;

  Health health;

    private void OnBeforeTransformParentChanged()
    {
        
    }

    private void Awake()
    {
        cam = this;
        cam_ = this.GetComponent<Camera>();
      //  health = GetComponentInParent<Health>();


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }
    
    private float rotX = 0.0f, rotY = 0.0f;
    [HideInInspector]
    public float rotZ = 0.0f;
    private void Update()
    {

        if (isDebug) { return; }

    //    if (health.CurrentHealth <= 0) { return; }
        
        // Mouse input
        mouseX = Input.GetAxis("Mouse X") * sensitivity;
        mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Calculations
        rotX -= mouseY;
        rotX = Mathf.Clamp(rotX, clampUp, clampDown);
        rotY += mouseX;

        // Placing values
        transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);
        player.Rotate(Vector3.up * mouseX);
        transform.position = Camera.position;


        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, clampSpineUp, clampSpineDown);

        int sign = _isInversed?-1:1;
    
        Camera.localRotation = Quaternion.Euler(sign * xRotation, 0f, 0f);
   
    }

    public void Shake(float magnitude, float duration)
    {
        StartCoroutine(IShake(magnitude, duration));
    }

    private IEnumerator IShake(float mag, float dur)
    {
        WaitForEndOfFrame wfeof = new WaitForEndOfFrame();
        for(float t = 0.0f; t <= dur; t += Time.deltaTime)
        {
            rotZ = Random.Range(-mag, mag) * (t / dur - 1.0f);
            yield return wfeof;
        }
        rotZ = 0.0f;
    }
}
