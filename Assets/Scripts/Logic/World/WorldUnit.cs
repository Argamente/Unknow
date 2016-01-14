﻿using UnityEngine;
using System.Collections.Generic;

namespace Argamente.Fight.Data
{
    public class WorldUnit : AbstractWorldUnit
    {
        /// <summary>
        // 当前世界块的宽度和高度，（都是在原始世界块大小的基础上算出来的，大小永远为 (x * 2 + 1) * originWorldUnitSize
        // 以保证最小区块中心点在世界(0.0)的位置
        /// </summary>
        //public float unit_width = 0.0f;
        //public float unit_height = 0.0f;

        /// <summary>
        ///  以当前区块大小为单元大小，距离原点的距离（肯定为整数）
        /// </summary>
        public int pos_x = 0;
        public int pos_y = 0;

        /// <summary>
        /// 当前区块距离远点的真实距离
        /// </summary>
        //public float realPos_x = 0.0f;
        //public float realPos_y = 0.0f;


        /// <summary>
        /// 是否被完全探索过（只有当所有子区块被完全探索过，此值才能true
        /// </summary>
        public bool isExploredCompletely = false;

        /// <summary>
        /// 子区块
        /// </summary>
        public List<WorldUnit> nextLevelWorldUnits = null;

        /// <summary>
        /// 未探索过的子区块
        /// </summary>
        public List<WorldUnit> unExplorUnits = null;

        /// <summary>
        /// 已探索过的子区块
        /// </summary>
        public List<WorldUnit> exploredUnits = null;
        


        public void Explore(Transform explorer)
        {

            if(unitLevel == 0)
            {
                ExploredCompletely();
            }
            else
            {
                ExploreSubUnits(explorer);
            }
        }


        private void ExploreSubUnits(Transform explorer)
        {
            if(unExplorUnits != null)
            {
                for(int i = 0; i < unExplorUnits.Count; ++i)
                {
                    unExplorUnits[i].Explore(explorer);
                }
            }
        }


        private void ExploredCompletely()
        {
            this.isExplored = true;
            this.isExploredCompletely = true;
            this.RenderUnit(this.isExploredCompletely);
            if(this.onExplored != null)
            {
                this.onExplored(this);
            }
        }


        public override void RenderUnit(bool explored)
        {
            base.RenderUnit(explored);

        }

































    }
}