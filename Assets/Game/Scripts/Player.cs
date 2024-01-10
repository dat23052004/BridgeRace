using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : ColorObject
{
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask StairLayer;
    [SerializeField] protected Transform skin;
    protected List<PlayerBricks> playerBricks = new List<PlayerBricks>();
    [SerializeField] protected PlayerBricks brickPrefabs;
    public Transform brickHolder;

    
    
    public Stage stage;

    [SerializeField] protected Rigidbody rb;
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] protected float moveSpeed;
    private bool canPlayerMove = true;

    private void Update()
    {
        Moving();

    }

    public virtual void Moving()
    {
        if (joystick == null)
            Debug.Log(gameObject.name);
        Vector3 movement = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        Vector3 velocity = movement * moveSpeed;

        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);

        if (movement.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);


        }
    }
    private void Start()
    {
        //SetRandomColor();
        ChangeColor(ColorType.Blue);
    }

    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1f;
        }

        return transform.position;
    }

    public bool CanPlayerMove(Vector3 nextPoint)
    {
        bool isCanMove = true;
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, StairLayer))
        {
            Debug.Log("raycast1");
            Stair stair = hit.collider.GetComponent<Stair>();
            if (stair.colorType != colorType && playerBricks.Count > 0)
            {
                Debug.Log("raycast2");
                stair.ChangeColor(colorType);
                RemoveBrick();
            }

            if (stair.colorType != colorType && playerBricks.Count <= 0 && skin.forward.z > 0)
            {
                Debug.Log("raycast3");
                isCanMove = false;

                canPlayerMove = true;
            }
        }
        return isCanMove;
    }


    public void AddBrick()
    {
        
        PlayerBricks playerBrick = Instantiate(brickPrefabs, brickHolder);
        playerBrick.ChangeColor(colorType);
        
        playerBrick.transform.localPosition = playerBricks.Count * 0.7f * Vector3.up;
        playerBricks.Add(playerBrick);
    }

    public void RemoveBrick()
    {
        if(playerBricks.Count > 0)
        {
            PlayerBricks playerBrick = playerBricks[playerBricks.Count - 1];
            playerBricks.RemoveAt(playerBricks.Count - 1);
            Destroy(playerBrick.gameObject);
            playerBrick.gameObject.SetActive(false);
            
        }
    }

    public void ClearBrick()
    {
        for(int i = 0;i < playerBricks.Count; i++)
        {
            Destroy(playerBricks[i]);
        }
        playerBricks.Clear();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick brick = other.GetComponent<Brick>();
            if(brick.colorType == colorType )
            {
                Destroy(brick.gameObject);
                AddBrick();
            }
        }
        if (other.CompareTag("Stair"))
        {
            Debug.Log(2);
            Stair stair = other.GetComponent<Stair>();
            if (stair.colorType != colorType && playerBricks.Count > 0)
            {
                Debug.Log("Stair");
                
                stair.ChangeColor(colorType);
                RemoveBrick();
            }
            else if(stair.colorType != colorType && playerBricks.Count <= 0)
            {
                canPlayerMove = false;

            }
        }
    }
    
}
