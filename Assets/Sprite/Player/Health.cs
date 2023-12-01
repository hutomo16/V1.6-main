using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]public int maxhealth;
    public int currentHealth;

    [Header("Health Sprite")]
    [SerializeField] private Sprite fullHealth;
    [SerializeField] private Sprite emptyHealth;
    [SerializeField] private Image[] HeartContainer;

    //player dead
    [HideInInspector] public bool playerisDead;


    private Rigidbody2D rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxhealth;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 && !playerisDead)
        {
            GameOver();
        }

        if(currentHealth > maxhealth)
        {
            currentHealth = maxhealth;
        }
    }

    private void OnGUI()
    {
        for(int i = 0; i < HeartContainer.Length; i++)
        {
            if(i < currentHealth)
            {
                HeartContainer[i].sprite = fullHealth;
            }
            else
            {
                HeartContainer[i].sprite = emptyHealth;
            }
        }
    }

    public void GameOver()
    {
        playerisDead = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth += amount;
    }

    internal void TakeDamage(int v)
    {
        throw new NotImplementedException();
    }
}
