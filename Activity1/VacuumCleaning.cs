using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity1
{
    internal class VacuumCleaning
    {
    }
    public abstract class Environment
    {
        public abstract void ExecuteAction(Agent agent, object action);
    }
    public abstract class Agent
    {
        public int Performance { get; set; } = 0;
        public abstract object Program(object percept);
    }
    public class VaccuumEnvironment : Environment
    {
        private int[,] grid = new int[2, 2];
        private int agentX = 0;
        private int agentY = 0;
        private Random rand = new Random();

        public VaccuumEnvironment()
        {
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    grid[i, j] = rand.Next(0, 2); // 0 or 1 
                }
            }

            agentX = 0;
            agentY = 0;
        }

        public int[] AgenLoc()
        {
            int[] loc = new int[2] { agentX, agentY };
            return loc;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Grid State: ");
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 2; j++)
                {
                    sb.Append(grid[i, j]);
                }
                sb.AppendLine();
            }

            sb.AppendLine($"Agent Position: ({agentX}, {agentY})");
            return sb.ToString();
        }

        public object Percept(Agent agent)
        {
            bool isDirty = grid[agentX, agentY] == 1;
            return Tuple.Create(agentX, agentY, isDirty);
        }

        public override void ExecuteAction(Agent agent, object action)
        {
            string act = action as string;
            if (act == null)
            {
                agent.Performance -= 1;
            }
            if (act == "Suck")
            {
                if (grid[agentX, agentY] == 1)
                {
                    grid[agentX, agentY] = 0;
                    agent.Performance += 10;
                }
            } 
            else if (act == "Left" && agentX > 0)
            {
                agentX -= 1;
                agent.Performance -= 1;
            }
            else if (act == "Right" && agentX < 1)
            {
                agentX += 1;
                agent.Performance -= 1;
            }
            else if (act == "Down" && agentY > 0)
            {
                agentY -= 1;
                agent.Performance -= 1;
            }
            else if (act == "Up" && agentY < 1)
            {
                agentY += 1;
                agent.Performance -= 1;
            }
            else
            {
                agent.Performance -= 1;
            }
        }
    }

    public class SimpleReflexAgent : Agent
    {
        private readonly Random rand = new Random();
        public override object Program(object percept)
        {
            var tup = percept as Tuple<int, int, bool>;
            if (tup == null) return null;
            bool isDirty = tup.Item3;
            if (isDirty)
            {
                return "Suck";
            }
            string[] choice = {"Up", "Down", "Left", "Right"};
            return choice[rand.Next(choice.Length)];
        }
    }
}
