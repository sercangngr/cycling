using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimation : MonoBehaviour 
{

    SpriteRenderer spriteRenderer;
	public Sprite[] sprites;
	public float frameDuration = 0.1f;
	int spritedIdx = 0;
	float timer = 0;

	// Use this for initialization
	void Start()
	{
        spriteRenderer = GetComponent<SpriteRenderer>();
		timer = frameDuration;
	}

	void Update()
	{
		timer -= Time.deltaTime;
		if (timer <= 0)
		{
			UpdateSprite();
			timer = frameDuration;
		}

	}

	void UpdateSprite()
	{
		spritedIdx = (spritedIdx + 1) % sprites.Length;
        spriteRenderer.sprite = sprites[spritedIdx];
	}

}
