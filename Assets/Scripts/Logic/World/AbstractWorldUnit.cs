using UnityEngine;
using System.Collections;

namespace Argamente.Fight.Data
{
    public class AbstractWorldUnit
    {
        /// <summary>
        /// 最小区块大小
        /// </summary>
        public float originWorldUnitSize = 0.64f;

        /// <summary>
        /// 区块等级，最小为0，最大为正无穷
        /// </summary>
        public int unitLevel = 0;

        public float exploreDistance = 0.0f;

        /// <summary>
        /// 是否被探索过
        /// </summary>
        public bool isExplored = false;


        public Listener<AbstractWorldUnit> onExplored;


        public virtual void RenderUnit(bool explored)
        {

        }
    }
}
