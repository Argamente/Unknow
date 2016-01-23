using UnityEngine;
using System.Collections.Generic;
using Argamente.Fight.Managers;
using Argamente.Fight.Constants;
using Argamente.Utils;

namespace Argamente.Fight.Data
{
    public class WorldUnit : AbstractWorldUnit
    {

        /// <summary>
        /// 当前区块包含的原始区块数量
        /// </summary>

        private float exploreUnitCount = 3.4444f;


        private int _unit_width_count = 0;
        private int _unit_height_count = 0;

        public int unit_width_count {
            get
            {
                return this._unit_width_count;
            }
            set
            {
                this._unit_width_count = value;
                this.unit_width = this._unit_width_count * originWorldUnitSize;
                this.exploreDistance = this.unit_width / 2 + exploreUnitCount * originWorldUnitSize;
            }
        }

        public int unit_height_count {
            get
            {
                return this._unit_height_count;   
            }
            set
            {
                this._unit_height_count = value;
                this.unit_height = this._unit_height_count * originWorldUnitSize;
            }
        }


        /// <summary>
        /// 当前区块的真实宽度
        /// </summary>
        /// <value>The width of the unit.</value>
        public float unit_width = 0.0f;

        public float unit_height = 0.0f;


        public float exploreDistance = 0.0f;


        // 当前区块大小，在父区块中的索引层，（区块总是正方形的）
        public int unitIndexFromParentUnit = 0;


        /// <summary>
        /// 当前区块距离原点的真实距离，也就是当前区块的中心点
        /// </summary>
        public float realPos_x = 0.0f;
        public float realPos_y = 0.0f;


        /// <summary>
        /// 是否被完全探索过（只有当所有子区块被完全探索过，此值才能true
        /// </summary>
        public bool isExploredCompletely = false;

        /// <summary>
        /// 子区块
        /// </summary>
        public List<WorldUnit> childWorldUnits = new List<WorldUnit> ();

        /// <summary>
        /// 未探索过的子区块
        /// </summary>
        public List<WorldUnit> unExplorUnits = new List<WorldUnit> ();

        /// <summary>
        /// 已探索过的子区块
        /// </summary>
        public List<WorldUnit> exploredUnits = new List<WorldUnit> ();


        private GameObject unitObj = null;



        /// <summary>
        /// 创建的时候，父区块知道是否要创建子区块s 
        /// </summary>
        public void Create ()
        {
            if (unitLevel == 0)
            {
                RenderUnit (false);
            }
            else
            {
                // 获得子区块的基础数据
                WorldUnitVo childUnitVo = WorldManager.GetInstance ().GetChildWorldUnitVoByParentLevel (this.unitLevel);

                // 计算横向纵向各有多少个子区块
                int horChildUnitCount = unit_width_count / childUnitVo.unit_count_width;
                int verChildUnitCount = unit_height_count / childUnitVo.unit_count_height;

                float childUnitWidth = childUnitVo.unit_count_width * originWorldUnitSize;
                float childUnitHeight = childUnitVo.unit_count_height * originWorldUnitSize;

                float startChildPos_x = realPos_x - (unit_width / 2) + childUnitWidth / 2;
                float startChildPos_y = realPos_y + (unit_height / 2) - childUnitHeight / 2;


                // 一行一行地去创建子区块
                for (int verIndex = 0; verIndex < verChildUnitCount; ++verIndex)
                {
                    for (int horIndex = 0; horIndex < horChildUnitCount; ++horIndex)
                    {
                        WorldUnit childUnit = new WorldUnit ();
                        childUnit.unitLevel = childUnitVo.level;
                        childUnit.unit_width_count = childUnitVo.unit_count_width;
                        childUnit.unit_height_count = childUnitVo.unit_count_height;
                        childUnit.realPos_x = startChildPos_x + horIndex * childUnitWidth;
                        childUnit.realPos_y = startChildPos_y - verIndex * childUnitHeight;
                        unExplorUnits.Add (childUnit);
                        childWorldUnits.Add (childUnit);
                    }
                }

                // 调用子区块的Create方法

                for (int unitIndex = 0; unitIndex < childWorldUnits.Count; ++unitIndex)
                {
                    childWorldUnits [unitIndex].Create ();
                }

            }

        }


        /// <summary>
        /// 当一个区块销毁时，需要告诉自己的相邻区块，以便相邻区块不再连接自己 
        /// </summary>
        public void Destroy ()
        {
            
			
        }




        public void Explore (Transform explorer)
        {

            if (unitLevel == 0)
            {
                if (isExplored)
                {
                    return;
                }
            }

            Vector2 unitPos = new Vector2 (realPos_x, realPos_y);
            Vector2 explorerPos = new Vector2 (explorer.position.x, explorer.position.y);


            float dis = Vector2.Distance (unitPos, explorerPos);

            //float disX = Mathf.Abs (explorerPos.x - unitPos.x);
            //float disY = Mathf.Abs (explorerPos.y - unitPos.y);

            //if (unitLevel < 2)
            //{
            if (dis < exploreDistance)
            {
                if (unitLevel == 0)
                {
                    ExploredCompletely ();
                }
                else
                {
                    ExploreChildUnits (explorer);
                }
            } 
            //}
            //else
            //{
            //    if (disX < exploreDistance && disY < exploreDistance)
            //    {
            //        ExploreChildUnits (explorer); 
            //    }
            //}

            // Log.Info (this, "Level:", unitLevel, "UnitPos:", unitPos, "ExplorerPos:", explorerPos, "Dis:", dis);
        }



        private void ExploreChildUnits (Transform explorer)
        {
            for (int i = 0; i < childWorldUnits.Count; ++i)
            {
                childWorldUnits [i].Explore (explorer);
            }
        }



        private void ExploredCompletely ()
        {
            this.isExplored = true;
            this.isExploredCompletely = true;
            this.RenderUnit (this.isExploredCompletely);
            if (this.onExplored != null)
            {
                this.onExplored (this);
            }
        }


        public override void RenderUnit (bool explored)
        {
            base.RenderUnit (explored);

            if (!explored)
            {
                unitObj = AssetsManager.GetInstance ().InstantiateGameObject (AppConstant.UnExploreConverPrefab);
                unitObj.transform.position = new Vector3 (realPos_x, realPos_y, 0);
            }
            else
            {
                if (unitObj != null)
                {
                    AssetsManager.GetInstance ().DestroyGameObject (AppConstant.UnExploreConverPrefab, unitObj);
                }
                
            }

        }

































    }
}
