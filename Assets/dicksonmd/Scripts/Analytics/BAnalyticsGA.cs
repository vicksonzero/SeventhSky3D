using UnityEngine;
using System.Collections;
using System.Net;
using System.Collections.Specialized;
using System.Text;
using UnityEngine.SceneManagement;

public class BAnalyticsGA : MonoBehaviour
{


    [Header("Fill in")]
    public bool verbose = false;

    public GoogleAnalyticsV4 googleAnalytics;

    [Header("For inspection only")]

    public static BAnalyticsGA i;

    public enum gaCustomMetricLabel { none, PlayerHP, PlayTime };

    void Awake()
    {
        if (!i)
        {
            i = this;
            DontDestroyOnLoad(gameObject);
            //DontDestroyOnLoad(googleAnalytics.gameObject);
        }
        else
        {
            Destroy(gameObject);
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

    public static void logGameStart()
    {
        if (i.verbose)
        {
            print("logGameStart");
        }
        i.googleAnalytics.LogScreen(new AppViewHitBuilder()
            .SetScreenName("Demo time attack")
        );
        i.googleAnalytics.LogEvent(new EventHitBuilder()
            .SetEventCategory("Game lifecycle")
            .SetEventLabel("Demo time attack")
            .SetEventAction("Start")
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
        );
    }

    public static void logKill(string enemyType)
    {
        if (i.verbose)
        {
            print("logKill(" + enemyType + ")");
        }
        i.googleAnalytics.LogEvent(new EventHitBuilder()
            .SetEventCategory("Combat")
            .SetEventAction("KilledEnemy")
            .SetEventLabel(enemyType)
        );
    }

    public static void logReadManualTime(float time)
    {
        if (i.verbose)
        {
            print("logReadManualTime: " + time);
        }
        i.googleAnalytics.LogTiming(new TimingHitBuilder()
            .SetTimingCategory("Read manual")
            .SetTimingName("Before game")
            .SetTimingInterval((long)(time * 1000))
            );
    }

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
