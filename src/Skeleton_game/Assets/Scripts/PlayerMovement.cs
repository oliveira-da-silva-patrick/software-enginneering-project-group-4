
/**
    Script Description

    This script is supposed to be attached to the Main player in Unity. There are 2 serialized fields that need to be filled.

        * Speed: Set this to whatever value suits your needs.

        * rotateSpeed: Set this to whatever value suits your needs.

        * Joystick: Drag your joystick inside this field. If you dont have a joystick yet please follow the instructions on the 'Creator's Guide'.

        * rb: Player's Rigidbody2D.

        * distance: The value of the distance between the closest enemy.

        * movement : Get the input of the joystick and translate it in movement for the player.

        * firePoint: Is the shooting point of the player needs an empty object (which is below the main player called Fire_Point).

        * attackPrefab: Needs the Player_shoot (the projectiles that the player shoots).

        * fireRate: Variable that sets the attack speed of the player.

        * nextFire: Variable which tells the methode that it has to shoot again.

        * enemy: Gets the closest enemy to the player.

    Once this is done the player should now move. (You can also use the 'CameraController' script to make the camera follow your player)

    Animations:

        Within this script you will find an animation section. The script is already setup for a 'Run' animation, but you
    can remove and/or add more animations.

    WARNING!!

    If you dont setup the animator for the player you will get a compilation error or warning BUT dont worry this won't affect your testing.

    For more instructions on how to setup your animations please check out the 'Creator's Guide'.



**/

