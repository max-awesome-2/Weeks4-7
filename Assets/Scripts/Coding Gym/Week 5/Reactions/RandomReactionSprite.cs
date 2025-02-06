using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomReactionSprite : MonoBehaviour
{
    public GameObject reactionBox;
    public Image reactionImage;

    public List<Sprite> randomSprites;

    public float playerWithinDistance = 3;

    private bool playerWithinRange = false;

    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= playerWithinDistance)
        {
            if (!playerWithinRange)
            {
                reactionBox.SetActive(true);

                playerWithinRange = true;

                reactionImage.sprite = randomSprites[Random.Range(0, randomSprites.Count)];
            }
        } else
        {
            if (playerWithinRange)
            {
                reactionBox.SetActive(false);

                playerWithinRange = false;
            }
        }
    }
}
