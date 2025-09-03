using UnityEngine;

public class CameraController : MonoBehaviour

{

    private bool doMovement = true;
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;

    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape))
            doMovement = !doMovement;

        // Weird fix, all the movement keys are invese since the camera is rotated 180 degrees
        // This is a temporary fix, will be fixed later
        if (!doMovement)
            return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);

        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            Vector3 move = transform.forward * scroll * 1000 * scrollSpeed * Time.deltaTime;
            Vector3 newPos = transform.position + move;

            newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
            transform.position = newPos;
        }
    }
}
