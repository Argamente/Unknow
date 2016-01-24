using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Argamente.Fight.Data;


public class WorldUnitVo
{
    public int level = 0;
    public int unit_count_width = 0;
    public int unit_count_height = 0;
}


public class WorldManager : MonoBehaviour
{

    private static WorldManager _instance = null;

    public static WorldManager GetInstance ()
    {
        if (_instance == null)
        {
            _instance = Argamente.Managers.SingletonManager.AddComponent<WorldManager> ();
        }
        return _instance;
    }


    public void Init (Transform explorer)
    {
        this.explorer = explorer;
        InitWorldUnitConfig ();
    }


    private int minLevel = 0;
    private int maxLevel = 3;


    Dictionary<int,WorldUnitVo> worldUnitConfig = new Dictionary<int,WorldUnitVo> ();

    void InitWorldUnitConfig ()
    {
        // level 0
        WorldUnitVo level0Vo = new WorldUnitVo ();
        level0Vo.level = 0;
        level0Vo.unit_count_width = 1;
        level0Vo.unit_count_height = 1;


        // level 1
        WorldUnitVo level1Vo = new WorldUnitVo ();
        level1Vo.level = 1;
        level1Vo.unit_count_width = 3;
        level1Vo.unit_count_height = 3;

        // level 2
        WorldUnitVo level2Vo = new WorldUnitVo ();
        level2Vo.level = 2;
        level2Vo.unit_count_width = 15;
        level2Vo.unit_count_height = 15;

        // level3
        WorldUnitVo level3Vo = new WorldUnitVo ();
        level3Vo.level = 3;
        level3Vo.unit_count_width = 30;
        level3Vo.unit_count_height = 30;

        worldUnitConfig.Add (level0Vo.level, level0Vo);
        worldUnitConfig.Add (level1Vo.level, level1Vo);
        worldUnitConfig.Add (level2Vo.level, level2Vo);
        worldUnitConfig.Add (level3Vo.level, level3Vo);
    }


    /// <summary>
    /// 根据当前区块ID，创建一个子区块结构
    /// </summary>
    /// <returns>The child world unit by parent level.</returns>
    /// <param name="level">Level.</param>
    public WorldUnitVo GetChildWorldUnitVoByParentLevel (int level)
    {
        if (level <= minLevel)
        {
            return null;
        }

        if (level > maxLevel)
        {
            return null;
        }

        WorldUnitVo currVo;
        worldUnitConfig.TryGetValue (level - 1, out currVo);

        return currVo;
    }


    public List<WorldUnit> topLevelWorldUnits = new List<WorldUnit> ();
    public Transform explorer = null;
    private int exploreIndex = -1;


    public void StartWorld ()
    {
        CreateTopLevel (0, 0);
    }


    public void CreateTopLevel (int topLevelIndexPosX, int topLevelIndexPosY)
    {
        WorldUnit unit = new WorldUnit ();
        unit.unitLevel = 3;
        unit.unit_width_count = 30;
        unit.unit_height_count = 30;
        unit.realPos_x = topLevelIndexPosX * unit.unit_width;
        unit.realPos_y = topLevelIndexPosY * unit.unit_height;
        unit.topLevelIndexPosX = topLevelIndexPosX;
        unit.topLevelIndexPosY = topLevelIndexPosY;

        unit.Create (); 

        topLevelWorldUnits.Add (unit); 
    }


    void FixedUpdate ()
    {
        if (explorer == null)
        {
            return;
        }

        if (topLevelWorldUnits.Count <= 0)
        {
            return;
        }
        /*
        ++exploreIndex;
        if (exploreIndex >= topLevelWorldUnits.Count)
        {
            exploreIndex = 0;
        }
*/


        for (exploreIndex = 0; exploreIndex < topLevelWorldUnits.Count; ++exploreIndex)
        {

            WorldUnit currTopUnit = topLevelWorldUnits [exploreIndex];
            // 如果当前顶层区块超出了检查区域
            if (currTopUnit.IsOutUnitCheckRange (this.explorer))
            {
                currTopUnit.ExitExploreRange ();
            }
            else
            {
                currTopUnit.EnterExploreRange ();
            }

            // 进入了当前顶层区块
            if (currTopUnit.IsEnterWorldUnit (this.explorer))
            {
                currTopUnit.Explore (this.explorer);
                CheckAndCreateNeighborTopLevelWorldUnit (currTopUnit);
            }

        }
    }


