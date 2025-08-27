using UnityEngine;

public class CameraCinemachine : MonoBehaviour
{
    public Transform backGround;
    private Vector3 lastPos;
    public Vector2 offsetSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastPos =  transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 amountMove = transform.position - lastPos;
        Vector3 offset = new Vector3(amountMove.x * offsetSpeed.x, amountMove.y * offsetSpeed.y, 0);
        backGround.position += offset;
        lastPos = transform.position;
    }
}
