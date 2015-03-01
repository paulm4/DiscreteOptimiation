﻿////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.IO;

////namespace GraphColoring {    

////    class Graph {
////        public HashSet<int> GetIncidentNodes(int u) {
////            return incidentNodesFor[u];
////        }

////        public List<int> GetAllNodeIds() {
////            return incidentNodesFor.Keys.ToList();
////        }

////        public void AddEdge(int u, int v) {
////            if (!incidentNodesFor.ContainsKey(u)) {
////                incidentNodesFor.Add(u, new HashSet<int>());
////                nodeOf.Add(u, new Node(u));
////            }
////            if (!incidentNodesFor.ContainsKey(v)) {
////                incidentNodesFor.Add(v, new HashSet<int>());
////                nodeOf.Add(v, new Node(v));
////            }

////            incidentNodesFor[u].Add(v);
////            incidentNodesFor[v].Add(u);

////            var nodeU = nodeOf[u];
////            var nodeV = nodeOf[v];
////            nodeU.AddIncidentNode(nodeV);
////            nodeV.AddIncidentNode(nodeU);
////        }

////        public HashSet<int> GetExcludedColorsFor(int u) {
////            return nodeOf[u].getExcludedColors();
////        }

////        public void ExcludeColorForAllIncidentNodesIfNotAlreadyColored(int node, int color) {
////            foreach (var v in incidentNodesFor[node]) {
////                if (!nodeOf[v].IsColored()) {
////                    nodeOf[v].ExcludeColor(color);
////                }
////            }
////        }

////        public void SetColorForNode(int node, int color) {
////            nodeOf[node].SetColor(color);
////        }

////        public int GetColorForNode(int node) {
////            return nodeOf[node].GetColor();
////        }

////        public List<int> GetNodesSortedByConnectivity() {
////            var nodeArray = nodeOf.Values.ToArray();
////            Array.Sort(nodeArray, new SortByMaxIncidentNodes());
////            return nodeArray.Select(x => x.GetId()).ToList();
////        }

////        private Dictionary<int, HashSet<int>> incidentNodesFor = new Dictionary<int, HashSet<int>>();
////        private Dictionary<int, Node> nodeOf = new Dictionary<int, Node>();

////        private class Node {
////            public Node(int id) {
////                this.id = id;
////                colored = false;
////            }

////            public int GetId() {
////                return id;
////            }

////            public HashSet<int> getExcludedColors() {
////                return excludedColors;
////            }

////            public bool IsColorExcluded(int color) {
////                return excludedColors.Contains(color);
////            }

////            public void ExcludeColor(int color) {
////                excludedColors.Add(color);
////            }

////            public bool IsColored() {
////                return colored;
////            }

////            public int GetColor() {
////                if (colored) {
////                    return color;
////                } else {
////                    return -1;
////                }
////            }

////            public void SetColor(int color) {
////                this.color = color;
////                colored = true;
////            }

////            public void AddIncidentNode(Node v) {
////                incidentNodes.Add(v.GetId(), v);
////            }

////            public int GetNumberOfIncidentNodes() {
////                return incidentNodes.Count;
////            }

////            private HashSet<int> excludedColors = new HashSet<int>();
////            private readonly int id;
////            private bool colored;
////            private int color;

////            private Dictionary<int, Node> incidentNodes = new Dictionary<int,Node>();
////        }

////        class SortByMaxIncidentNodes : IComparer<Node> {

////            public int Compare(Node x, Node y) {
////                int xIncidentNodes = x.GetNumberOfIncidentNodes();
////                int yIncidentNodes = y.GetNumberOfIncidentNodes();
////                if (xIncidentNodes > yIncidentNodes) return -1;
////                else if (yIncidentNodes > xIncidentNodes) return 1;
////                else return 0;
////            }
////        }
////    }

////    class Program {
////        static void Main(string[] args) {
////            string fName = @"D:\san\Algo\courses\Coursera-DsicreteOptimization PascalVanHentenryck\Lectures\_Assignments\coloring\coloring\data\gc_1000_5";


////            if (args.Length > 0) {
////                fName = args[0];
////                //Console.WriteLine("Input file : " + fName);
////            }

////            Graph graph = ReadGraphFromFile(fName);

////            var nodesInGraph = graph.GetAllNodeIds();
////            int highestAssignedColor = -1;

////            int totalNodes = nodesInGraph.Count;
////            int currProgress = 0;

////            List<int> nodesToProcess = graph.GetNodesSortedByConnectivity();

////            for (int i = 0; i < totalNodes; i++) {

////                int node = nodesToProcess[i];

////                //Console.WriteLine("Processing node : {0} out of {1}", ++currProgress, totalNodes);

////                var excludedColorsForNode = graph.GetExcludedColorsFor(node);

////                int possibleColorForNode = -1;
////                for (int j = 0; j <= highestAssignedColor; j++) {
////                    if (!excludedColorsForNode.Contains(j)) {
////                        possibleColorForNode = j;
////                        break;
////                    }
////                }

////                int colorForNode;
////                if (possibleColorForNode != -1) {
////                    colorForNode = possibleColorForNode;
////                } else {
////                    colorForNode = ++highestAssignedColor;
////                }

////                graph.SetColorForNode(node, colorForNode);
////                graph.ExcludeColorForAllIncidentNodesIfNotAlreadyColored(node, colorForNode);

////            }

////            Console.WriteLine(highestAssignedColor + 1 + " 0");
////            for (int i = 0; i < totalNodes; i++) {
////                Console.Write(graph.GetColorForNode(i) + " ");
////            }
////            Console.WriteLine();

////        }

////        static Graph ReadGraphFromFile(string fName) {
////            Graph g = new Graph();

////            var lines = File.ReadAllLines(fName);
////            var header = lines[0].Split(' ');

////            var numNodes = int.Parse(header[0]);
////            var numEdges = int.Parse(header[1]);

////            for (int i = 1; i <= numEdges; i++) {
////                var uv = lines[i].Split(' ');
////                var u = int.Parse(uv[0]);
////                var v = int.Parse(uv[1]);

////                g.AddEdge(u, v);
////            }

////            return g;
////        }
////    }
////}
