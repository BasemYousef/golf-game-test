using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    public static GameManager Instance { get; private set; }

    private int shotCount;
    private bool levelCompleted;
    public bool IsLevelCompleted => levelCompleted;
    public int ShotCount => shotCount;

    [SerializeField] private int threeStars ;
    [SerializeField] private int twoStars;
    [SerializeField] private int oneStar;
    private void Awake()
    {
        Instance=this;
    }

   public void RegisterShot()
    {
        if (levelCompleted) return;
        shotCount++;

        Debug.Log("Shots taken" + shotCount);
    }

    public void CompleteLevel()
    {
        if (levelCompleted)
            return;

        levelCompleted = true;

        int stars = CalculateStars();

        Debug.Log("Level completed!");
        Debug.Log("Total shots: " + shotCount);
        Debug.Log("Stars earned: " + stars);

        uiManager.ShowVictory(stars);
    }

    private int CalculateStars()
    {
        if (shotCount < threeStars) 
        return 3;
        if (shotCount < twoStars)
            return 2;
        if (shotCount >= oneStar) 
            return 1;
        return 0;
    }
   
}
