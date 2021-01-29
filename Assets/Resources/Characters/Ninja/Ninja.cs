using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Character
{
    //Personnage ninja, rapide , faible degats mais grande resistance 
    //A : lancer de shurikan 
    //Z : Buff de speed 
    //E : Boule de feu 
    //R : Téléportation


    [SerializeField] GameObject m_shuriken;
    [SerializeField] GameObject m_fireball;

    public Ninja() : base("Ninja", 150, 120, 9f)
    {
       
    }


    void Start()
    {

        this.InitialisationStat(0, 0, 0, 0); //Init base statistiques
        this.InitialisationRegen(1, 2); //Init pv and pm regen
        this.InitialisationDeplacement(); //Init movement
        this.InitialisationGUI("Characters/Ninja/"); //Init GUI
        //this.InitialisationAnimator();

        #region Init Action
        //Initialisation des actions
        Action Auto,A,Z,E,R;
        Auto = null;
        A = new Action("Shuriken", 50, 20, 20, 5f, "Envoie d'un shuriken ! ", 1f, Action.TYPE_OF_ACTION.dammage, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.projectile);
        //Load A projectile Assets
        A.setProjectilePath("Characters/Ninja/Action/Shuriken/Shuriken");
        A.setProjectileSpeed(20);
        A.setProjectileRotation(true);
        A.setStartHeightPos(transform.position.y + 5f);
        A.setProjectile(m_shuriken);

        Z = new Action("Shadow Speed" , 25, 15, 0f, 6f, "Augmente votre vitesse de déplacement" , .1f, Action.TYPE_OF_ACTION.speed_up, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.self);
        
        E = new Action("Fireball", 50, 20, 20, 5f, "Envoie d'une boule de feu ! ", 1f, Action.TYPE_OF_ACTION.dammage, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.projectile);
        //Load A projectile Assets
        E.setProjectilePath("Characters/Ninja/Action/Fireball/Fireball");
        E.setProjectileSpeed(20);
        E.setProjectileRotation(false);
        E.setStartHeightPos(transform.position.y + 5f);
        E.setProjectile(m_fireball);
        E.setReorientation(true);


        R = null;
        #endregion
        InitialisationAction(Auto, A, Z, E, R);

    }


    new void Update()
    {
        base.Update();

        if (this.Animated)
            return;
        //Recuperation du clic droit
        bool RCB = Input.GetMouseButtonDown(1); //Récuperation du clic droit souris
        if (RCB && this.IsControllable)
        {
            RaycastHit hit;
            Ray ray;
            if (Camera.main != null)
                ray = Camera.main.ScreenPointToRay(Input.mousePosition); //Raycast slon la camera pour voir ou l'on a cliqué 
            else
                ray = this.m_camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 destination = hit.point;
                if (hit.collider.gameObject.GetComponent<Character>() != null)
                    destination = hit.collider.gameObject.transform.position;
                if (A && Vector3.Distance(this.transform.position, destination) < this.getAAction().getRange())
                {
                    LaunchA(destination);
                }
                if (E && Vector3.Distance(this.transform.position, destination) < this.getEAction().getRange())
                {
                    LaunchE(destination);
                }
            }
        }


        //Lecture des inputs
        if (Input.GetButton("A") && !A && this.IsControllable && this.getAAction().getCost() <= this.getPm() && !cdA)
        {
            A = true;
            ShowActionRange(this.getAAction());
        }
        if (Input.GetButton("Z") && !Z && this.IsControllable && this.getZAction().getCost() <= this.getPm() && !cdZ)
        {
            Z = true;
            LaunchZ();
        }
        if (Input.GetButton("E") && !E && this.IsControllable && this.getEAction().getCost() <= this.getPm() && !cdE)
        {
            E = true;
            ShowActionRange(this.getEAction());
        }
    }


    public void LaunchA(Vector3 destination)
    {
        StartCoroutine(StartCoolDownA());
        this.setPm(this.getPm() - this.getAAction().getCost());
        Attack(this.getAAction(), destination, true);
        AttackAnimation(this.getAAction(), this.m_animator, "A");
        DeleteActionRange();
    }
    public void LaunchZ()
    {

    }
    public void LaunchE(Vector3 destination)
    {
        StartCoroutine(StartCoolDownE());
        this.setPm(this.getPm() - this.getEAction().getCost());
        Attack(this.getEAction(), destination, true);
        AttackAnimation(this.getAAction(), this.m_animator, "A");
        DeleteActionRange();
    }
    public void LaunchR(Vector3 destination)
    {

    }


}
