# Unity Slot Machine Game

A simple slot machine game developed in Unity featuring animated reels, weighted random symbol selection, betting mechanics, and payout logic.

---

# Game Overview

This project is a playable slot machine game built using Unity and C#. The player places a bet, pulls the lever, and watches the reels spin. A jackpot is awarded when all reels land on the same symbol.

The project focuses on:
- Smooth reel animations
- Randomized outcomes using weighted RNG
- Clean symbol alignment
- Object-Oriented Programming principles
- UI interaction and betting systems

---

# Gameplay Features

- 🎰 Animated spinning reels
- 🎲 Weighted random symbol generation
- 💰 Betting and payout system
- 🏆 Jackpot win detection
- 🖥️ UI for bets and bank balance
- 🔒 Bet locking during active spins
- 🎚️ Reel snapping/alignment system

---

# Running the WebGL Build

## Option 1 — Run Locally Through Browser
1. Open the project repository.
2. Navigate to:
Build/

3. Run the `run.bat` file if on windows
Otherwise host the folder using a local web server.

Example using Python:
python -m http.server 8000

4. Open your browser and go to:
http://localhost:8000

---

## Option 2 — Open in Unity
1. Open Unity Hub.
2. Add the project folder.
3. Open the project using the correct Unity version.
4. Open the main scene.
5. Press Play.

---

# Bonus Features

The project includes several additional mechanics beyond the minimum requirements:

- Weighted symbol probabilities
- Dynamic betting system
- Lever pull animation
- Reel snapping system
- UI state locking during active gameplay
- Adjustable spin timing per symbol

---

# Thought Process / Approach

The project was designed with modularity and readability in mind using Object-Oriented Programming principles.

The architecture separates gameplay responsibilities into different systems:

- `GameManager` handles game state, money, and payouts
- `SlotMachine` controls reel spinning and result generation
- `Reel` manages reel movement and symbol alignment
- `Symbol` stores symbol-related data
- `BetManager` handles betting UI and player interaction

Weighted random number generation was implemented using cumulative probability selection to ensure fair and configurable outcomes.

Reel animations were implemented using Unity Coroutines and `Time.deltaTime` for smooth frame-independent movement.

A snapping system was created to ensure symbols always align cleanly to fixed reel slots after spinning.

---

# Controls

- Up and Down Arrows: Navigate between available bets
- Enter: Select a bet
- Lever: Left-Click to spin the slot machine

---
