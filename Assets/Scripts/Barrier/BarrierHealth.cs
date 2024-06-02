using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierHealth : MonoBehaviour
{
    // Create instances for each segment
    [SerializeField] public SegmentHealth topSegment = new SegmentHealth { name = "Top"};
    [SerializeField] public SegmentHealth downSegment = new SegmentHealth { name = "Down"};
    [SerializeField] public SegmentHealth leftSegment = new SegmentHealth { name = "Left"};
    [SerializeField] public SegmentHealth rightSegment = new SegmentHealth { name = "Right"};

    [System.Serializable]
    public class SegmentHealth
    {
        [SerializeField] public string name;
        [SerializeField] public int maxHealt;
        [SerializeField] public int initialHealth;
        public int currentHealth { get; set; }
    }

    private void Awake()
    {
        UIManager.Instance.UpdateBarrierHealth(topSegment.maxHealt, topSegment.currentHealth, downSegment.currentHealth, leftSegment.currentHealth, rightSegment.currentHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        ResetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.UpdateBarrierHealth(topSegment.maxHealt, topSegment.currentHealth, downSegment.currentHealth, leftSegment.currentHealth, rightSegment.currentHealth);
        if (Input.GetKeyDown(KeyCode.P))
        {
            CheatHealth();
        }
    }
    public void ResetHealth()
    {
        topSegment.currentHealth = topSegment.initialHealth;
        downSegment.currentHealth = downSegment.initialHealth;
        leftSegment.currentHealth = leftSegment.initialHealth;
        rightSegment.currentHealth = rightSegment.initialHealth;
    }

    public void CheatHealth()
    {
        topSegment.currentHealth = 10000;
        downSegment.currentHealth = 10000;
        leftSegment.currentHealth = 10000;
        rightSegment.currentHealth = 10000;
    }
    public void TakeDamage(string segmentName, int damage)
    {
        SegmentHealth segment = GetSegment(segmentName);
        if (segment != null)
        {
            segment.currentHealth -= damage;
            segment.currentHealth = Mathf.Max(segment.currentHealth, 0);
            //Debug.Log(segmentName + " segment took " + damage + " damage, remaining health: " + segment.currentHealth);
            CheckGameOver();
        }

    }

    private SegmentHealth GetSegment(string name)
    {
        switch (name.ToLower())
        {
            case "top": return topSegment;
            case "down": return downSegment;
            case "left": return leftSegment;
            case "right": return rightSegment;
            default:
                Debug.LogError("Invalid segment name.");
                return null;
        }
    }

    private void CheckGameOver()
    {
        if (topSegment.currentHealth <= 0 || downSegment.currentHealth <= 0 || leftSegment.currentHealth <= 0 || rightSegment.currentHealth <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over: One of the barrier segments has reached 0 health.");
        SoundManager.Instance.musicAudioSource.clip = SoundManager.Instance.GameOverMusicClip;
        SoundManager.Instance.musicAudioSource.Play();
        Time.timeScale = 0f;
        // Implement additional game over logic here, such as displaying a game over screen, stopping the game, etc.
    }
}


