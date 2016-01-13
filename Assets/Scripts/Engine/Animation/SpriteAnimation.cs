using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour {
    public SpriteAnimationClip[] clips;

    private SpriteRenderer m_SpriteHolder;

    private int spriteIndex = 0;
    private SpriteAnimationClip currClip;
    private Sprite[] currClipSprites = null;

    public float delayTime = 0.1f;
    private float currTime = 0.0f;


    void Awake()
    {
        m_SpriteHolder = this.gameObject.GetComponent<SpriteRenderer>();
        InitClips();
    }


    void InitClips()
    {
        for(int i = 0; i < clips.Length; ++i)
        {
            clips[i].isActive = false;
            clips[i].onActive = OnClipActive;
        }
    }

    void OnClipActive(SpriteAnimationClip clip)
    {
        if(currClip == clip)
        {
            return;
        }

        InitClips();
        //Debug.LogError("Active:" + clip.name);
        currClip = clip;
        currClipSprites = clip.sprites;
        spriteIndex = 0;
        currTime = -1;
    }


    public void Play(string name)
    {
        if(clips == null)
        {
            Debug.LogError("Clips can not be null");
            return;
        }


        for(int i = 0; i < clips.Length; ++i)
        {
            if (clips[i].name == name)
            {
                clips[i].isActive = true;
                break;
            }
        }
    }

    public void Stop()
    {
        currClip = null;
        currClipSprites = null;
    }


    void Update()
    {
        if(Time.time - currTime < delayTime)
        {
            return;
        }

        currTime = Time.time;
        if(currClipSprites != null)
        {
            if(spriteIndex >= currClipSprites.Length)
            {
                spriteIndex = 0;
            }

            m_SpriteHolder.sprite = currClipSprites[spriteIndex++];
        }
    }
	
}
