using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGrid : MonoBehaviour
{

    public MovePoint startPoint;

    public Vector2Int spawnRange;

    public LayerMask whatIsGround, whatIsObstical;

    public float obsticalCheckRange;

    public List<MovePoint> allMovePoints = new List<MovePoint>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateMoveGrid();
        //HideMovePoints();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GenerateMoveGrid()
    {
        for (int x = -spawnRange.x; x <= spawnRange.x; x++)
        {
            for (int y = -spawnRange.y; y <= spawnRange.y; y++)
            {
                RaycastHit hit;

                //check if ground in under grid point
                if(Physics.Raycast(transform.position + new Vector3(x, 10f, y), Vector3.down, out hit, 20f, whatIsGround))
                {               
                    //check if obstical is in grid point
                    if(Physics.OverlapSphere(hit.point, obsticalCheckRange, whatIsObstical).Length == 0)
                    {
                        MovePoint newPoint = Instantiate(startPoint, hit.point, transform.rotation);
                        newPoint.transform.SetParent(transform);

                        allMovePoints.Add(newPoint);
                    }
                }

            }
        }
        startPoint.gameObject.SetActive(false);
    }

    public void HideMovePoints()
    {
        foreach (MovePoint movePoint in allMovePoints)
        {
            movePoint.gameObject.SetActive(false);
        }
    }
}
