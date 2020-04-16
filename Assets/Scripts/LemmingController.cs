
using UnityEngine;
using UnityEngine.AI;
using CreatorKitCode;
using CreatorKitCodeInternal;

public class LemmingController : MonoBehaviour
{
    public float m_maxRadius = 2f;

    Transform m_followTransform; //The object to move towards
    Animator m_Animator; // reference to the animator component
    NavMeshAgent m_Agent; // reference to the NavMeshAgent
    SkinnedMeshRenderer m_Mesh;

    CharacterData m_CharacterData;

    float m_startRadius;
    float m_startDistance;

    void Awake()
    {
        m_CharacterData = GetComponent<CharacterData>();
        m_CharacterData.Init();

        m_Animator = GetComponent<Animator>();
        m_Agent = GetComponent<NavMeshAgent>();
        m_Mesh = this.GetComponentInChildren<SkinnedMeshRenderer>();

        InvokeRepeating("Tick", 0, 0.5f);

        m_startRadius = m_Agent.radius;
        m_startDistance = m_Agent.stoppingDistance;


        m_CharacterData.OnDamage += () =>
        {
            UpdateLemmingSeparation(.4f);
        };

    }

    void Update()
    {
        m_Animator.SetFloat("Speed", m_Agent.velocity.magnitude);

        if (m_CharacterData.Stats.CurrentHealth == 0)
        {
            //m_Animator.SetTrigger(m_DeathAnimHash);
            //SkinnedMeshRenderer m = this.GetComponentInChildren<SkinnedMeshRenderer>();

            if (m_Mesh)
                m_Mesh.enabled = false;
            m_Agent.enabled = false;

            //m_CharacterAudio.Death(transform.position);
            m_CharacterData.Death();

            GetComponent<Collider>().enabled = false;

            //if (m_LootSpawner != null)
            //    m_LootSpawner.SpawnLoot();

            //Destroy(m_Agent);
            //Destroy(GetComponent<Collider>());
            //Destroy(this);
            return;
        }

    }

    void Tick()
    {
        if (m_followTransform != null && m_Agent.isActiveAndEnabled)
        {
            m_Agent.destination = m_followTransform.position;

        }
    }


    public void UpdateAgentTarget(Transform toFollow)
    {
        m_followTransform = toFollow;
        Reset();
    }

    public void UpdateLemmingSeparation(float f)
    {
        m_Agent.radius += f;
        m_Agent.stoppingDistance += (f * 2f) + .1f;

        if (m_Agent.radius > m_maxRadius)
        {
            m_Agent.radius = m_maxRadius;
            m_Agent.stoppingDistance = (m_maxRadius * 2f) + .2f;
        }
    }

    private void Reset()
    {
        m_Agent.radius = m_startRadius;
        m_Agent.stoppingDistance = m_startDistance;

        if (m_Mesh)
            m_Mesh.enabled = true;
        m_Agent.enabled = true;
        m_CharacterData.Init();
        GetComponent<Collider>().enabled = true;
    }

}

