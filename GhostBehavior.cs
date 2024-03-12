using UnityEngine;

[RequireComponent(typeof(Ghost))]

public abstract class GhostBehavior : MonoBehaviour
{
    public Ghost ghost { get; private set; }

    public float duration;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false;
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        if (this.ghost != null)
        {
            this.enabled = true;

            CancelInvoke();
            Invoke(nameof(Disable), duration);
        }
     
    }

    public virtual void Disable()
    {
        if (this.ghost != null)
        {
            this.enabled = false;

            CancelInvoke();
        }
  
    }
}
