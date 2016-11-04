using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BAnalyticsGA : MonoBehaviour
{


    [Header("Fill in")]
    public bool verbose = false;

    public GoogleAnalyticsV4 googleAnalytics;

    [Header("For inspection only")]

    public static BAnalyticsGA i;

    public enum gaCustomDimensionLabel { none, UseAutoHeal, PlayTime };
    public enum gaCustomMetricLabel { none, PlayerHP, PlayTime, PercentMoving, HealedAmount };

    public static float healedAmount = 0;


    // TODO: move to enemy kill counters
    private Dictionary<string, int> killCount = new Dictionary<string, int>();

    void Awake()
    {
        if (!i)
        {
            i = this;
            //DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(googleAnalytics.gameObject);
        }
        else
        {
            //Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start()
    {
        print("GA onStart");
        this.googleAnalytics.DispatchHits();
        this.googleAnalytics.StartSession();
        print("GA onStart completes");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnApplicationQuit()
    {
        this.googleAnalytics.StopSession();
    }

    void OnApplicationFocus(bool hasFocus)
    {
        var isPaused = !hasFocus;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        var isPaused = pauseStatus;
    }

    public static void logGameStart()
    {
        if (i.verbose)
        {
            print("logGameStart");
        }
        i.googleAnalytics.LogScreen(new AppViewHitBuilder()
            .SetScreenName("Demo time attack")
        );
        string useAutoHeal = PlayerPrefs.GetInt("useAutoHeal", 0) == 1 ? "WithAutoHeal" : "WithoutAutoHeal";
        i.googleAnalytics.LogEvent(new EventHitBuilder()
            .SetEventCategory("Game lifecycle")
            .SetEventLabel("Demo time attack")
            .SetEventAction("Start")
            .SetCustomDimension((int)gaCustomDimensionLabel.UseAutoHeal, useAutoHeal)
        );
    }

    public static void logGameEnd(bool isWin)
    {
        if (i.verbose)
        {
            print("logGameEnd");
        }
        var gameMaster = GameObject.FindObjectOfType<BGameMaster>();
        i.googleAnalytics.LogEvent(new EventHitBuilder()
            .SetEventCategory("Game lifecycle")
            .SetEventLabel("Demo time attack")
            .SetEventAction("End (" + (isWin ? "WIN" : "LOSE") + ")")
            .SetCustomMetric((int)gaCustomMetricLabel.PlayerHP, "" + (gameMaster.player.getHPPercent() * 100))
            .SetCustomMetric((int)gaCustomMetricLabel.PlayTime, "" + ((int)gameMaster.gameTimeTimer.readTimer()))
            .SetCustomMetric((int)gaCustomMetricLabel.HealedAmount, "" + ((int)healedAmount))
        );
    }

    public static void logKill(string enemyType)
    {
        // count enemies
        int count = 0;
        i.killCount.TryGetValue(enemyType, out count);
        count++;
        i.killCount[enemyType] = count;

        if (i.verbose)
        {
            print("logKill(" + enemyType + "): " + count + "; wait for final");
        }

        //i.googleAnalytics.LogEvent(new EventHitBuilder()
        //    .SetEventCategory("Combat")
        //    .SetEventAction("KilledEnemy")
        //    .SetEventLabel(enemyType)
        //    .SetEventValue(count)
        //);
    }

    public static void logFinalKills()
    {
        if (i.verbose)
        {
            print("logFinalKills()");
        }
        foreach (var item in i.killCount)
        {
            i.googleAnalytics.LogEvent(new EventHitBuilder()
                .SetEventCategory("Combat")
                .SetEventAction("KilledEnemy")
                .SetEventLabel(item.Key)
                .SetEventValue(item.Value)
            );
        }
    }

    public static void logUseShield()
    {
        if (i.verbose)
        {
            print("logUseShield()");
        }

        i.googleAnalytics.LogEvent(new EventHitBuilder()
            .SetEventCategory("Combat")
            .SetEventAction("Shielded something")
            .SetEventValue(1)
        );
    }

    public static void logSkipTutorial()
    {
        if (i.verbose)
        {
            print("logSkipTutorial()");
        }
        i.googleAnalytics.LogEvent(new EventHitBuilder()
            .SetEventCategory("Tutorial")
            .SetEventAction("Skip")
            .SetEventValue(1)
        );
    }

    //public static void logReadManualTime(float time)
    //{
    //    if (i.verbose)
    //    {
    //        print("logReadManualTime: " + time);
    //    }
    //    i.googleAnalytics.LogTiming(new TimingHitBuilder()
    //        .SetTimingCategory("Read manual")
    //        .SetTimingName("Before game")
    //        .SetTimingInterval((long)(time * 1000))
    //        );
    //}

    public static void logKeepWeaponTime(string weaponName, float time)
    {
        if (i.verbose)
        {
            print("logKeepWeaponTime(" + weaponName + "): " + time);
        }
        i.googleAnalytics.LogTiming(new TimingHitBuilder()
            .SetTimingCategory("Keep weapon")
            .SetTimingName(weaponName)
            .SetTimingInterval((long)(time * 1000))
            );
    }

    public static void logWeaponKillCounts(string weaponName, long count)
    {
        if (i.verbose)
        {
            print("logWeaponKillCounts(" + weaponName + "): " + count);
        }
        i.googleAnalytics.LogTiming(new TimingHitBuilder()
            .SetTimingCategory("Weapon kills")
            .SetTimingName(weaponName)
            .SetTimingInterval((long)(count))
            );
    }

    public static void logWeaponSwitchCounts(string weaponName, long count)
    {
        if (i.verbose)
        {
            print("logWeaponSwitchCounts(" + weaponName + "): " + count);
        }
        i.googleAnalytics.LogTiming(new TimingHitBuilder()
            .SetTimingCategory("Weapon selected")
            .SetTimingName(weaponName)
            .SetTimingInterval((long)(count))
            );
    }

}
