using System.Collections;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;
    public LayerMask obstacleLayer;

    private const float TransitionDuration = 0.5f;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        // Check for active self to prevent error when object is destroyed
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(AnimateExitTransition());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Reverse direction every time the ghost hits a wall to create the
        // effect of the ghost bouncing around the home
        if (enabled && collision.gameObject.layer == obstacleLayer.value)
        {
            if (ghost != null && ghost.movement != null)
            {
                ghost.movement.SetDirection(-ghost.movement.direction);
            }
        }
    }

    private IEnumerator AnimateExitTransition()
    {
        // Turn off movement while we manually animate the position
        if (ghost != null && ghost.movement != null && ghost.transform != null)
        {
            ghost.movement.SetDirection(Vector2.up, true);
            ghost.movement.rigidbody.isKinematic = true;
            ghost.movement.enabled = false;

            Vector3 startPosition = ghost.transform.position;

            float elapsed = 0f;

            // Animate to the starting point
            while (elapsed < TransitionDuration)
            {
                ghost.transform.position = Vector3.Lerp(startPosition, inside.position, elapsed / TransitionDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            elapsed = 0f;

            // Animate exiting the ghost home
            while (elapsed < TransitionDuration)
            {
                ghost.transform.position = Vector3.Lerp(inside.position, outside.position, elapsed / TransitionDuration);
                elapsed += Time.deltaTime;
                yield return null;
            }

            // Pick a random direction left or right and re-enable movement
            ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1f : 1f, 0f), true);
            ghost.movement.rigidbody.isKinematic = false;
            ghost.movement.enabled = true;
        }
    }
}
