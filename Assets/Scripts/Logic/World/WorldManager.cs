using UnityEngine;
using System.Collections;

public class WorldManager : MonoBehaviour {

    private string worldConverPrefabPath = "GroundCover";

    private float unitSize = 0.64f;
    private Vector2 currPoint = Vector2.zero;
    private int index = 0;
    private int maxCircle = 20;

    private GameObject originConverPrefab = null;

    private Transform worldConverParent = null;

    void Start()
    {
        Debug.Log("HAHA");
        InitWorldConverHolder();
        StartCoroutine(Generate());
    }


    void InitWorldConverHolder()
    {
        GameObject obj = new GameObject();
        obj.name = "WorldConvers";
        obj.transform.position = Vector3.zero;
        worldConverParent = obj.transform;
    }





    IEnumerator Generate()
    {
        originConverPrefab = Resources.Load(worldConverPrefabPath) as GameObject;
        
        for(int i = 1; i < maxCircle; ++i)
        {
            // left -> up
            float x = -(i * unitSize);
            for(int iY = 0; iY <= i; ++iY)
            {
                currPoint.x = x;
                currPoint.y = iY * unitSize;
                CreateUnit(currPoint);
                yield return null;
            }

            // top -> right
            float y = i * unitSize;
            for(int iX = -i + 1; iX <= i; ++iX)
            {
                currPoint.x = iX * unitSize;
                currPoint.y = y;
                CreateUnit(currPoint);
                yield return null;
            }

            // right -> down
            x = (i * unitSize);
            for(int iY = i - 1;iY >= -i; --iY)
            {
                currPoint.x = x;
                currPoint.y = iY * unitSize;
                CreateUnit(currPoint);
                yield return null;
            }

            // down -> left
            y = -(i * unitSize);
            for(int iX = i - 1; iX >= -i; --iX)
            {
                currPoint.x = iX * unitSize;
                currPoint.y = y;
                CreateUnit(currPoint);
                yield return null;
            }

            x = -(i * unitSize);
            for(int iY = -i + 1; iY < 0; ++iY)
            {
                currPoint.x = x;
                currPoint.y = iY * unitSize;
                CreateUnit(currPoint);
                yield return null;
            }

        }

    }



    void CreateUnit(Vector2 pos)
    {
        GameObject obj = Instantiate(originConverPrefab);
        obj.name = "GroundConver";
        obj.transform.SetParent(worldConverParent);
        obj.transform.position = pos;
    }

}
