# Othello MVC API Application
This is a backend-driven web application for a multiplayer Othello (Reversi) game built using ASP.NET Core MVC. The application consists of a REST API and an MVC client. The gameplay is developed in vanilla JavaScript and uses a Single Page Application. To populate the server there are bots that make use of minmax algorithms of different diffilculties.

# Table of Contents
- [Project Overview](#ProjectOverview)  
- [Features](#Features)  
- [Technologies](#Technologies)  
- [API Structure](#APIStructure)  
- [Controllers](##Controllers)  
- [Database Access Layer](#DatabaseAccessLayer)  
- [MVC Structure](#MVCStructure)  
- [Controllers](##Controllers)  
- [Game Flow](#GameFlow)  
- [Installation and Setup](#InstallationandSetup)  

## Project Overview
The Othello MVC API project is designed to handle multiplayer Othello gameplay through a robust backend architecture. The application provides essential features for managing players, game sessions, and results. The REST API offers a suite of endpoints to facilitate game creation, player interaction, and game results tracking, while the MVC structure allows seamless interaction between the API and the client application.

https://github.com/user-attachments/assets/a484df8e-e06d-485d-95b0-ae878e4449db

https://github.com/user-attachments/assets/4cb98709-c655-411b-b084-09e658618e3f

https://github.com/user-attachments/assets/92e78dad-711e-4550-8b98-844a36f9f351

https://github.com/user-attachments/assets/6cd1e990-9c56-4d8d-bbef-af94222bda6d

# Features
- User Authentication: Players register and log in through the MVC application using ASP.NET Identity.  
- Game Management: Players can create new Othello games or join existing ones.  
- Game Progress Locking: Players in an ongoing game are redirected to the game page and cannot leave until the game is finished.  
- Real-time Game Updates (Planned): Vanilla JavaScript with AJAX will be used to periodically update the game state.  
- Friend System: Players can send and accept friend requests and invite friends to join games.  
- Result Tracking: Match results, including wins, losses, and draws, are stored in the database and can be viewed via the player’s profile.  

# Technologies
- ASP.NET Core MVC: For the web framework and MVC client.
- Entity Framework Core: Used for data access and database management.
- ASP.NET Identity: Handles user authentication and authorization.
- MS SQL Server: The database system for storing game, player, and result data.
- REST API: The backend API handles game logic, player data, and real-time game updates.
- Vanilla JavaScript (Planned): For handling client-side interactions and real-time updates.

# API Structure
The API handles game logic, player management, and game results. It interacts with the database via Entity Framework Core and includes various layers for repository management.

## Controllers
1. GameController:
    - Manages game creation, joining, and gameplay.
    - End
points include:
        - POST /api/game/create: Create a new game.
        - POST /api/game/join: Join an existing game.
        - PUT /api/game/move: Submit a move.
        - POST /api/game/delete: Delete a pending game.
        - GET /api/game/{token}: Retrieve game details.
2. PlayerController:
    - Handles player registration, login, and friend requests.
    - Endpoints include:
        - POST /api/player/create: Register a new player.
        - POST /api/player/login: Log in an existing player.
        - POST /api/player/friend/send: Send a friend request.
        - POST /api/player/friend/accept: Accept a friend request.

3. ResultController:
    - Manages game results and provides statistics.
    - Endpoints include:
        - GET /api/result/{playerId}: Retrieve match history.
        - GET /api/result/stats/{playerId}: Retrieve player statistics.
  
# Database Access Layer
The application uses a repository pattern to manage data. The following repositories interact with the database:
- GameAccessLayer: Manages game-related data.
- PlayerAccessLayer: Manages player data.
- ResultAccessLayer: Handles game results and statistics.
- Repository: Implements IRepository to combine game, player, and result repository logic.

Interfaces include: IGameRepository, IPlayerRepository, IResultRepository, and IRepository for abstracting database interactions.

# MVC Structure
The MVC side of the project is responsible for rendering views and handling user interactions. It communicates with the API for all game-related actions.

# MVC Controllers
1. GameController:
    - Manages game-related interactions like creating, joining, and playing games.
    - Includes logic to fetch game status from the API and update the views accordingly.

2. HomeController:
    - Responsible for the home page where players can view games, pending games, and player statistics.
    - Automatically redirects players in an active game to the game page.

3. ProfileController:
    - Displays player profiles, including match history, friends, and friend requests.

4. GameInProgressMiddleware & GameInProgressFilter:
    - Middleware to automatically redirect players to their ongoing game if they attempt to navigate to other parts of the app while a game is in progress.

# Game Flow
1. Login/Register: Players log in or register using ASP.NET Identity.
2. Game State Check: After login, the system checks if the player is already in a game. If they are, they are redirected to the game page.
3. Create/Join Game: Players can create a new game or join a pending game.
4. Gameplay: The game board is updated in real-time using AJAX (or SignalR in the future).
5. Game Completion: Once the game is over, players are free to start or join another game.
