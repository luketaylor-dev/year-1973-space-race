using UnityEngine;
using Random = UnityEngine.Random;

public class Astroid : MonoBehaviour
{
    public float maxSpeed = 8f;
    public float minSpeed = 3f;
    
    private float speed;
    
    private Rigidbody2D rb;
    Renderer[] renderers;
	
    bool isWrappingX = false;
    bool isWrappingY = false;
    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
        rb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<Renderer>();
    }
 
    bool CheckRenderers()
    {
        foreach(var renderer in renderers)
        {
            // If at least one render is visible, return true
            if(renderer.isVisible)
            {
                return true;
            }
        }
 
        // Otherwise, the object is invisible
        return false;
    }
    
    void ScreenWrap()
    {
        var isVisible = CheckRenderers();
 
        if(isVisible)
        {
            isWrappingX = false;
            isWrappingY = false;
            return;
        }
 
        if(isWrappingX && isWrappingY) {
            return;
        }
 
        var cam = Camera.main;
        var viewportPosition = cam.WorldToViewportPoint(transform.position);
        var newPosition = transform.position;
 
        if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        {
            newPosition.x = -newPosition.x;
 
            isWrappingX = true;
        }
 
        if (!isWrappingY && (viewportPosition.y > 1 || viewportPosition.y < 0))
        {
            newPosition.y = -newPosition.y;
 
            isWrappingY = true;
        }
 
        transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.inPauseMenu)
        {
            rb.velocity = new Vector2(0, 0);
            return;
        }
        rb.velocity = new Vector2(1 * speed, 0);
        ScreenWrap();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            GameManager.instance.ResetShip(col.gameObject.GetComponent<ShipMovement>().isPlayer1);
        }
    }
}
