using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Psycho : Character
{

    //Le psycho est un dps 
    //Il auto au CaC (passif 0% : 35 passif max : 59)
    //Son A lui permet de lancer sa buzz Axe (passif 0% : 40 passif max : 102)
    //Son Z boost sa vitesse et heal
    //Son E de faire un dash
    //Passif : a chaque fois que psycho perd des pv , il augmente son attaque de 1% (cumul jusqu'a 70%) 


    private int auto_dammage;
    private int a_dammage;


    private float psychoPassif;
    private float boostSpeed;
    private float boostAtt;
    private int tmp_pv;


    [SerializeField] GameObject ZEffectPos;
    [SerializeField] GameObject ZEffect;
    [SerializeField] GameObject a_BuzzAxe;

    public Psycho() : base("psycho", 245, 95 , 7.5f)
    {
    }

    new public void Start()
    {
        base.Start();
        this.InitialisationStat(0, 0, 0, 0); //Init base statistiques
        this.InitialisationRegen(1, 2); //Init pv and pm regen
        this.InitialisationDeplacement(); //Init movement
        this.InitialisationGUI("Characters/Psycho/"); //Init GUI

        #region Action init
        //Chargement des actions
        Action AutoAttaque = new Action("coup de buzz axe", 35, 0, 10, .5f, "Vous infligez un coup avec votre buzz axe.", .7f);

        Action A = new Action("Lancer de buzz axe ", 60, 10, 25f, 3f, "Vous lancez votre buzz axe.", .7f, Action.TYPE_OF_ACTION.dammage, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.projectile);
        A.setProjectilePath("Characters/Psycho/Assets/BuzzAxeNew");
        A.setProjectileSpeed(30f);
        A.setProjectileRotation(true);
        //Load A projectile Assets
        A.setStartHeightPos(transform.position.y + 5f);
        A.setProjectile(a_BuzzAxe);

        Action Z = new Action("Délire psychodélique", 25, 25, 0f, 15f, "Vous entrez dans une rage qui vous fait oubliez la douleur, vous regagnez 20% de votre santé max et augmenter votre vitesse de 25% pour 5secondes", .1f, Action.TYPE_OF_ACTION.speed_up, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.self);
        Z.setDuree(5);

        Action E = new Action("Roulade", 0, 10, 15f, 5f, "Vous effectuez une roulade.", 1f, Action.TYPE_OF_ACTION.dash, Action.ACTION_EFFECT.none, 0f, Action.ACTION_CIBLE.dash);
        E.setDashSpeed(20f);
        #endregion

        this.InitialisationAction(AutoAttaque, A, Z, E, null);

        //Passif
        psychoPassif = 1f;
        tmp_pv = this.getPv();
        auto_dammage = AutoAttaque.getDammage();
        a_dammage = A.getDammage();

    }


    new public void Update() 
    {
        //Appel de l'update de Character
        base.Update();
        //Si le perso est en pleine action on ne l'interompt pas
        if (this.Animated)
            return;
        //Recuperation du controlleur d'animation
        Animator anim = this.gameObject.GetComponentInChildren<Animator>();
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
                //Si le clic a touché le sol 
                if(hit.collider.gameObject.tag=="Ground")
                {

                    if (A && Vector3.Distance(this.transform.position, hit.point) < this.getAAction().getRange() && this.getAAction().getCible()!= Action.ACTION_CIBLE.cible)
                    {
                        LaunchA(hit.point, anim);
                    }
                    if (E && Vector3.Distance(this.transform.position, hit.point) < this.getEAction().getRange() && this.getEAction().getCible() != Action.ACTION_CIBLE.cible)
                    {
                        LaunchE(hit.point, anim);
                    }
                }
                //Si le clic a touché un character
                else 
                {
                    GameObject go = hit.collider.gameObject;
                    Character character = go.GetComponent<Character>();
                    if (go!=null && go != gameObject && character)
                    {
                        if (A == false && E == false)
                        {
                            //On regarde si l'auto action peut atteindre la cible 
                            if (Vector3.Distance(this.transform.position, go.transform.position) <= this.getAutoAction().getRange())
                            {
                                //lancement de l'auto attaque
                                Attack(character, this.getAutoAction());
                                AttackAnimation(this.getAutoAction(), anim, "Auto");
                            }
                        }
                        else
                        {
                            if (A && Vector3.Distance(this.transform.position, go.transform.position) < this.getAAction().getRange())
                            {
                                LaunchA(go.transform.position, anim);
                            }
                            if (E && Vector3.Distance(this.transform.position, go.transform.position) < this.getEAction().getRange())
                            {
                                LaunchE(go.transform.position, anim);
                            }
                        }
                    }
                }                
            }
        }
        //Lecture des inputs
        if (Input.GetButton("A") && !A && this.IsControllable && this.getAAction().getCost()<= this.getPm() && !cdA && TimeA <= 0)
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
        
        //Proc du passif du perso 
        if(tmp_pv!=this.getPv())
        {
            //Debug.Log("Les pv du Psycho ont changés");
            if(tmp_pv > this.getPv())
            {
                //Debug.Log("Les pv du psycho ont baissé");
                Passif();
            }
            tmp_pv = this.getPv();
        }

       
    }


    private void LaunchA(Vector3 destination , Animator anim)
    {
        StartCoroutine(StartCoolDownA());
        this.setPm(this.getPm() - this.getAAction().getCost());
        Attack(this.getAAction(), destination, true);
        AttackAnimation(this.getAAction(), anim, "A");
        DeleteActionRange();
    }

    private void LaunchZ()
    {
        //Debug.Log("Launch Z");
        StartCoroutine(StartCoolDownZ());
        this.setPm(this.getPm() - this.getZAction().getCost());
        //Heal
        int tmp_pv =(int) this.getPv() + ((this.m_pv_max / 100) * 20);
        if (tmp_pv > m_pv_max)
            tmp_pv = m_pv_max;
        this.setPv(tmp_pv);
        //SpeedUp
        //Debug.Log("Augmentation de + " + ((float)this.getZAction().getDammage() / 100) + "%");
        StartCoroutine(ZSelfBuff());

        //GameObject Zeffect = Instantiate(Resources.Load("Characters/Psycho/Assets/Zeffect")) as GameObject;
        GameObject Zeffect = Instantiate(ZEffect);
        
        Zeffect.transform.parent = ZEffectPos.transform;
        Zeffect.transform.localPosition = ZEffectPos.transform.localPosition;
        Zeffect.transform.localScale = Zeffect.transform.localScale * 2;
        Destroy(Zeffect, 5f);
    }

    IEnumerator ZSelfBuff()
    {
        float boost = (float)(1.5 + (float)this.getZAction().getDammage() / 100);
        this.setSpeed(this.getSpeed() * boost);
        yield return new WaitForSeconds(this.getZAction().getDuree());
        this.setSpeed(this.getSpeed() / boost);

    }

    private void LaunchE(Vector3 destination , Animator anim)
    {
        StartCoroutine(StartCoolDownE());
        this.setPm(this.getPm() - this.getEAction().getCost());
        Attack(this.getEAction(),destination);
        AttackAnimation(this.getEAction(), anim, "E");
        DeleteActionRange();
    }

    private void LaunchAuto()
    {

    }

    private void Passif()
    {
        if (psychoPassif >= 1.70f)
            return;
        psychoPassif += 0.01f;
        this.getAutoAction().setDammage((int)(auto_dammage*psychoPassif));
        //Debug.Log("auto :" +this.getAutoAction().getDammage());
        this.getAAction().setDammage((int)(a_dammage*psychoPassif));
        //Debug.Log("A : " + this.getAAction().getDammage());
    }





}
