/*using Newtonsoft.Json.Linq;
using Backend.Models;
using System.Collections.Generic;
using System.Reflection;

namespace Backend.Data
{
    public class GameAccessLayer : IGameRepository
    {
        private readonly ReversiDbContext _context;

        public GameAccessLayer(ReversiDbContext context)
        {
            _context = context;     
        }

        private Player? GetPlayer(string token)
        {
            return _context.Players.FirstOrDefault(s => s.Token.Equals(token));
        }

        public List<Game>? GetGames()
        {         
            return _context.Games.ToList();
        }

        public Game? GetGame(string token)
        {
            return _context.Games.FirstOrDefault(s => s.Token.Equals(token));
        }

        public Game? GetPlayersGame(string playerToken)
        {
            var games = GetGames();

            var game = games!.FirstOrDefault(s => s.First.Token.Equals(playerToken));

            if (game is null)
            {
                game = games!.FirstOrDefault(s => s.Second.Token.Equals(playerToken));
            }

            return game;
        }

        public void AddPlayer(Player player)
        {
            var control = GetPlayer(player.Token);

            if (control is null)
            {
                _context.Players.Add(player);
                _context.SaveChanges();
            }
        }

        public void AddGame(Game game)
        {
            var creater = GetPlayer(game.First.Token);

            if (creater is not null && creater.InGame == false)
            {
                _context.Games.Add(game);
                creater.InGame = true;
                _context.SaveChanges();
            }
        }

        public void AddResult(GameResult result)
        {
            var control = GetGame(result.Token);

            if (control is not null && control.Token == result.Token)
            {
                if(result.Draw is not null)
                {
                    string[] array = result.Draw.Split(' ');
                    var first = GetPlayer(array[0]);
                    var second = GetPlayer(array[1]);

                    if(first is not null && second is not null && first.InGame == true && second.InGame == true)
                    {
                        first.Draw++;
                        second.Draw++;
                        first.InGame = false;
                        second.InGame = false;
                    }
                }
                else
                {
                    var winner = GetPlayer(result.Winner);
                    var loser = GetPlayer(result.Loser);

                    if(winner is not null && loser is not null && winner.InGame == true && loser.InGame == true)
                    {
                        winner.Won++;
                        winner.InGame = false;
                        loser.Lost++;
                        loser.InGame = false;
                    }
                }

                _context.Results.Add(result);
                _context.Games.Remove(control);
                _context.SaveChanges();
            }
        }

        public void JoinGame(GameJoiner joiner)
        {
            var player = GetPlayer(joiner.Player.Token);

            if (player is not null && player.InGame == false)
            {
                var game = GetGame(joiner.Token);

                if (game is not null && GetPlayer(game.First.Token)!.InGame == true && joiner.Player.Token != game.First.Token)
                {
                    game.Second.Token = joiner.Player.Token;
                    game.Status = Status.Playing;
                    _context.Games.Update(game);
                    player.InGame = true;
                    _context.SaveChanges();
                }
            }
        }

        public void UpdatePlayer(Player player)
        {
*//*            var update = GetPlayer(player.Token);

            if (update is not null)
            {
                update.Username = player.Username;
                update.InGame = player.InGame;
                update.Won = player.Won;
                update.Lost = player.Lost;
                update.Draw = player.Draw;
                _context.SaveChanges();
            }*//*
        }

        public void UpdateGame(Game game)
        {
            var update = GetGame(game.Token);

            if(update is not null)
            {
                _context.Games.Update(update);
                _context.SaveChanges();
            }
        }

        public void DeleteGame(Game delete)
        {
            var remove = GetGame(delete.Token);

            if (remove is not null)
            {
                _context.Games.Remove(remove);
                _context.SaveChanges();
            }
        }
    }
}
*/