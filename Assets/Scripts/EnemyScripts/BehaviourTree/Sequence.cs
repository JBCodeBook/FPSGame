using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    private int currentNodeIndex = 0;

    protected List<Node> nodes = new List<Node>();

    public Sequence(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        if (currentNodeIndex < nodes.Count)
        {
            _nodeState = nodes[currentNodeIndex].Evaluate();

            if (_nodeState == NodeState.RUNNING)
                return NodeState.RUNNING;

            else if (_nodeState == NodeState.FAILURE)
            {
                currentNodeIndex = 0;
                return NodeState.FAILURE;
            }
            else
            {
                currentNodeIndex++;
                if (currentNodeIndex < nodes.Count)
                    return NodeState.RUNNING;
                else
                {
                    currentNodeIndex = 0;
                    return NodeState.SUCCESS;
                }
            }
        }
        return NodeState.SUCCESS;
    }


    /*    public override NodeState Evaluate()
        {
            bool isAnyNodeRunning = false;
            foreach (var node in nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.RUNNING:
                        isAnyNodeRunning = true;
                        break;
                    case NodeState.SUCCESS:
                        break;
                    case NodeState.FAILURE:
                        _nodeState = NodeState.FAILURE;
                        return _nodeState;
                    default:
                        break;
                }
            }
            _nodeState = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return _nodeState;
        }*/
}
