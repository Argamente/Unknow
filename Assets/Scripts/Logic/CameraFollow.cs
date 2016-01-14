using UnityEngine;
using System.Collections;
using Argamente.Fight.Actors;

public class CameraFollow : MonoBehaviour {

    private static CameraFollow _instance;
    public static CameraFollow GetInstance()
    {
        return _instance;
    }

    private Transform m_MainCamera;

    private float ViewSize_Width = 0;
    private float ViewSize_Height = 0;

    void Awake()
    {
        m_MainCamera = transform;
        _instance = this;

        // 设置跟随目标
        GameObject followPlayer = GameObject.FindGameObjectWithTag("Player");
        if(followPlayer != null)
        {
            Actor actor = followPlayer.GetComponent<Actor>();
            if(actor != null)
            {
                actor.onOutofView = this.OnFollowerOutOfView;
            }
        }
        CalculateViewPointSize();
    }

    /// <summary>
    /// 如果追随者超出了摄像机范围，则滚动一屏
    /// </summary>
    /// <param name="followerTrans"></param>
    public void OnFollowerOutOfView(Transform followerTrans)
    {
        if(followerTrans == null || m_MainCamera == null)
        {
            return;
        }

        Vector3 diff = followerTrans.position - m_MainCamera.position;
        float offsetX = 0;
        float offsetY = 0;

        float diffX = Mathf.Abs(diff.x);
        float diffY = Mathf.Abs(diff.y);

        if(diffX > ViewSize_Width / 2)
        {
            int dir = diff.x > 0 ? 1 : -1;
            offsetX = dir * ViewSize_Width;
        }

        if(diffY > ViewSize_Height / 2)
        {
            int dir = diff.y > 0 ? 1 : -1;
            offsetY = dir * ViewSize_Height;
        }

        m_MainCamera.position += new Vector3(offsetX, offsetY, 0);
    }
	
    /// <summary>
    /// 计算摄像机所看到的宽度和高度（在世界坐标内）
    /// </summary>
    public void CalculateViewPointSize()
    {
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Mathf.Abs(m_MainCamera.position.z)));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Mathf.Abs(m_MainCamera.position.z)));
        Debug.Log(bottomLeft + "    " + topRight);
        ViewSize_Width = topRight.x - bottomLeft.x;
        ViewSize_Height = topRight.y - bottomLeft.y;
    }

}
