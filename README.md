# A* Path Finding in 2D Maps

A Windows Forms application that visualizes classic graph search algorithms on interactive 2D maps. Build or load a graph of nodes and edges, set start and goal locations, then compare how different search strategies explore the map and find paths.

## Features

- **Five search algorithms**: Depth-First Search (DFS), Breadth-First Search (BFS), Uniform Cost Search (UC), A* Search, and Greedy Best-First Search
- **Interactive graph visualization**: Nodes are drawn as circles with labeled edges; discovered paths are highlighted in green
- **Multiple map modes**: Predefined static map, randomly generated map, or fully custom user-built graph
- **Path cost display**: Shows the total cost of the path found by the selected algorithm
- **Node inspection**: Right-click any node to view its heuristic value (`H`) and edge costs

## Requirements

- **OS**: Windows (Windows Forms application)
- **.NET Framework**: 4.7.2 or later
- **IDE** (optional): Visual Studio 2017+ with the .NET desktop development workload

## Quick Start

### Run the prebuilt executable

A compiled binary is available at the repository root:

```
Project.exe
```

Double-click to launch the application on Windows.

### Build from source

1. Open `sorce code/A Path Finding in 2D maps Project.sln` in Visual Studio.
2. Set the build configuration to **Debug** or **Release**.
3. Build the solution (**Ctrl+Shift+B**).
4. Run the application (**F5**).

The output executable is written to `sorce code/bin/Debug/WindowsFormsApp29.exe` (or `bin/Release/` for release builds).

## Usage Overview

1. **Load a map** — Press **S** for the built-in static map, **R** for a random map, or **Q** to enter manual graph-building mode.
2. **Set start and goal** — Left-click nodes to assign:
   - First click: **Start** (`s`)
   - Second click: **Goal** (`g`)
   - Third click (optional): **Midpoint** (`m`) — search stops here instead of the goal
3. **Run a search** — Press a key to run an algorithm (see table below).
4. **Inspect results** — The found path is drawn in green; total path cost appears at the top of the window.

For a full list of keyboard shortcuts and map modes, see [docs/USER_GUIDE.md](docs/USER_GUIDE.md).

## Keyboard Shortcuts

| Key | Action |
|-----|--------|
| **S** | Load the predefined static map |
| **R** | Generate a random map |
| **Q** | Enter manual graph-building mode |
| **Z** | Exit manual graph-building mode |
| **Y** | Select start/goal on an existing manual map |
| **K** | Randomly assign start and goal on the current map |
| **D** | Run Depth-First Search (DFS) |
| **B** | Run Breadth-First Search (BFS) |
| **U** | Run Uniform Cost Search (UC) |
| **A** | Run A* Search |
| **G** | Run Greedy Best-First Search |

**Mouse controls**

| Action | Effect |
|--------|--------|
| Left-click node | Assign start, goal, or midpoint (in selection mode) |
| Right-click node | Show heuristic and edge costs |
| Left-click (manual mode) | Place a new node |
| Right-click pairs (manual mode) | Connect two nodes with a weighted edge |

## Search Algorithms

| Algorithm | Key | Strategy | Optimal? |
|-----------|-----|----------|----------|
| DFS | D | Explores as deep as possible before backtracking | No |
| BFS | B | Explores all neighbors at the current depth before going deeper | Yes (unweighted graphs) |
| Uniform Cost | U | Always expands the lowest-cost path so far | Yes |
| A* | A | Expands nodes with lowest `f = g + h` (cost + heuristic) | Yes (with admissible heuristic) |
| Greedy | G | Expands nodes with lowest heuristic `h` only | No |

Paths are visualized by drawing green edges between nodes on the solution path. The total path cost is summed from edge weights along the reconstructed route.

## Project Structure

```
.
├── Project.exe                          # Prebuilt Windows executable
├── README.md                            # This file
├── docs/
│   └── USER_GUIDE.md                    # Detailed usage guide
└── sorce code/
    ├── A Path Finding in 2D maps Project.sln
    ├── A Path Finding in 2D maps Project.csproj
    ├── Program.cs                       # Application entry point
    ├── Form1.cs                         # Main UI, graph logic, and search algorithms
    ├── Form1.Designer.cs                # Windows Forms designer code
    └── Properties/                      # Assembly info and resources
```

## Graph Model

Each node (`state`) in the graph has:

- **Position** (`X`, `Y`) — screen coordinates for rendering
- **ID** (`id`) — character label (`s` = start, `g` = goal, `m` = midpoint)
- **Heuristic** (`h`) — estimated cost to the goal (used by A* and Greedy)
- **Children** — adjacent nodes (undirected edges)
- **Cost** — dictionary mapping each neighbor to the edge weight

Search algorithms maintain open and closed lists, track parent pointers for path reconstruction, and highlight the resulting path on the canvas.

## License

No license file is included in this repository. Contact the project owner for usage terms.
