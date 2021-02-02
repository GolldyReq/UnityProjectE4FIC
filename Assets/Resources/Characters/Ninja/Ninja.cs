using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Character
{
    //Personnage ninja, rapide , faible degats mais grande resistance 
    //A : lancer de shurikan 
    //Z : Double la vitesse pendant 1seconde
    //E : Boule de feu (25% de chance d'enflammer la cible)
    //R : Téléportation


    [SerializeField] GameObject m_shuriken;
    [SerializeField] GameObject m_fireball;

    public Ninja() : base("Ninja", 150, 500, 9f)
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
        Auto = new Action("coup de pied", 15, 0, 10, .5f, "Vous infligez un coup de pied.", .7f);
        A = new Action("Shuriken", 25, 10, 25, 3f, "Envoie d'un shuriken ! ", 1f, Action.TYPE_OF_ACTION.dammage, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.projectile);
        //Load A projectile Assets
        A.setProjectilePath("Characters/Ninja/Action/Shuriken/Shuriken");
        A.setProjectileSpeed(20);
        A.setProjectileRotation(true);
        A.setStartHeightPos(transform.position.y + 5f);
        A.setProjectile(m_shuriken);
        A.setVisualEffect("Characters/Ninja/Action/Shuriken/OnHitEffect");

        Z = new Action("Shadow Speed" , 25, 15, 0f, 6f, "Augmente votre vitesse de déplacement" , .1f, Action.TYPE_OF_ACTION.speed_up, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.self);
        Z.setDuree(1);

        string Edesc = "Envoie d'une boule de feu ! 25% de chance d'enflammer la cible pendant 10 secondes ";
        E = new Action("Fireball", 30, 20, 20, 12f, Edesc, 1f, Action.TYPE_OF_ACTION.dammage, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.projectile);
        //Load A projectile Assets
        E.setProjectilePath("Characters/Ninja/Action/Fireball/Fireball");
        E.setProjectileSpeed(20);
        E.setProjectileRotation(false);
        E.setStartHeightPos(transform.position.y + 5f);
        E.setProjectile(m_fireball);
        E.setReorientation(true);
        E.setProjectileEffect(Action.ACTION_EFFECT.ignite, 75, 5, 15) ;


        R = new Action("Teleportation", 0, 100, 50, 75f, "Le ninja disparaît et réapparaît aussitôt comme s'il s'était téléporté ! ", .1f, Action.TYPE_OF_ACTION.dash, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.self); 
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
                Character cible = hit.collider.gameObject.GetComponent<Character>();
                if (hit.collider.gameObject.GetComponent<Character>() != null)
                    destination = hit.collider.gameObject.transform.position;
                //Auto attaque
                if (Vector3.Distance(this.transform.position, destination) <= this.getAutoAction().getRange() && cible != null && cible != this)
                {
                    //lancement de l'auto attaque
                    Attack(cible, this.getAutoAction());
                    AttackAnimation(this.getAutoAction(), this.m_animator, "Auto");
                }
                // A competence
                if (A && Vector3.Distance(this.transform.position, destination) < this.getAAction().getRange())
                {
                    LaunchA(destination);
                }
                // E competence
                if (E && Vector3.Distance(this.transform.position, destination) < this.getEAction().getRange())
                {
                    LaunchE(destination);
                }
                //R competence
                if( R && Vector3.Distance(this.transform.position , destination) < this.getRAction().getRange() && hit.collider.gameObject.tag == "Ground")
                {
                    LaunchR(destination);
                }
            }
        }
        //test
        transform.GetChild(0).gameObject.transform.localPosition = new Vector3(0, 0, 0);

        //Lecture des inputs
        if (Input.GetButton("A") && !A && this.IsControllable && this.getAAction().getCost() <= this.getPm() && !cdA && TimeA<=0)
        {
            A = true;
            ShowActionRange(this.getAAction());
        }
        if (Input.GetButton("Z") && !Z && this.IsControllable && this.getZAction().getCost() <= this.getPm() && !cdZ && TimeZ <= 0)
        {
            Z = true;
            LaunchZ();
        }
        if (Input.GetButton("E") && !E && this.IsControllable && this.getEAction().getCost() <= this.getPm() && !cdE && TimeE <= 0)
        {
            E = true;
            ShowActionRange(this.getEAction());
        }
        if (Input.GetButton("R") && !R && this.IsControllable && this.getRAction().getCost() <= this.getPm() && !cdR && TimeR <= 0)
        {   
            R = true;
            ShowActionRange(this.getRAction());
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
        //Debug.Log("Launch Z");
        StartCoroutine(StartCoolDownZ());
        this.setPm(this.getPm() - this.getZAction().getCost());
        //SpeedUp
        //Debug.Log("Augmentation de + " + ((float)this.getZAction().getDammage() / 100) + "%");
        StartCoroutine(ZSelfBuff());
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
        StartCoroutine(StartCoolDownR());
        this.setPm(this.getPm() - this.getRAction().getCost());
        transform.LookAt(destination);
        this.m_newPosition = destination;
        this.transform .position = destination;
        if(nav !=null)
        {
            try
            {
                nav.SetDestination(destination);
            }catch(Exception e) { }
        }
        DeleteActionRange();

    }

    IEnumerator ZSelfBuff()
    {
        this.setSpeed(this.getSpeed() * 2);
        yield return new WaitForSeconds(this.getZAction().getDuree());
        this.setSpeed(this.getSpeed() / 2);

    }


}
