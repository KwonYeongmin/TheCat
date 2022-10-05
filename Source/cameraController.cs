using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;

    private float speed=3f;
    private Vector3 playerPos;

    public Vector2 center;
    public Vector2 size;

    private float width;
    private float height;


    void Start()
    {
      height= Camera.main.orthographicSize;
       width = height * Screen.width / Screen.height;
       
    }

    private void OnDrawGizmos()
    {
       Gizmos.color = Color.red;
       Gizmos.DrawWireCube(center, size);
    }

    void LateUpdate()
    {
        playerPos = this.player.transform.position;

        transform.position = Vector3.Lerp(transform.position, playerPos, Time.deltaTime * speed);

        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
       

    }
   
}
