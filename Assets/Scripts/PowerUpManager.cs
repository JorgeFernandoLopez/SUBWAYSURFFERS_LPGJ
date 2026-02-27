using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField]
    private int minPlatformsNumber = 5;
    [SerializeField]
    private int maxPlaformsNumber = 7;
    [SerializeField]
    private InstantiatePoolObjects[] powerUpPools;
    [SerializeField]
    private float powerUpOffset = 2f;
    private int PlatformsNumber;
    private int platformsCounter = 0;
    public void Awake()
    {
        SetPlatformsNumber();
            
        
    }
    private void SetPlatformsNumber()
    {
        PlatformsNumber = Random.Range(minPlatformsNumber, maxPlaformsNumber);
    }
    public void PlatformPassed(Platform platform)
    {
        platformsCounter++;
        if (platformsCounter >= PlatformsNumber)
        {
            SpawnPowerUp(platform);
            platformsCounter = 0;
            SetPlatformsNumber();
        }

    }
    private void SpawnPowerUp(Platform platform)
    {
        InstantiatePoolObjects pool = powerUpPools[Random.Range(0, powerUpPools.Length)];
        pool.InstantiateObject(Vector3.zero);
        GameObject powerUp = pool.GetCurrentObject();
        powerUp.transform.Translate(  Vector3.up * powerUpOffset);
        platform.AddPowerUp(powerUp);
    }
}
