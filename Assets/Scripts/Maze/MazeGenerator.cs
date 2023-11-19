using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class MazeGenerator : MonoBehaviour
{
    public Text currentPathNodeTEXT;
    public Text completedNodesTEXT;
    public Text possibleNextNodesTEXT;
    public Text possibleDirectionsTEXT;
    public Text currentNodeXTEXT;
    public Text currentNodeYTEXT;
    public Text RadioCountTEXT;
    public Text LockerCountTEXT;

    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector2Int mazeSize;

    [SerializeField] GameObject radio;
    [SerializeField] GameObject player;
    [SerializeField] GameObject lamp;

    [SerializeField] GameObject floor;
    [SerializeField] GameObject monster;
    [SerializeField] GameObject lockerPrefab;

    public bool done = false;

    bool firstTime = true;
    int maxRadio = 5;
    public int countRadio = 0;

    int maxLockers = 8;
    int lockers;

    bool firstTimeM = true;

    private void Start()
    {
        floor.transform.localScale = new Vector3(mazeSize.x, 0.1f, mazeSize.y);
        floor.transform.position = new Vector3(-0.5f, -0.5f, -0.5f);
        floor.GetComponent<NavMeshSurface>().BuildNavMesh();

        StartCoroutine(GenerateMaze(mazeSize));
    }

    IEnumerator GenerateMaze(Vector2Int size)
    {
        
        List<MazeNode> nodes = new List<MazeNode>();
        
        for (int x = 0; x < size.x; x++)
        {
           
            for (int y = 0; y < size.y; y++)
            {
               
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f));
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);

                //newNode.SetLocker(nodePos + new Vector3(0, -0.45f, 0), Quaternion.Euler(0, 0, 0));
                if(y % 2 == 0 && x % 2 == 0)
                {
                    newNode.SetLamp(nodePos + new Vector3(0, 0.45f, 0));
                }
                

                nodes.Add(newNode);

                yield return null;
            }
            
        }

        


            List<MazeNode> currentPathNode = new List<MazeNode>();
        List<MazeNode> completedNodes = new List<MazeNode>();

        currentPathNode.Add(nodes[Random.Range(0, nodes.Count)]);
        Instantiate(player, currentPathNode[0].transform.position, Quaternion.identity);

        while (completedNodes.Count < nodes.Count)
        {
            List<int> possibleNextNodes = new List<int>();
            List<int> possibleDirections = new List<int>();

            int currentNodeIndex = nodes.IndexOf(currentPathNode[currentPathNode.Count - 1]);
            int currentNodeX = currentNodeIndex / size.y;
            int currentNodeY = currentNodeIndex % size.y;

            // Check Node to the ***RIGHT*** of the current node
            if (currentNodeX < size.x - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPathNode.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);
                    //nodes[currentNodeIndex + size.y].SpawnLocker(LockerPos.PosX);
                }
            }
            // Check Node to the ***LEFT*** of the current node
            if (currentNodeX > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPathNode.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                    //nodes[currentNodeIndex - size.y].SpawnLocker(LockerPos.NegX);
                }
            }
            // Check Node to the ***UP*** of the current node
            if (currentNodeY < size.y - 1)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPathNode.Contains(nodes[currentNodeIndex + 1]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex + 1);
                    //nodes[currentNodeIndex + 1].SpawnLocker(LockerPos.PosY);
                }
            }
            // Check Node to the ***DOWN*** of the current node
            if (currentNodeY > 0)
            {
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPathNode.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                    //nodes[currentNodeIndex - 1].SpawnLocker(LockerPos.NegY);
                }
            }

            possibleNextNodesTEXT.text = possibleNextNodes.Count.ToString();
            possibleDirectionsTEXT.text = possibleDirections.Count.ToString();

            if (possibleDirections.Count > 0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];

                switch (possibleDirections[chosenDirection])
                {
                    case 1: //right
                        chosenNode.RemoveWall(1);
                        currentPathNode[currentPathNode.Count - 1].RemoveWall(0);
                        
                        
                        break;
                    case 2: // Left
                        chosenNode.RemoveWall(0);
                        currentPathNode[currentPathNode.Count - 1].RemoveWall(1);
                        
                        break;
                        

                    case 3: // Above
                        chosenNode.RemoveWall(3);
                        currentPathNode[currentPathNode.Count - 1].RemoveWall(2);
                        
                        break;

                    case 4: // Below
                        chosenNode.RemoveWall(2);
                        currentPathNode[currentPathNode.Count - 1].RemoveWall(3);
                        
                        break;
                }

                currentPathNode.Add(chosenNode);
                
                firstTime = true;

                //locker
                /*int randomNumberLocker = Random.Range(1, 5);
                if (randomNumberLocker == 2 && lockers < maxLockers)
                {
                    Quaternion lockerRotation = Quaternion.LookRotation(Vector3.left);
                    Vector3 lockerPos = new Vector3(0, -0.45f, 0);
                    Instantiate(lockerPrefab, lockerPos, lockerRotation);
                    lockers++;
                }*/
                //locker@
                

            }
            else
            {
                completedNodes.Add(currentPathNode[currentPathNode.Count - 1]);
                
                if (firstTime && countRadio < maxRadio)
                {
                    if (Random.Range(1, 5) == 1)
                    {
                        Instantiate(radio, new Vector3(currentPathNode[currentPathNode.Count - 1].transform.position.x - 0.3f, currentPathNode[currentPathNode.Count - 1].transform.position.y - 0.45f, currentPathNode[currentPathNode.Count - 1].transform.position.z + 0.3f), Quaternion.Euler(-90, 0, 0));
                        countRadio++;
                    }

                    firstTime = false;
                }

                
                if (firstTimeM)
                {
                    Instantiate(monster, new Vector3(currentPathNode[currentPathNode.Count - 1].transform.position.x - 0.3f, currentPathNode[currentPathNode.Count - 1].transform.position.y - 0.45f, currentPathNode[currentPathNode.Count - 1].transform.position.z + 0.3f), Quaternion.identity);
                    firstTimeM = false;
                }

                RadioCountTEXT.text = countRadio.ToString();


                int randomNumberLocker = Random.Range(1, 10);
                //if(randomNumberLocker == 5)
                //{
                    for (int i = 0; i < currentPathNode[currentPathNode.Count - 1].wallGet().Length; i++)
                    {
                        if (currentPathNode[currentPathNode.Count - 1].wallGet()[i].name == "PosXWall" && randomNumberLocker == 9)
                        {
                            currentPathNode[currentPathNode.Count - 1].SpawnLocker(LockerPos.PosX);
                        }

                        if (currentPathNode[currentPathNode.Count - 1].wallGet()[i].name == "NegXWall" && randomNumberLocker == 3)
                        {
                            currentPathNode[currentPathNode.Count - 1].SpawnLocker(LockerPos.NegX);
                        }

                        if (currentPathNode[currentPathNode.Count - 1].wallGet()[i].name == "PosZWall" && randomNumberLocker == 7)
                        {
                            currentPathNode[currentPathNode.Count - 1].SpawnLocker(LockerPos.PosY);
                        }

                        if (currentPathNode[currentPathNode.Count - 1].wallGet()[i].name == "NegZWall" && randomNumberLocker == 1)
                        {
                            currentPathNode[currentPathNode.Count - 1].SpawnLocker(LockerPos.NegY);
                        }
                    }
                //}
                


                currentPathNode.RemoveAt(currentPathNode.Count - 1);

            }

           


            completedNodesTEXT.text = completedNodes.Count.ToString();
            currentPathNodeTEXT.text = currentNodeIndex.ToString();
            currentNodeXTEXT.text = currentNodeX.ToString();
            currentNodeYTEXT.text = currentNodeY.ToString();
            LockerCountTEXT.text = lockers.ToString();

            
            yield return null;
        }

        done = true;
    }
}