    // 当进入某一个顶层区块时，就要相应地创建它的相邻顶层区块，
    private void CheckAndCreateNeighborTopLevelWorldUnit (WorldUnit currTopUnit)
    {
        // top Neighbor
        int neighborIndexPosX = currTopUnit.topLevelIndexPosX;
        int neighborIndexPosY = currTopUnit.topLevelIndexPosY + 1;

        if (!IsTopWorldUnitExist (neighborIndexPosX, neighborIndexPosY))
        {
            CreateTopLevel (neighborIndexPosX, neighborIndexPosY);
        }

        // down Neighbor
        neighborIndexPosX = currTopUnit.topLevelIndexPosX;
        neighborIndexPosY = currTopUnit.topLevelIndexPosY - 1;
        if (!IsTopWorldUnitExist (neighborIndexPosX, neighborIndexPosY))
        {
            CreateTopLevel (neighborIndexPosX, neighborIndexPosY);
        }

        // left Neighbor
        neighborIndexPosX = currTopUnit.topLevelIndexPosX - 1;
        neighborIndexPosY = currTopUnit.topLevelIndexPosY;
        if (!IsTopWorldUnitExist (neighborIndexPosX, neighborIndexPosY))
        {
            CreateTopLevel (neighborIndexPosX, neighborIndexPosY);
        }

        // right neighbor
        neighborIndexPosX = currTopUnit.topLevelIndexPosX + 1;
        neighborIndexPosY = currTopUnit.topLevelIndexPosY;
        if (!IsTopWorldUnitExist (neighborIndexPosX, neighborIndexPosY))
        {
            CreateTopLevel (neighborIndexPosX, neighborIndexPosY);
        }

        // left top
        neighborIndexPosX = currTopUnit.topLevelIndexPosX - 1;
        neighborIndexPosY = currTopUnit.topLevelIndexPosY + 1;
        if (!IsTopWorldUnitExist (neighborIndexPosX, neighborIndexPosY))
        {
            CreateTopLevel (neighborIndexPosX, neighborIndexPosY);
        }

        // left down
        neighborIndexPosX = currTopUnit.topLevelIndexPosX - 1;
        neighborIndexPosY = currTopUnit.topLevelIndexPosY - 1;
        if (!IsTopWorldUnitExist (neighborIndexPosX, neighborIndexPosY))
        {
            CreateTopLevel (neighborIndexPosX, neighborIndexPosY);
        }

        // right top
        neighborIndexPosX = currTopUnit.topLevelIndexPosX + 1;
        neighborIndexPosY = currTopUnit.topLevelIndexPosY + 1;
        if (!IsTopWorldUnitExist (neighborIndexPosX, neighborIndexPosY))
        {
            CreateTopLevel (neighborIndexPosX, neighborIndexPosY);
        }

        // right down
        neighborIndexPosX = currTopUnit.topLevelIndexPosX + 1;
        neighborIndexPosY = currTopUnit.topLevelIndexPosY - 1;
        if (!IsTopWorldUnitExist (neighborIndexPosX, neighborIndexPosY))
        {
            CreateTopLevel (neighborIndexPosX, neighborIndexPosY);
        }

    }


    public bool IsTopWorldUnitExist (int indexPosX, int indexPosY)
    {
        bool result = false;
        for (int i = 0; i < topLevelWorldUnits.Count; ++i)
        {
            if (topLevelWorldUnits [i].topLevelIndexPosX == indexPosX && topLevelWorldUnits [i].topLevelIndexPosY == indexPosY)
            {
                result = true;
                break;
            }
        }

        return result;
    }


    private void OnGUI ()
    {
        GUILayout.Label ("Explorer Pos:" + this.explorer.position);
    }

}
