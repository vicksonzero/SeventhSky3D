using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BWeaponLineMachineGun : BWeapons {


    [Header("Specific")]
    [Tooltip("We will use the WIDTH of this crosshair sprite to randomize the bullet shooting direction noise")]
    public Image crosshair;
    public RectTransform crosshairCanvas;
    [Tooltip("must be a child of the shooter. the bullet will be created and shot at the angle of this transform")]
    public Transform bulletSpawnVector;

    public BLineBullet bullet;
    public Transform firework;

    public void Start()
    {
        base.Start();
        print("weapon line machine gun start");
        
    }
    public override void doShoot()
    {
        print("line machine gun doshoot");
        //print(this.crosshair.rectTransform.rect.width);
        print(this.crosshairCanvas.localScale);
        Vector2 shootPoint = Random.insideUnitCircle * this.crosshair.rectTransform.rect.width/2;//s(pixelWidth, pixelHeight).
        
        float xx = Screen.width / 2;
        float yy = Screen.height / 2;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(xx + shootPoint.x, yy + shootPoint.y, 0));
        //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.yellow);

        Vector3 hitpoint = ray.GetPoint(Camera.main.farClipPlane);

        RaycastHit hitInfo;
        int layerMask = 1 << 10;// enemy's layer
        bool isHitEnemy = Physics.Raycast(ray, out hitInfo, Camera.main.farClipPlane, layerMask);
        if (isHitEnemy)
        {
            hitpoint = hitInfo.point;
            hitInfo.collider.GetComponentInParent<BEnemy>().takeDamage(this.damage);
            Instantiate(this.firework, hitpoint, Quaternion.identity);
        }

        this.bullet.Shoot(hitpoint);
    }

}
