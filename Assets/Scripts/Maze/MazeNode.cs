using UnityEngine;

public enum NodeState
{
    Available,
    Current,
    Completed
}

public enum LockerPos
{
    PosX,
    NegX,
    PosY,
    NegY

}

public class MazeNode : MonoBehaviour
{
    public GameObject[] walls;
    [SerializeField] MeshRenderer floor;
    [SerializeField] GameObject lamp;
    [SerializeField] GameObject locker;

    public void RemoveWall(int wallToRemove)
    {
        walls[wallToRemove].SetActive(false);
        walls[wallToRemove].name = "N/A";
    }

    public void SetLamp(Vector3 pos)
    {
        Instantiate(lamp, pos, Quaternion.Euler(0, 0, 180));
    }

    public void SetLocker(Vector3 pos, Quaternion qr)
    {
        Instantiate(locker, pos, qr);
    }

    public GameObject[] wallGet()
    {
        return walls;
    }

    // public void SetState(NodeState state)
    // {
    //     switch (state)
    //     {
    //         case NodeState.Available:
    //             floor.material.color = Color.white;
    //             break;
    //         case NodeState.Current:
    //             floor.material.color = Color.yellow;
    //             break;
    //         case NodeState.Completed:
    //             floor.material = floorMat;
    //             break;
    //         default:
    //             break;
    //     }
    // }

     public void SpawnLocker(LockerPos LR)
     {
         switch (LR)
         {
             case LockerPos.PosX:
                Instantiate(locker, transform.position + new Vector3(0.35f, -0.45f, 0), Quaternion.Euler(0, -90, 0));
                break;
             case LockerPos.NegX:
                Instantiate(locker, transform.position + new Vector3(-0.35f, -0.45f, 0), Quaternion.Euler(0, 90, 0));
                break;
            case LockerPos.PosY:
                Instantiate(locker, transform.position + new Vector3(0, -0.45f, 0.35f), Quaternion.Euler(0, 180, 0));
                break;
            case LockerPos.NegY:
                Instantiate(locker, transform.position + new Vector3(0, -0.45f, -0.35f), Quaternion.Euler(0, 0, 0));
                
                break;

            default:
                 break;
         }
     }
}
