using System;

class Node
{
    public string Id { get; set; }
    public Dictionary<string, Node> Transitions { get; set; } // <input, next state>
    public bool isFinal;

    public Node(string id, Dictionary<string, Node> transitions, bool final)
    {
        Id = id;
        Transitions = transitions;
        isFinal = final;
    }

    public String toString()
    {
        return "Id: " + Id + " isFinal: " + isFinal + " Transitions: " + transitionsString();
    }

    private String transitionsString()
    {
        String str = "";
        foreach (var kvp in Transitions)
        {
            str = str + "Token = " + kvp.Key + " Transition To " + kvp.Value.Id;
        }
        return str;
    }


}