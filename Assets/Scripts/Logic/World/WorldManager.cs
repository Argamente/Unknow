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
        WorldUnit unit = new WorldUnit ();
        unit.unitLevel = 3;
        unit.unit_width_count = 30;
        unit.unit_height_count = 30;
        unit.realPos_x = 0;
        unit.realPos_y = 0;

        unit.Create (); 

        topLevelWorldUnits.Add (unit);

        //unit.childWorldUnits [0].Explore (explorer);
    }


    void Update ()
    {

        for (int i = 0; i < topLevelWorldUnits.Count; ++i)
        {
            topLevelWorldUnits [i].Explore (explorer);
        }
        return;

        //return;
        if (explorer == null)
        {
            return;
        }

        if (topLevelWorldUnits.Count <= 0)
        {
            return;
        }

        ++exploreIndex;
        if (exploreIndex >= topLevelWorldUnits.Count)
        {
            exploreIndex = 0;
        }

        topLevelWorldUnits [exploreIndex].Explore (this.explorer);
    }




}
