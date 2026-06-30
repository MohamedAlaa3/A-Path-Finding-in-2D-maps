# User Guide

This guide explains how to use the **A* Path Finding in 2D Maps** application step by step.

## Getting Started

When the application launches, the window opens maximized with a black canvas. No map is loaded by default — choose a map mode to begin.

### Recommended first run

1. Press **S** to load the built-in static map (10 nodes with predefined edges and costs).
2. The start (`s`) and goal (`g`) nodes are already assigned.
3. Press **A** to run A* Search.
4. Observe the green path drawn between nodes and the path cost displayed at the top.

## Map Modes

### Static map (S)

Loads a fixed graph with 10 labeled nodes (`s`, `a`–`j`, `g`, `m`). Edge weights and heuristic values are predefined. Start and goal are pre-assigned, so you can run a search immediately.

Use this mode to compare algorithms on a consistent graph.

### Random map (R)

Generates a new graph with 8–12 randomly placed nodes. Nodes are positioned on a grid; edge weights and heuristics are randomized. After generation:

1. Left-click a node to set **start** (`s`).
2. Left-click another node to set **goal** (`g`).
3. Optionally left-click a third node to set **midpoint** (`m`).

### Manual graph building (Q)

Build your own graph from scratch:

1. Press **Q** to enter manual mode.
2. **Left-click** on the canvas to place nodes. The first click creates the root node; subsequent clicks add more nodes with auto-incrementing labels.
3. **Right-click** two nodes in sequence to connect them. Each pair creates an undirected edge with random weight and heuristic values.
4. Press **Y** to switch to start/goal selection on your custom map.
5. Left-click nodes to assign start, goal, and optional midpoint (same as random map mode).
6. Press **Z** to exit manual mode.

### Random start/goal (K)

On an existing map (without predefined start/goal), press **K** to randomly pick two distinct nodes and assign them as start and goal.

## Running Search Algorithms

Before running any search, ensure both a **start** (`s`) and **goal** (`g`) node exist on the map. If either is missing, a dialog will prompt you to select them.

| Key | Algorithm | Description |
|-----|-----------|-------------|
| **D** | Depth-First Search | Uses a stack (LIFO). Explores one branch fully before backtracking. May not find the shortest or lowest-cost path. |
| **B** | Breadth-First Search | Uses a queue (FIFO). Explores all nodes at the current depth before moving deeper. Finds the shortest path in unweighted graphs. |
| **U** | Uniform Cost Search | Expands the node with the lowest accumulated path cost (`g`). Guarantees the lowest-cost path when edge weights are non-negative. |
| **A** | A* Search | Uses `f(n) = g(n) + h(n) - h(parent)` to prioritize nodes. Combines actual cost with heuristic guidance for efficient optimal search. |
| **G** | Greedy Best-First | Expands the node with the lowest heuristic `h(n)` only. Fast but not guaranteed to find the optimal path. |

After a search completes:

- The algorithm name and **path cost** appear at the top of the window.
- Edges on the solution path are drawn in **green**.
- All other edges remain **yellow**.

Press the same or a different algorithm key to re-run a search. Previous path highlights are cleared automatically.

## Inspecting Nodes

**Right-click** any node to open an information dialog showing:

- **H** — the node's heuristic value
- **Edge costs** — the weight to each connected neighbor

This is useful for understanding why A* or Greedy chose a particular route.

## Visual Legend

| Element | Color / Style | Meaning |
|---------|---------------|---------|
| Node circle | Blue outline | Graph node |
| Node label | Red text | Node ID character |
| Edge | Yellow line | Graph connection |
| Solution path | Green line | Edges on the found path |
| Header text | White | Active algorithm and path cost |

## Tips

- **Compare algorithms**: Load the static map (**S**) and run DFS, BFS, UC, A*, and Greedy in sequence. Compare path costs and which edges each algorithm chooses.
- **Midpoint nodes**: Assign a midpoint (`m`) to stop the search at an intermediate node instead of the goal. Useful for multi-stage routing scenarios.
- **Re-generate**: Press **R** or **S** again to reset the map and start fresh. All nodes and paths are cleared.

## Troubleshooting

| Issue | Solution |
|-------|----------|
| "Please Select the pickup location" | Left-click a node to assign start (`s`) before running a search. |
| "Please Select the destination location" | Left-click a node to assign goal (`g`) before running a search. |
| No path drawn | The algorithm may not have reached the goal. Check that start and goal are connected in the graph. |
| Application won't start on Linux/macOS | This is a Windows Forms (.NET Framework) app and requires Windows. |

## Technical Notes

- The application targets **.NET Framework 4.7.2** and uses **Windows Forms** for rendering.
- Graphs are undirected: connecting two nodes creates edges in both directions with the same weight.
- Rendering uses an off-screen bitmap (`Bitmap`) with double-buffering to reduce flicker during animation.
- The `state` class encapsulates each graph node; search methods (`DFS`, `BFS`, `UC`, `Astar`, `greedy`) are implemented in `Form1.cs`.
