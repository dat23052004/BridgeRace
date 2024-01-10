using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Bot : Player
{
    public Transform endPoint;
    private Brick targetBrick;

    private NavMeshAgent navMeshAgent;

    public LevelManager levelManager;

    private List<Brick> FitBrick = new List<Brick>();  // List Brick thích hợp để Bot ăn

    private bool isMovingToTarget = false;

    public Player player;



    // Start is called before the first frame update
    private void Start()
    {

        ChangeColor(ColorType.Green);
        navMeshAgent = GetComponent<NavMeshAgent>();
        levelManager = FindObjectOfType<LevelManager>(); // Tìm LevelManager trong scene
        //StartCoroutine(MoveToNewTargetPointRoutine());
    }

    // Update is called once per frame
    private void Update()
    {
        FindAndEatBricks();
        StartCoroutine(MoveToNewTargetPointRoutine());
        //Debug.Log(transform.position);
    }

    private IEnumerator MoveToNewTargetPointRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f); // Chờ 20 giây
            Debug.Log(1);
            MoveToEndPoint(); // Di chuyển đến đích
        }
    }
    private void MoveToEndPoint()
    {
        // Kiểm tra 
        if (endPoint != null)
        {
            // Di chuyển đến đích
            navMeshAgent.SetDestination(endPoint.position);
            isMovingToTarget = true;
        }
        else
        {
            Debug.LogWarning("null");
        }
    }

    private void FindAndEatBricks()
    {
        // Nếu bot không di chuyển, thì mới tìm brick mới
        if (!isMovingToTarget)
        {
            //Debug.Log(1);
            // TÌm brick
            FindBrick();
        }

        // Nếu có brick để ăn và bot không di chuyển, hãy di chuyển đến nó
        if (FitBrick.Count > 0 && !isMovingToTarget)
        {
            //Debug.Log(2);
            MoveToTarget();
        }
    }

    private void FindBrick()
    {
        if(levelManager != null)
        {
            
            List<Brick> bricks = levelManager.brickList;
            
            FitBrick.Clear();   // Xóa để làm mới mỗi lần ăn

            for(int i = 0; i < bricks.Count; i++) {
                
                
                //float distance =Vector3.Distance(transform.position, brick.transform.position);
                if(bricks[i] != null)
                {
                    if (CanMove(bricks[i]) && bricks[i].colorType == colorType)
                    {

                        FitBrick.Add(bricks[i]);                      
                    }
                }                
            } 
            
        }
    }

    
    private bool CanMove(Brick brick)  // trả về true nếu bot canmove
    {
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(brick.transform.position, path);   // Tính toán đường đi từ bot đến brick, lưu tt vào path
        return path.status == NavMeshPathStatus.PathComplete;    // Kiểm tra đường tính toán, Nếu status là PathComplete, điều này có nghĩa là bot có thể đến được brick, và hàm trả về true.
    }

    private void MoveToTarget()
    {
        if (FitBrick.Count > 0  )
        {
            //Debug.LogError(FitBrick.Count);
            // Chọn một brick ngẫu nhiên từ danh sách 
            targetBrick = FitBrick[Random.Range(0, FitBrick.Count)];
                
            // Di chuyển đến brick đã chọn
            navMeshAgent.SetDestination(targetBrick.transform.position);
            //Debug.Log("Moving");
            isMovingToTarget = true;    
        }
    }

    public override void Moving()
    {
        
    }

    public void BotAddBrick()
    {
        PlayerBricks playerBrick = Instantiate(brickPrefabs, brickHolder);
        playerBrick.ChangeColor(colorType);
        playerBrick.transform.localPosition = playerBricks.Count * 0.5f * Vector3.up;
        playerBricks.Add(playerBrick);

    }

    public void BotRemoveBrick()
    {
        if(playerBricks.Count > 0)
        {
           PlayerBricks playerBrick = playerBricks[playerBricks.Count - 1];
            playerBricks.RemoveAt(playerBricks.Count - 1);          
            Destroy(playerBrick.gameObject);            
            Debug.Log(1);
        }
    
    }
    
    public void UnRemove()
    {
        if (playerBricks.Count > 0)
        {
            PlayerBricks playerBrick = playerBricks[playerBricks.Count - 1];

            // Activate the playerBrick
            playerBrick.gameObject.SetActive(true);
            Debug.Log(1234);
        }
    }

    public void BotClearBrick()
    {
        for(int i = 0;i < playerBricks.Count;i++)
        {
            Destroy(playerBricks[i]);

        }
        playerBricks.Clear();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            Brick brick = other.GetComponent<Brick>();
            if (brick.colorType == colorType)
            {
                Destroy(brick.gameObject);
                
                AddBrick();
                isMovingToTarget = false;
            }
        }
        if (other.CompareTag("Stair"))
        {
            Stair stair = other.GetComponent<Stair>();
            if(stair.colorType == colorType && playerBricks.Count > 0)
            {
                stair.ChangeColor(colorType);
                RemoveBrick();
            }
        }
    }
}

    //void CheckIfOnNavMesh()        // Check Bot và Brick có nằm trên NavMesh hk ->> Có 
    //{
    //    Vector3 botPosition = transform.position;
    //    NavMeshHit hit;

    //    // Kiểm tra xem vị trí của bot có nằm trên một NavMesh không
    //    if (NavMesh.SamplePosition(botPosition, out hit, 1.0f, NavMesh.AllAreas))
    //    {
    //        Debug.Log("Bot is on NavMesh.");
    //    }
    //    else
    //    {
    //        Debug.LogError("Bot is not on NavMesh.");
    //    }
    //}