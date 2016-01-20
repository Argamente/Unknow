using UnityEngine;
using System.Collections.Generic;

namespace Argamente.Fight.Data
{
    public class WorldUnit : AbstractWorldUnit
    {

        /// <summary>
        /// 当前区块包含的原始区块数量
        /// </summary>
        public int unit_width_count = 0;
        public int unit_height_count = 0;


        /// <summary>
        /// 当前区块的真实宽度
        /// </summary>
        /// <value>The width of the unit.</value>
        public float unit_width {
            get
            {
                return unit_width_count * originWorldUnitSize;
            }
        }

        public float unit_height {
            get
            {
                return unit_height_count * originWorldUnitSize;
            }
        }


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
        public List<WorldUnit> nextLevelWorldUnits = null;

        /// <summary>
        /// 未探索过的子区块
        /// </summary>
        public List<WorldUnit> unExplorUnits = null;

        /// <summary>
        /// 已探索过的子区块
        /// </summary>
        public List<WorldUnit> exploredUnits = null;

        public WorldUnit left = null;
        public WorldUnit right = null;
        public WorldUnit up = null;
        public WorldUnit down = null;
        public WorldUnit leftUp = null;
        public WorldUnit rightUp = null;
        public WorldUnit leftDown = null;
        public WorldUnit rightDown = null;



        public void Create (bool isCreateNeighbourUnit)
        {

            if (!isCreateNeighbourUnit)
            {
                return;
            }

            // 创建八个方向相邻的区块
            if (left == null)
            {
                left = new WorldUnit ();
                left.Create (false);
                left.right = this;
            }

            if (right == null)
            {
                right = new WorldUnit ();
                right.Create (false);
                right.left = this;
            }

            if (up == null)
            {
                up = new WorldUnit ();
                up.Create (false);
                up.down = this;
            }

            if (down == null)
            {
                down = new WorldUnit ();
                down.Create (false);
                down.up = this;
            }

            if (leftUp == null)
            {
                leftUp = new WorldUnit ();
                leftUp.Create (false);
                leftUp.rightDown = this;
            }

            if (rightUp == null)
            {
                rightUp = new WorldUnit ();
                rightUp.Create (false);
                rightUp.leftDown = this;
            }

            if (leftDown == null)
            {
                leftDown = new WorldUnit ();
                leftDown.Create (false);
                leftDown.rightUp = this;
            }

            if (rightDown == null)
            {
                rightDown = new WorldUnit ();
                rightDown.Create (false);
                rightDown.leftUp = this;
            }
        }


        public void Destroy ()
        {
            if (left != null)
            {
                left.right = null;
            }

            if (right != null)
            {
                right.left = null;
            }

            if (up != null)
            {
                up.down = null;
            }

            if (down != null)
            {
                down.up = null;
            }

            if (leftUp != null)
            {
                leftUp.rightDown = null;
            }

            if (rightUp != null)
            {
                rightUp.leftDown = null;
            }

            if (leftDown != null)
            {
                leftDown.rightUp = null;
            }

            if (rightDown != null)
            {
                rightDown.leftUp = null;
            }


			
        }




        public void Explore (Transform explorer)
        {

            if (unitLevel == 0)
            {
                ExploredCompletely ();
            }
            else
            {
                ExploreSubUnits (explorer);
            }
        }


        private void ExploreSubUnits (Transform explorer)
        {
            if (unExplorUnits != null)
            {
                for (int i = 0; i < unExplorUnits.Count; ++i)
                {
                    unExplorUnits [i].Explore (explorer);
                }
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

        }

































    }
}
