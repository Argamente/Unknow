using UnityEngine;
using System.Collections;

[System.Serializable]
public class SpriteAnimationClip {
    public string name;
    public Sprite[] sprites;
    public Listener<SpriteAnimationClip> onActive;

    private bool _isActive = false;

    public bool isActive
    {
        get
        {
            return this._isActive;
        }
        set
        {
            this._isActive = value;
            if (_isActive == true)
            {
                if (onActive != null)
                {
                    onActive(this);
                }
            }
        }
    }

}
