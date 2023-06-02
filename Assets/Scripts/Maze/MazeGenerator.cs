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

    [SerializeField] MazeNode nodePrefab;
    [SerializeField] Vector2Int mazeSize;

    [SerializeField] GameObject radio;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject lamp;

    //public Transform[] objectsToRotate;
    //public NavMeshSurface[] surfaces;

    bool firstTime = true;
    int MaxRadio = 8;
    int CountRadio = 0;


    //[SerializeField] NavigationBaker baker;

    private void Start()
    {
        StartCoroutine(GeneratMaze(mazeSize));
    }

    IEnumerator GeneratMaze(Vector2Int size)
    {
        List<MazeNode> nodes = new List<MazeNode>();

        
        //create nodes
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                
                Vector3 nodePos = new Vector3(x - (size.x / 2f), 0, y - (size.y / 2f));// spawns node in center
                MazeNode newNode = Instantiate(nodePrefab, nodePos, Quaternion.identity, transform);
                //if (Random.Range(1, 5) == 3)
                //{
                //    Instantiate(lamp, new Vector3(nodePos.x, nodePos.y+0.45f, nodePos.z), Quaternion.Euler(0, 0, 180), transform);
                //}

                nodes.Add(newNode);
                
                yield return null;
            }
        }
        
        

        List<MazeNode> currentPathNode = new List<MazeNode>();
        
        List<MazeNode> completedNodes = new List<MazeNode>();
        

        //choose starting node
        currentPathNode.Add(nodes[Random.Range(0, nodes.Count)]);
        currentPathNode[0].SetState(NodeState.Current);
        Instantiate(Player, currentPathNode[0].transform.position, Quaternion.identity);

        while (completedNodes.Count < nodes.Count)
        {

           

            //Check nodes next to currrent node
            List<int> possibleNextNodes = new List<int>();
            
            List<int> possibleDirections = new List<int>();
            

            int currentNodeIndex = nodes.IndexOf(currentPathNode[currentPathNode.Count - 1]);
            


            int currentNodeX = currentNodeIndex / size.y;
           
            int currentNodeY = currentNodeIndex % size.y;
            

            if (currentNodeX < size.x - 1)
            {
                // Check Node to the ***RIGHT*** of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex + size.y]) && !currentPathNode.Contains(nodes[currentNodeIndex + size.y]))
                {
                    possibleDirections.Add(1);
                    possibleNextNodes.Add(currentNodeIndex + size.y);   
                    
                }
            }

            if(currentNodeX > 0)
            {
                // Check Node to the ***LEFT*** of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - size.y]) && !currentPathNode.Contains(nodes[currentNodeIndex - size.y]))
                {
                    possibleDirections.Add(2);
                    possibleNextNodes.Add(currentNodeIndex - size.y);
                }
            }
            if(currentNodeY < size.y -1)
            {
                // Check Node to the ***ABOVE*** of the current node
                if(!completedNodes.Contains(nodes[currentNodeIndex + 1]) && !currentPathNode.Contains(nodes[currentNodeIndex + 1 ]))
                {
                    possibleDirections.Add(3);
                    possibleNextNodes.Add(currentNodeIndex +1 );
                }
            }

            if (currentNodeY > 0)
            {
                // Check Node to the ***BELLOW*** of the current node
                if (!completedNodes.Contains(nodes[currentNodeIndex - 1]) && !currentPathNode.Contains(nodes[currentNodeIndex - 1]))
                {
                    possibleDirections.Add(4);
                    possibleNextNodes.Add(currentNodeIndex - 1);
                }
            }
            possibleNextNodesTEXT.text = possibleNextNodes.Count.ToString();
            possibleDirectionsTEXT.text = possibleDirections.Count.ToString();
            //Choose next node
            if (possibleDirections.Count>0)
            {
                int chosenDirection = Random.Range(0, possibleDirections.Count);
                MazeNode chosenNode = nodes[possibleNextNodes[chosenDirection]];


                switch (possibleDirections[chosenDirection])
                {
                    case 1:
                        chosenNode.RemoveWall(1);
                        currentPathNode[currentPathNode.Count - 1].RemoveWall(0);
                        break;
                    case 2:
                        chosenNode.RemoveWall(0);
                        currentPathNode[currentPathNode.Count - 1].RemoveWall(1);
                        break;
                    case 3:
                        chosenNode.RemoveWall(3);
                        currentPathNode[currentPathNode.Count - 1].RemoveWall(2);
                        break;
                    case 4:
                        chosenNode.RemoveWall(2);
                        currentPathNode[currentPathNode.Count - 1].RemoveWall(3);
                        break;
                }

                currentPathNode.Add(chosenNode);

                chosenNode.SetState(NodeState.Current);
                firstTime = true;
            }
            else
            {
                
                completedNodes.Add(currentPathNode[currentPathNode.Count - 1]);

                currentPathNode[currentPathNode.Count - 1].SetState(NodeState.Completed);


                if(firstTime)
                {
                    int randomNumber = Random.Range(1, 5);

                    if(randomNumber == 2)
                    {
                        Instantiate
                            (
                                radio, 
                                new Vector3
                                (currentPathNode[currentPathNode.Count - 1].transform.position.x-0.3f,
                                currentPathNode[currentPathNode.Count - 1].transform.position.y-0.45f,
                                currentPathNode[currentPathNode.Count - 1].transform.position.z+0.3f), 
                                Quaternion.Euler(-90, 0, -30)
                            );

                       
                        CountRadio += 1;
                    }
                    
                    
                    firstTime = false;
                }
                RadioCountTEXT.text = CountRadio.ToString();

                currentPathNode.RemoveAt(currentPathNode.Count - 1);
            }
           

            completedNodesTEXT.text = completedNodes.Count.ToString();
            currentPathNodeTEXT.text = currentNodeIndex.ToString();



            currentNodeXTEXT.text = currentNodeX.ToString();
            currentNodeYTEXT.text = currentNodeY.ToString();

            

            

            yield return null;
            



           
        }


    }
}
