using System;

class Dfa
{
    private Node head = new Node("", new Dictionary<string, Node>(), false);
    private Dictionary<String, Node> states = new Dictionary<string, Node>(); // Need to keep track of created nodes


    public Dfa(string representation)
    {
        build(representation);
    }

    public bool isValid(String input)
    {
        char[] characters = input.ToCharArray();
        Node cur = head;

        for (int i = 0; i < input.Length; i++)
        {
            string character = characters[i].ToString();
            // does a transition exist?
            if (cur.Transitions.ContainsKey(character))
            {
                cur = cur.Transitions[character];
            }
            else
            {
                return false;
            }
        }
        // At the end of the input. Are we in an accept state?
        return cur.isFinal;
    }

    private void build(string rep)
    {
        // Example: s0*(s0:0, s1:1), s1(s0:1, s2:0), s2(s1:0, s2:1)

        // split state definitions,  s0*(s0:0, s1:1)
        String[] defs = rep.Split("),");

        // Allocate the nodes first
        setupStates(defs);

        // Go back and assign edges
        assignEdges(defs);

    }

    private void setupStates(String[] defs)
    {
        foreach (String def in defs)
        {
            String id = def.Split("(")[0];
            bool isFinal = false;

            // is this a final state?
            if (id.Contains("*"))
            {
                isFinal = true;
                id = id.Remove(id.Length - 1);
            }

            // keep track of this node 
            Node newNode = new Node(id, new Dictionary<string, Node>(), isFinal);
            if (head.Id == "")
            {
                head = newNode;
            }
            states.Add(id, newNode);
        }
    }

    private void assignEdges(String[] defs)
    {
        foreach (String def in defs)
        {
            String[] split = def.Split("(");
            String id = split[0];
            if (id.Contains("*"))
            {
                id = id.Remove(id.Length - 1);
            }
            Node curState = states[id];
            String edgesStr = split[1];

            // s2:x, s3:x, ....
            String[] edges = edgesStr.Split(",");

            // Add edges to the nodes using the nodes we already allocated
            foreach (String edge in edges)
            {
                // s2:x
                String[] splitEdge = edge.Split(":");
                String toStateStr = splitEdge[0];
                String token = splitEdge[1];
                // add the edge
                curState.Transitions.Add(token, states[toStateStr]);
            }
        }
    }

    // Debug
    private void printStateDict()
    {
        foreach (var kvp in states)
        {
            Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value.toString());
        }
    }
}