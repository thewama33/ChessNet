# ♟️ **Console Chess Game (MVC Architecture)**  

🚀 A **console-based Chess game** written in **C#** using the **Model-View-Controller (MVC) architecture**.  
Supports **Player vs. Player** and **Player vs. Computer (AI)** modes.  

---

## 📌 **Features**
✔ **MVC Architecture** - Clean separation of concerns for better maintainability  
✔ **Console-based UI** - Simple and interactive board display  
✔ **Player vs. Player Mode** - Play against a friend locally  
✔ **Player vs. Computer Mode** - AI opponent with random move selection  
✔ **Basic Chess Movement Rules** - Pieces follow fundamental movement rules  
✔ **Check Detection** - Alerts when a king is in check  
✔ **Turn-based Gameplay** - Enforces turn order for White and Black  
✔ **Exit Anytime** - Type `"exit"` to quit the game  

---

## 🏗️ **Project Structure**
```
ChessGame/
│── Model/         # Handles game logic (board, rules, AI)
│   ├── ChessBoard.cs
│   ├── ChessAI.cs
│── View/          # Manages user interface and display
│   ├── ChessView.cs
│── Controller/    # Controls game flow and player input
│   ├── ChessController.cs
│── Program.cs     # Entry point of the application
│── README.md      # Documentation
```

---

## 🚀 **How to Run**
### **1️⃣ Clone the Repository**
```sh
git clone https://github.com/thewama33/ChessNet.git
cd ConsoleChessGame
```

### **2️⃣ Compile & Run the Project**
If using **.NET CLI**:
```sh
dotnet run
```
If using **Visual Studio**:  
- Open `ChessNet.sln`
- Press `F5` to run

---

## 🎮 **How to Play**
1️⃣ **Select a Game Mode**  
   - `1` → Play with a friend  
   - `2` → Play against the computer  

2️⃣ **Move Pieces**  
   - Enter moves using **Algebraic Notation** (e.g., `e2e4` moves a piece from `e2` to `e4`)  
   - Type `"exit"` to quit  

3️⃣ **Game Rules**  
   - Standard Chess movement rules apply  
   - The game alerts when a King is **in check**  
   - Turns alternate between **White and Black**  

---

## 🔧 **Planned Improvements**
🔹 **Checkmate & Stalemate detection**  
🔹 **Castling & En Passant moves**  
🔹 **Better AI with Minimax Algorithm**  
🔹 **GUI version using WinForms/WPF**  
🔹 **Online Multiplayer with Networking**  

---

## 🤝 **Contributing**
Pull requests and suggestions are **welcome**! Please follow these steps:
1. **Fork** the repository  
2. **Create a new branch** (`feature-new-ai`)  
3. **Commit your changes**  
4. **Push to your fork**  
5. **Create a Pull Request**  

---

## 📜 **License**
This project is **open-source** under the **MIT License**.  

---

## 💬 **Contact & Support**
📩 **Email:** thewama33@gmail.com  
🌐 **GitHub:** [thewama33](https://github.com/thewama33)  

🔹 If you like this project, **⭐ Star** it on GitHub! 🎉♟️