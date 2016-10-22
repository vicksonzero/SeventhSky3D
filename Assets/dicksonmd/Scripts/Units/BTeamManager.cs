using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System;

public class BTeamManager : MonoBehaviour
{

    public string teamName = "none";

    public BTeamManager[] flatChildTeams
    {
        get
        {
            return this.childTeams.SelectMany(team => team.childTeams).ToArray(); ;
        }
    }

    //[NonSerialized]
    public BTeamManager[] childTeams = new BTeamManager[0];

    private List<BUnit> members = new List<BUnit>();

    // events
    public delegate void UnitUpdatedDelegate();
    public event UnitUpdatedDelegate unitUpdated;
    

    public BTeamManager find(string teamName)
    {
        if (this.teamName == teamName) return this;
        if (this.childTeams.Length == 0) return null;
        return this.childTeams.Aggregate((acc, teamManager) =>
        {
            return (acc != null ? acc : teamManager.find(teamName));
        });
    }

    public void addChildTeam(BTeamManager team)
    {
        //this.childTeams = //.Add(team);
        List <BTeamManager> tempList = new List<BTeamManager>(this.childTeams);
        tempList.Add(team);
        // You can convert it back to an array if you would like to
        this.childTeams = tempList.ToArray();
    }

    public void add(BUnit unit)
    {
        this.members.Add(unit);
        this.unitUpdated();
    }

    public void remove(BUnit unit)
    {
        this.members.Remove(unit);
        this.unitUpdated();
    }

    public List<BUnit> getMembers()
    {
        var result = new List<BUnit>(this.members);

        return result;
    }

    public int count()
    {
        return this.members.Count;
    }

    public override string ToString()
    {
        if (this.childTeams.Length == 0) return this.teamName;
        return this.teamName + ": {" + (
            this.childTeams.Select(child => child.ToString())
                .Aggregate((acc, childString) => acc + ", " + childString)
            ) + "}";

    }

}
