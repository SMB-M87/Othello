# Othello MVC API Application
This is a backend-driven web application for a multiplayer Othello (Reversi) game built using ASP.NET Core MVC. The application consists of a REST API and an MVC client. The frontend is planned to be developed in vanilla JavaScript, and the current implementation handles all logic via the API and server-side code.

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
- [Planned Frontend](#PlannedFrontend)  
- [Installation and Setup](#InstallationandSetup)  
- [Future Enhancements](#FutureEnhancements)  

## Project Overview
The Othello MVC API project is designed to handle multiplayer Othello gameplay through a robust backend architecture. The application provides essential features for managing players, game sessions, and results. The REST API offers a suite of endpoints to facilitate game creation, player interaction, and game results tracking, while the MVC structure allows seamless interaction between the API and the client application.
![Unauthenticated](https://github.com/user-attachments/assets/d019d59d-2dd9-44fd-b990-5a49f23663c1)
![Registration](https://github.com/user-attachments/assets/1075be61-2112-476c-9f7d-c16d69a8d05a)
![Home](https://github.com/user-attachments/assets/8f2fd601-b916-4978-9b7b-46b55feccac7)
![Create](https://github.com/user-attachments/assets/91dcb072-a52b-4436-9dd3-6e29cbf274f5)
![Wait](https://github.com/user-attachments/assets/0a50b714-2e81-4f41-8dae-041dae74d7f8)
![Game](https://github.com/user-attachments/assets/af22cd5e-6e60-4ca3-bfc4-49faafd629de)
![Result](https://github.com/user-attachments/assets/32d26c27-ebf6-4813-b1be-8cd460d1fec4)
![Rematch](https://github.com/user-attachments/assets/e1347d79-4306-489f-9192-cca762854942)
![Profile](https://github.com/user-attachments/assets/336cd202-f2a7-4cbf-b2f0-3ce57e6a26da)
![ProfileAcceptDecline](https://github.com/user-attachments/assets/f86ecc20-4d8f-46b1-a781-445dc89b0d05)
![ProfileNoFriend](https://github.com/user-attachments/assets/f830db27-ba8d-4f18-8588-6e790c67071d)
![Dashboard](https://github.com/user-attachments/assets/0755cc3e-338d-42ee-8295-0a9f4ff444ff)
![Players](https://github.com/user-attachments/assets/ee7516f4-242b-4757-9c5a-7fb93494b1ef)
![PlayersDetail](https://github.com/user-attachments/assets/71b43652-a18a-4d74-abfa-0f6e773de5a2)
![Games](https://github.com/user-attachments/assets/41a6def5-3ea5-4427-a260-3570ac92b074)
![GameDetail](https://github.com/user-attachments/assets/9f574836-9d27-404d-b535-0d711979c464)
![Results](https://github.com/user-attachments/assets/31a011e3-2a6b-4e82-8130-b9de0d194b78)
![ResultDetail](https://github.com/user-attachments/assets/faded18b-f9df-4fc1-b358-8904243216eb)
![Privacy](https://github.com/user-attachments/assets/2672b668-744e-43bd-8808-e7e7ed486970)

https://github.com/user-attachments/assets/379a3a77-7a70-4bb7-9359-d31cc58dcbd0


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
    - Endpoints include:
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

# Planned Frontend
The frontend will be developed using vanilla JavaScript. Features like real-time updates, game state polling, and user interactions will be handled with AJAX requests.

## Planned Features
- AJAX Polling: Check the game state periodically to update the game board in real-time.
- Vanilla JavaScript: Used for UI interactivity, including showing move options, handling game state changes, and responding to player actions.
- Responsive Design: Ensure that the game and all other pages are responsive across devices.

#Installation and Setup
1. Clone the repository:
    - git clone https://github.com/your-repository/othello-game.git
    - cd othello-game

2. Setup the database:
    - Ensure SQL Server is running.
    - Update the connection string in appsettings.json.
    - Run migrations to create the database schema:
         - dotnet ef migrations add InitialCreate
         - dotnet ef database update
  
3. Run the application:
    - Run both the MVC and API projects via Visual Studio or CLI:
         - dotnet run

4. Access the app:
    - Go to https://localhost:{port} to start the game.

# Future Enhancements
1. SignalR Integration: Real-time updates via SignalR to avoid polling.
2. Frontend Development: Develop a rich user interface using vanilla JavaScript and possibly transition to a modern frontend framework (React/Angular) later.
3. Player Statistics: Show more detailed player statistics such as move efficiency, longest winning streak, etc.
4. AI Opponent: Implement an AI opponent for single-player mode.

# Conclusion
This Othello MVC and API application provides a backend foundation for building a fully-featured multiplayer game with real-time updates, player management, and friend-based interactions. The project is designed to be flexible and scalable, allowing for future enhancements and frontend development using vanilla JavaScript or any modern framework.
