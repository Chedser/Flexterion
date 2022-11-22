using UnityEngine;

public class Health : BasicHealth, IDemagable{

     float _currentHealth;
    float _healthInit;

    float MAX_HEALTH;

    Animator anim;
    [SerializeField] AudioSource idleAudio;
    [SerializeField] AudioSource deathAudio;
    [SerializeField] AudioSource demageAudio;

    public float CurrentHealth { get { return _currentHealth; } }
   
    // Start is called before the first frame update
  void Awake(){

        anim = GetComponent<Animator>();
        heroType = GetComponentInParent<SpawnInfo>().heroType;

        _currentHealth = InitHealth(heroType);
        Debug.Log(_currentHealth);

    }


    public void TakeDamage(int demage) {

        if (_currentHealth <= 0) { return; }

    /*    idleAudio.Pause();

        if (idleAudio != null)
        {

            idleAudio.Stop();

        }

        if (!demageAudio.isPlaying)
        {

            demageAudio.Play();

        } */

        _currentHealth -= demage;


        if (_currentHealth <= 0)
        {

            Die();

            Invoke(nameof(Respawn), 3.0f);

        }

        if (heroType == HeroType.Player) {

            HealthImageColorChanger hicc = GetComponentInChildren<HealthImageColorChanger>();
            hicc.ChangeColor(_currentHealth, MAX_HEALTH);

        }

    }

 protected override void Die() {

        anim.enabled = false;

        if (idleAudio != null) {

            idleAudio.Stop();
     
        }

        demageAudio.Stop();
        deathAudio.Play();
       
    }

    protected override float InitHealth(HeroType heroType){

        float MIN_HEALTH;

        switch (heroType) {

            case HeroType.Player: MIN_HEALTH = 101.0f; MAX_HEALTH = 110.0f; break;
            default: MIN_HEALTH = 101.0f; MAX_HEALTH = 110.0f; break;


        }

        return Random.Range(MIN_HEALTH, MAX_HEALTH);
    }

  protected override  void Respawn() {
 
        _currentHealth = InitHealth(heroType);
           anim.enabled = true;
        deathAudio.Stop();
        transform.position = GetRandomSpawnPoint();

        if (heroType == HeroType.Player){

            HealthImageColorChanger hicc = GetComponentInChildren<HealthImageColorChanger>();
            hicc.ChangeColor(_currentHealth, MAX_HEALTH);

        }

    }

 protected override   Vector3 GetRandomSpawnPoint(){

        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");

        return spawnPoints[Random.Range(0, spawnPoints.Length)].transform.position;
    }

}
