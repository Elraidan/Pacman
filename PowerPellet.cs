using UnityEngine;

public class PowerPellet : MonoBehaviour
{
    public float duration = 8.0f;
     public int points = 50;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Eat();
        }
    }

    private void Eat()
    {
        MyGameManager gameManager = FindObjectOfType<MyGameManager>();
        if (gameManager != null)
        {
            gameManager.PowerPelletEaten(this);
            Destroy(gameObject); // Assuming you want to destroy the power pellet when eaten
        }
    }
}

public class MyGameManager : MonoBehaviour
{
    // Existing code...

    public void PowerPelletEaten(PowerPellet powerPellet)
    {
        // Handle power pellet eaten logic
    }
}
