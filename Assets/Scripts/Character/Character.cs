using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum CharacterTypes
    {
        Player,
        AI
    }

    [SerializeField] private CharacterTypes characterType;
    [SerializeField] private GameObject characterSprite;
    [SerializeField] private Animator characterAnimator;

    public CharacterTypes CharacterType => characterType;
    public GameObject CharacterSprite => characterSprite;
    public Animator CharacterAnimator => characterAnimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
