using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        if (this.ghost != null && this.ghost.chase != null)
        {
            this.ghost.chase.Enable();
        }
        else
        {
          
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frighten.enabled && node.availableDirections.Count > 0)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0; // Reset the index to avoid going out of range
                }
            }

            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }
}
