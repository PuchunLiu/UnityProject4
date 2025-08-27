using UnityEngine;

public class StartSceneMove : MonoBehaviour
{
    public float speed = 0.1f;
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
