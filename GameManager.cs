using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Ghost[] ghosts;
    public Pacman[] pacmanArray;
    public Transform[] pellets;

    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int lives { get; private set; }

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet != null)
            {
                pellet.gameObject.SetActive(true);
            }
        }

        ResetState();
    }

    private void ResetState()
    {
        ResetGhostMultiplier();

        foreach (Ghost ghost in ghosts)
        {
            ghost?.ResetState();
        }

        foreach (Pacman pacman in pacmanArray)
        {
            pacman?.ResetState();

            if (pacman != null)
            {
                pacman.gameObject.SetActive(true);
            }
        }
    }

    private void GameOver()
    {
        foreach (Ghost ghost in ghosts)
        {
            ghost?.gameObject.SetActive(false);
        }

        foreach (Pacman pacman in pacmanArray)
        {
            pacman?.gameObject.SetActive(false);
        }
    }

    public void PelletEaten(Pellet pellet)
    {
        if (pellet != null)
        {
            pellet.gameObject.SetActive(false);
            SetScore(score + pellet.points);

            if (!HasRemainingPellets())
            {
                foreach (Pacman pacman in pacmanArray)
                {
                    pacman?.gameObject.SetActive(false);
                }
                Invoke(nameof(NewRound), 3.0f);
            }
        }
    }

    public void PowerPelletEaten(Pellet pellet)
    {
        if (pellet != null)
        {
            PelletEaten(pellet);
            CancelInvoke();
            ResetGhostMultiplier();
        }
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in pellets)
        {
            if (pellet != null && pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier()
    {
        ghostMultiplier = 1;
    }

    private void SetScore(int newScore)
    {
        score = newScore;
    }

    private void SetLives(int newLives)
    {
        lives = newLives;
    }

    public void GhostEaten(Ghost ghost)
    {
        if (ghost != null)
        {
            int points = ghost.points * ghostMultiplier;
            SetScore(score + points);
            ghostMultiplier++;
        }
    }

    public void PacmanEaten()
    {
        foreach (Pacman pacman in pacmanArray)
        {
            pacman?.gameObject.SetActive(false);
        }

        SetLives(lives - 1);

        if (lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }
}
