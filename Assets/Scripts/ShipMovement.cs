using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool isPlayer1;
    
    private Rigidbody2D rb;
    Renderer[] renderers;
	
    bool isWrappingX = false;
    bool isWrappingY = false;
    void Start()
    {
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
 
        // if (!isWrappingX && (viewportPosition.x > 1 || viewportPosition.x < 0))
        // {
        //     newPosition.x = -newPosition.x;
        //
        //     isWrappingX = true;
        // }
 
        if (!isWrappingY && (viewportPosition.y > 1))
        {
            newPosition.y = -newPosition.y;
            
            isWrappingY = true;
            
            GameManager.instance.Score(isPlayer1);
        }
 
        transform.position = newPosition;
    }

    protected virtual void Update()
    {
        ScreenWrap();
        if (GameManager.instance.inMenu || GameManager.instance.inPauseMenu)
        {
            UpdateMotor(0);
            return; 
        }
        UpdateMotor(Input.GetAxisRaw(isPlayer1 ? "Vertical" : "Vertical2"));
    }

    protected virtual void UpdateMotor(float y)
    {
        rb.velocity = new Vector2(0, y * speed);
    }
}
