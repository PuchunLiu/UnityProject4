using UnityEngine;

public class BackGroundMap : MonoBehaviour
{
    public GameObject mainCamera;
    public float mapWidth;
    public int mapNum;
    private float totalWidth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mapWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        totalWidth = mapNum * mapWidth;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempPos = transform.position;
        if(mainCamera.transform.position.x > transform.position.x + totalWidth / 2)
        {
            tempPos.x += totalWidth;
            transform.position = tempPos;
        }
        else if(mainCamera.transform.position.x < transform.position.x - totalWidth / 2)
        {
            tempPos.x -= totalWidth;
            transform.position = tempPos;
        }
    }
}