//----------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private float rotateSpeed;

    public Joystick joystick;
    private Rigidbody2D rb;
    private float distance;
    Vector2 movement;
    public Transform firePoint;
    public GameObject attackPrefab;
    public GameObject attackPrefabAxis;
    public GameObject magnetPrefab;

    private float fireRateNormal = 1.5f;
    private float fireRateAxis = 2f;
    private float nextFireNormal = 0.0F;
    private float nextFireAxis = 0.0F;


    public GameObject laserPrefab;
    public float laserRate = 1f;
    private float nextLaserDamage = 0f;

    public GameObject linePrefab;
    private List<GameObject> lineList;
    public LayerMask obstacleLayer;


    private GameObject enemy;
    private GameObject[] enemies;



    void Start()
    {
        rotateSpeed = 1500f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        Load();
        if(SkillTree.UnlockedAbilities[10])
        {
            fireRateNormal = fireRateNormal - 0.2F;
        }
        if(SkillTree.UnlockedAbilities[13])
        {
            fireRateNormal = fireRateNormal - 0.3F;
        }
        if(SkillTree.UnlockedAbilities[24])
        {
            fireRateAxis = 1f;
        }
        if(SkillTree.UnlockedAbilities[15])
        {
            Instantiate(magnetPrefab, new Vector2(transform.position.x - 3f, transform.position.y), Quaternion.Euler(0, 0, 0));
            Instantiate(magnetPrefab, new Vector2(transform.position.x + 1.5f, transform.position.y + 2.6f), Quaternion.Euler(0, 0, 0));
            Instantiate(magnetPrefab, new Vector2(transform.position.x + 1.5f, transform.position.y - 2.6f), Quaternion.Euler(0, 0, 0));
        }else if(SkillTree.UnlockedAbilities[14])
        {
            Instantiate(magnetPrefab, new Vector2(transform.position.x - 3f, transform.position.y), Quaternion.Euler(0, 0, 0));
            Instantiate(magnetPrefab, new Vector2(transform.position.x + 3f, transform.position.y), Quaternion.Euler(0, 0, 0));
        } else if(SkillTree.UnlockedAbilities[11])
        {
            Instantiate(magnetPrefab, new Vector2(transform.position.x - 3f, transform.position.y), Quaternion.Euler(0, 0, 0));
        }

        if(SceneManager.GetActiveScene().name.Contains("Floor")) Spawn();
    }

    private void Spawn()
    {
        int position = FloorInfo.getLastVisitedRoom();
        Debug.Log(position);
        if(position == 0)
            rb.transform.position = GameObject.Find("LBSpawn").transform.position;
        else if (position == 1)
            rb.transform.position = GameObject.Find("RBSpawn").transform.position;
        else if (position == 2)
            rb.transform.position = GameObject.Find("LTSpawn").transform.position;
        else if (position == 3)
            rb.transform.position = GameObject.Find("RTSpawn").transform.position;
        else
            rb.transform.position = GameObject.Find("Spawn").transform.position;
    }



    // Update is called once per frame
    void Update()
    {
        // destroy children with linerenderers
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("LineRendererPrefab");
        foreach(GameObject g in gos)
        {
            Destroy(g);
        }

        movement.x = joystick.Horizontal;
        movement.y = joystick.Vertical;
        movement.Normalize();

        enemies = FindClosestEnemies();
        shootEnemy(movement, enemies);
        shootLasers(enemies);
    }

    void FixedUpdate()
    {
        rb.MovePosition(new Vector2(transform.position.x + (movement.x * speed * Time.deltaTime), transform.position.y + (movement.y * speed * Time.deltaTime)));
    }

    void Shoot()
    {
        if (Time.time > nextFireNormal)
        {
            nextFireNormal = Time.time + fireRateNormal;
            Instantiate(attackPrefab, firePoint.position, firePoint.rotation);
        }
        if (Time.time > nextFireAxis)
        {
            nextFireAxis = Time.time + fireRateAxis;
            if(SkillTree.UnlockedAbilities[22])
            {
                Instantiate(attackPrefabAxis, new Vector2(transform.position.x + 1.5f, transform.position.y), Quaternion.Euler(0, 0, 0));
                Instantiate(attackPrefabAxis, new Vector2(transform.position.x - 1.5f, transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            if(SkillTree.UnlockedAbilities[23])
            {
                Instantiate(attackPrefabAxis, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.Euler(0, 0, 90));
                Instantiate(attackPrefabAxis, new Vector2(transform.position.x, transform.position.y - 1.5f), Quaternion.Euler(0, 0, 270));
            }
        }
    }

    void ShootAxisXY()
    {
        if (Time.time > nextFireAxis)
        {
            nextFireAxis = Time.time + fireRateAxis;
            if(SkillTree.UnlockedAbilities[22])
            {
                Instantiate(attackPrefabAxis, new Vector2(transform.position.x + 1.5f, transform.position.y), Quaternion.Euler(0, 0, 0));
                Instantiate(attackPrefabAxis, new Vector2(transform.position.x - 1.5f, transform.position.y), Quaternion.Euler(0, 0, 180));
            }
            if(SkillTree.UnlockedAbilities[23])
            {
                Instantiate(attackPrefabAxis, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.Euler(0, 0, 90));
                Instantiate(attackPrefabAxis, new Vector2(transform.position.x, transform.position.y - 1.5f), Quaternion.Euler(0, 0, 270));
            }
        }
    }
    

    void shootLasers(GameObject[] enemies)
    {
        int laserDamage = 0;
        if (SkillTree.UnlockedAbilities[0] && enemies.Length > 0)
        {
            laserDamage = 10;
            if (SkillTree.UnlockedAbilities[1])
            {
                laserDamage = 20;
            }
            if (SkillTree.UnlockedAbilities[4])
            {
                laserDamage = 30;
            }
            // shoot closest enemy
            shootLaser(enemies[0], laserDamage);
        }
        // shoot second closest enemies
        if (SkillTree.UnlockedAbilities[2] && enemies.Length > 1)
        {
            shootLaser(enemies[1], laserDamage);
        }
        // shoot third closest enemies
        if (SkillTree.UnlockedAbilities[3] && enemies.Length > 2)
        {
            shootLaser(enemies[2], laserDamage);
        }
        // shoot all enemies
        if (SkillTree.UnlockedAbilities[5])
        {
            for (int i = 3; i < enemies.Length; i++)
            {

                shootLaser(enemies[i], laserDamage);
            }
        }
    }

    void shootLaser(GameObject enemy, float laserDamage)
    {
        distance = Vector2.Distance(transform.position, enemy.transform.position);
        if (distance <= 5f && Physics2D.Raycast(transform.position, (enemy.transform.position - transform.position).normalized, 5f, 1 << 0))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, (enemy.transform.position - transform.position).normalized, 5f, 1 << 0);
            if (hit.collider.tag == "Enemy" && !Physics2D.Raycast(transform.position, (enemy.transform.position - transform.position).normalized, 5f, obstacleLayer))
            {
                draw2DRay(transform.position, hit.point);
                //Debug.Log(hit.collider.tag);
                if (Time.time > nextLaserDamage)
                {
                    nextLaserDamage = Time.time + laserRate;
                    healthSystem health = enemy.GetComponent<healthSystem>();
                    health.TakeDamage((int)laserDamage);
                    Debug.Log(enemy.ToString());
                }
            }
        }
    }

    void draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        GameObject go = Instantiate(linePrefab);
        LineRenderer line = go.GetComponent<LineRenderer>();
        line.startWidth = 0.3f;
        line.endWidth = 0.3f;
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
    }

    void shootEnemy(Vector2 movement, GameObject[] enemies)
    {
        if (enemies.Length > 0)
        {
            enemy = enemies[0];
            distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance <= 10f && movement == Vector2.zero)
            {
                Vector3 relPos = enemy.transform.position - transform.position;
                Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * relPos;
                Quaternion toRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
                if (transform.rotation == toRotation)
                {
                    Shoot();
                }
            }
            else if (movement != Vector2.zero)
            {
                Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * movement);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation2, rotateSpeed * Time.deltaTime);
                ShootAxisXY();

            }
        }
        else
        {
            if (movement != Vector2.zero)
            {
                Quaternion toRotation2 = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 90) * movement);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation2, rotateSpeed * Time.deltaTime);
                ShootAxisXY();
            }
        }
    }

    // returns an array of all enemies in the scene, sorted by distance to the player
    // called in Update()
    public GameObject[] FindClosestEnemies()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");

        EnemyDistance[] temp = new EnemyDistance[gos.Length];

        for (int i = 0; i < gos.Length; i++)
        {
            temp[i] = new EnemyDistance((gos[i].transform.position - transform.position).sqrMagnitude, gos[i]);
        }

        temp = temp.OrderBy(x => x.distance).ToArray();

        for (int i = 0; i < gos.Length; i++)
        {
            gos[i] = temp[i].enemy;
            // Debug.Log(temp[i].enemy.ToString() + " " + temp[i].distance +  "Position : " + i);
        }

        return gos;
    }

    public void Load()
    {
        SkillTreeData data = SaveLoadSystem.LoadSkillTree();

        if (data != null)
        {
            SkillTree.UnlockedAbilities = (bool[])data.UnlockedAbilities.Clone();
        }
        else
        {
            for (var i = 0; i < SkillTree.UnlockedAbilities.Length; i++)
            {
                SkillTree.UnlockedAbilities[i] = false;
            }
        }
    }
}
