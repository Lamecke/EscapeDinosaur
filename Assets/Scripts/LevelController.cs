using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    [SerializeField] List<Road> roads;
    [SerializeField] Road finalRoad;
    [SerializeField] Road currentRoad;
    // Start is called before the first frame update
    void Start()
    {
        finalRoad = roads[roads.Count - 1];
    }
    private void LateUpdate()
    {
        LevelCreate();
    }

    public void LevelCreate()
    {
        for (int i = 0; i < roads.Count; i++)
        {
            currentRoad = roads[i];
            if (!currentRoad.gameObject.activeSelf)
            {

               
                Vector3 tempPos = new Vector3(finalRoad.transform.position.x
                    , finalRoad.transform.position.y
                    , finalRoad.transform.position.z + 60f);
                currentRoad.gameObject.transform.position = tempPos;
                currentRoad.gameObject.SetActive(true);
                finalRoad = currentRoad;
            }
        }
    }
}
