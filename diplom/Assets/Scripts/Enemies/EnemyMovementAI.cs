using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
[DisallowMultipleComponent]

public class EnemyMovementAI : MonoBehaviour
{
    #region Tooltip
    [Tooltip("MovementDetailsSO scriptable object containing movement details such as speed")]
    #endregion Tooltip
    [SerializeField] private MovementDetailsSO movementDetails;
    private Enemy enemy;
    private Stack<Vector3> movementSteps = new Stack<Vector3>();
    private Vector3 playerReferencePosition;
    private Coroutine moveEnemyRoutine;
    private float currentEnemyPathRebuildCooldown;
    private WaitForFixedUpdate waitForFixedUpdate;
    [HideInInspector] public float moveSpeed;
    private bool chasePlayer = false;
    [HideInInspector] public int updateFrameNumber = 1; 
    private List<Vector2Int> surroundingPositionList = new List<Vector2Int>();

    private void Awake()
    {
        enemy = GetComponent<Enemy>();

        moveSpeed = movementDetails.GetMoveSpeed();
    }

    private void Start()
    {
        
        waitForFixedUpdate = new WaitForFixedUpdate();

        
        playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();

    }

    private void Update()
    {
        MoveEnemy();
    }

    private void MoveEnemy()
    {
        
        currentEnemyPathRebuildCooldown -= Time.deltaTime;

        
        if (!chasePlayer && Vector3.Distance(transform.position, GameManager.Instance.GetPlayer().GetPlayerPosition()) < enemy.enemyDetails.chaseDistance)
        {
            chasePlayer = true;
        }

        
        if (!chasePlayer)
            return;

        
        if (Time.frameCount % Settings.targetFrameRateToSpreadPathfindingOver != updateFrameNumber) return;
        
        if (currentEnemyPathRebuildCooldown <= 0f || (Vector3.Distance(playerReferencePosition, GameManager.Instance.GetPlayer().GetPlayerPosition()) > Settings.playerMoveDistanceToRebuildPath))
        {
            currentEnemyPathRebuildCooldown = Settings.enemyPathRebuildCooldown;
            playerReferencePosition = GameManager.Instance.GetPlayer().GetPlayerPosition();
            CreatePath();

            if (movementSteps != null)
            {
                if (moveEnemyRoutine != null)
                {
                    
                    enemy.idleEvent.CallIdleEvent();
                    StopCoroutine(moveEnemyRoutine);
                }
                
                moveEnemyRoutine = StartCoroutine(MoveEnemyRoutine(movementSteps));

            }
        }
    }

    private IEnumerator MoveEnemyRoutine(Stack<Vector3> movementSteps)
    {
        while (movementSteps.Count > 0)
        {
            Vector3 nextPosition = movementSteps.Pop();
            
            while (Vector3.Distance(nextPosition, transform.position) > 0.2f)
            {
                enemy.movementToPositionEvent.CallMovementToPositionEvent(nextPosition, transform.position, moveSpeed, (nextPosition - transform.position).normalized);
                yield return waitForFixedUpdate;  

            }

            yield return waitForFixedUpdate;
        }

        enemy.idleEvent.CallIdleEvent();
    }

    private void CreatePath()
    {
        Room currentRoom = GameManager.Instance.GetCurrentRoom();
        Grid grid = currentRoom.instantiatedRoom.grid;
        Vector3Int playerGridPosition = GetNearestNonObstaclePlayerPosition(currentRoom);
        Vector3Int enemyGridPosition = grid.WorldToCell(transform.position);
        movementSteps = AStar.BuildPath(currentRoom, enemyGridPosition, playerGridPosition);

        if (movementSteps != null)
        {
            movementSteps.Pop();
        }
        else
        {
            enemy.idleEvent.CallIdleEvent();
        }
    }

    public void SetUpdateFrameNumber(int updateFrameNumber)
    {
        this.updateFrameNumber = updateFrameNumber;
    }

    private Vector3Int GetNearestNonObstaclePlayerPosition(Room currentRoom)
    {
        Vector3 playerPosition = GameManager.Instance.GetPlayer().GetPlayerPosition();
        Vector3Int playerCellPosition = currentRoom.instantiatedRoom.grid.WorldToCell(playerPosition);
        Vector2Int adjustedPlayerCellPositon = new Vector2Int(playerCellPosition.x - currentRoom.templateLowerBounds.x, playerCellPosition.y - currentRoom.templateLowerBounds.y);
        int obstacle = Mathf.Min(currentRoom.instantiatedRoom.aStarMovementPenalty[adjustedPlayerCellPositon.x, adjustedPlayerCellPositon.y], currentRoom.instantiatedRoom.aStarItemObstacles[adjustedPlayerCellPositon.x, adjustedPlayerCellPositon.y]);

        if (obstacle != 0)
        {
            return playerCellPosition;
        }
        
        else
        {
            surroundingPositionList.Clear();

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (j == 0 && i == 0) continue;
                    surroundingPositionList.Add(new Vector2Int(i, j));
                }
            }

            for (int l = 0; l < 8; l++)
            {
                int index = Random.Range(0, surroundingPositionList.Count);
                
                try
                {
                    obstacle = Mathf.Min(currentRoom.instantiatedRoom.aStarMovementPenalty[adjustedPlayerCellPositon.x + surroundingPositionList[index].x, adjustedPlayerCellPositon.y + surroundingPositionList[index].y], currentRoom.instantiatedRoom.aStarItemObstacles[adjustedPlayerCellPositon.x + surroundingPositionList[index].x, adjustedPlayerCellPositon.y + surroundingPositionList[index].y]);

                    if (obstacle != 0)
                    {
                        return new Vector3Int(playerCellPosition.x + surroundingPositionList[index].x, playerCellPosition.y + surroundingPositionList[index].y, 0);
                    }

                }

                catch
                {

                }

                surroundingPositionList.RemoveAt(index);
            }

            return (Vector3Int)currentRoom.spawnPositionArray[Random.Range(0, currentRoom.spawnPositionArray.Length)];

        }
    }

    #region Validation

#if UNITY_EDITOR

    private void OnValidate()
    {
        HelperUtilities.ValidateCheckNullValue(this, nameof(movementDetails), movementDetails);
    }

#endif

    #endregion Validation
}