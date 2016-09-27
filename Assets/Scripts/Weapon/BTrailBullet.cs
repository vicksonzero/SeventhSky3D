using UnityEngine;
using System.Collections;
using System.Linq;
using System;

[RequireComponent(typeof(LineRenderer))]
public class BTrailBullet : MonoBehaviour{

    private LineRenderer trail;

    private float alpha = 1;

    public Color lineColor = new Color();

    private Color finalColor;

    private float timeCreated;
    private float timeToLive = 0;

    private Vector3[] positions;
    private float maxLength;

    private float bulletSpeed = 0.001f;

    void Start()
    {
        this.finalColor = this.getFadedColor(this.lineColor);
        this.finalColor.a = 0.1f;
        this.trail = this.GetComponent<LineRenderer>();
    }

    void Update()
    {
        // fade out
        if (this.timeToLive > 0)
        {
            var progress = ((Time.time - this.timeCreated) / this.timeToLive);
            this.lineColor = Color.Lerp(this.lineColor, this.finalColor, progress);
            //this.trail.SetColors(this.getFadedColor(this.lineColor), this.lineColor);
            //this.trail.SetColors(this.lineColor, this.lineColor);

            var directionVector = this.positions[1] - this.positions[0];
            // trail tail
            //print(string.Join(",", this.positions.Select(p => p.ToString()).ToArray()));
            //print(this.maxLength * (1 - progress));
            this.positions[0] = Vector3.Lerp(this.positions[0], this.positions[1], Time.deltaTime * this.bulletSpeed);

            //this.positions[1] - Vector3.ClampMagnitude(directionVector, bulletSpeed);
            //print(this.positions[0]);
            // trail tail
            this.trail.SetPositions(this.positions);
        }

    }

    private Color getFadedColor(Color c)
    {
        return new Color(c.r, c.g, c.b, Mathf.Max(c.a - 0.3f, 0));
    }
    
    public void setFromTo(Vector3 from, Vector3 to)
    {
        this.positions = new Vector3[] { from, to };
        this.maxLength = (this.positions[1] - this.positions[0]).magnitude;
    }

    public void setDuration(float duration)
    {
        this.timeCreated = Time.time;
        this.timeToLive = duration;
        Destroy(this.gameObject, duration);
    }
}
