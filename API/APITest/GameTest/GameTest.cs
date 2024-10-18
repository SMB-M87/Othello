using API.Models;

namespace APITest.GameTest
{
    [TestFixture]
    public class ModelTest
    {
        private Player one;
        private Player two;
        // No color = 0
        // White = 1
        // Black = 2

        [SetUp]
        public void SetUp()
        {
            one = new("first", "One");
            two = new("second", "Two");
        }

        [Test]
        public void GameCreation_BlackStarts_IsValid()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing
            };

            Assert.That(game.PlayersTurn, Is.EqualTo(Color.Black));
        }

        [Test]
        public void PossibleMove_OutOfBound_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };

            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.PossibleMove(8, 8); });

            Assert.That(ex.Message, Is.EqualTo("Move (8,8) is outside the board!"));
        }

        [Test]
        public void PossibleMove_StartSituationMove23Black_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 2 0 0 0 0  <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(2, 3);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void PossibleMove_StartSituationMove23White_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 1 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(2, 3);

            Assert.That(actual, Is.False);
        }


        [Test]
        public void PossibleMove_MoveOnTopEdge_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };

            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(0, 3);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void PossibleMove_MoveOnTopEdge_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing
            };

            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.PlayersTurn = Color.White;

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 1 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(0, 3);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void PossibleMove_MoveOnTopEdgeWhileColumnAlmostFilled_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };

            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 2 0 0 0 0

            var actual = game.PossibleMove(0, 3);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void PossibleMove_MoveOnTopEdgeWhileColumnAlreadyFilled_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.White;

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 1 0 0 0 0

            var actual = game.PossibleMove(0, 3);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void PossibleMove_MoveOnRightEdge_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 2 0 0 0 0  
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(4, 7);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void PossibleMove_MoveOnRightEdge_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 1 0 0 0 0  
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(4, 7);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void PossibleMove_MoveOnRightEdgeWhileRowAlmostFilled_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0 
            // 4   2 1 1 1 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(4, 7);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void PossibleMove_MoveOnRightEdgeWhileRowAlreadyFilled_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  

            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   2 1 1 1 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(4, 7);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void PossibleMove_StartSituationMove22White_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };

            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(2, 2);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void PossibleMove_StartSituationMove22Black_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };

            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(2, 2);

            Assert.That(actual, Is.False);
        }


        [Test]
        public void PossibleMove_MoveOnRightTopCorner_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 1  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(0, 7);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void PossibleMove_MoveOnRightTopCorner_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 2  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.PossibleMove(0, 7);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void PossibleMove_MoveOnRightBottomCorner_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 2 <

            var actual = game.PossibleMove(7, 7);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void PossibleMove_MoveOnRightBottomCorner_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 1  <

            var actual = game.PossibleMove(7, 7);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void PossibleMove_MoveOnLeftTopCorner_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   2 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0 

            var actual = game.PossibleMove(0, 0);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void PossibleMove_MoveOnLeftTopCorner_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0          

            var actual = game.PossibleMove(0, 0);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void PossibleMove_MoveOnLeftBottomCorner_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   1 0 0 0 0 0 0 0 <

            var actual = game.PossibleMove(7, 0);

            Assert.That(actual, Is.True);
        }

        [Test]
        public void PossibleMove_MoveOnLeftBottomCorner_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  <
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   2 0 0 0 0 0 0 0

            var actual = game.PossibleMove(7, 0);

            Assert.That(actual, Is.False);
        }

        [Test]
        public void MakeMove_OutOfBound_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };

            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(8, 8); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (8,8) is outside the board!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.PlayersTurn, Is.EqualTo(Color.White));
            });
        }

        [Test]
        public void MakeMove_StartSituationMove23Black_MoveCorrectlyExecuted()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 2 0 0 0 0  <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            game.MakeMove(2, 3);

            Assert.Multiple(() =>
            {
                Assert.That(game.Board[2, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.PlayersTurn, Is.EqualTo(Color.White));
            });
        }

        [Test]
        public void MakeMove_StartSituationMove23White_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 1 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(2, 3); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (2,3) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[2, 3], Is.EqualTo(Color.None));
                Assert.That(game.PlayersTurn, Is.EqualTo(Color.White));
            });
        }

        [Test]
        public void MakeMove_MoveOnTopEdge_MoveCorrectlyExecuted()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            game.MakeMove(0, 3);

            Assert.Multiple(() =>
            {
                Assert.That(game.Board[0, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[1, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[2, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.PlayersTurn, Is.EqualTo(Color.White));
            });
        }

        [Test]
        public void MakeMove_MoveOnTopEdge_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 1 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(0, 3); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (0,3) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[1, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[2, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[0, 3], Is.EqualTo(Color.None));
            });
        }

        [Test]
        public void MakeMove_MoveOnTopEdgeWhileColumnAlmostFilled_MoveCorrectlyExecuted()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 2 0 0 0 0

            game.MakeMove(0, 3);

            Assert.Multiple(() =>
            {
                Assert.That(game.Board[0, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[1, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[2, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[5, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[6, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[7, 3], Is.EqualTo(Color.Black));
            });
        }

        [Test]
        public void MakeMove_MoveOnTopEdgeWhileColumnAlreadyFilled_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[1, 3] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[7, 3] = Color.White;

            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 1 0 0 0 0

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(0, 3); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (0,3) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[1, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[2, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[0, 3], Is.EqualTo(Color.None));
            });
        }

        [Test]
        public void MakeMove_MoveOnRightEdge_MoveCorrectlyExecuted()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            game.MakeMove(4, 7);

            Assert.Multiple(() =>
            {
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 5], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 6], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 7], Is.EqualTo(Color.Black));
            });
        }

        [Test]
        public void MakeMove_MoveOnRightEdge_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 1 0 0 0 0  
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(4, 7); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (4,7) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 5], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 6], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 7], Is.EqualTo(Color.None));
            });
        }

        [Test]
        public void MakeMove_MoveOnRightEdgeWhileRowAlmostFilled_MoveCorrectlyExecuted()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0 
            // 4   2 1 1 1 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            game.MakeMove(4, 7);

            Assert.Multiple(() =>
            {
                Assert.That(game.Board[4, 0], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 1], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 2], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 5], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 6], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 7], Is.EqualTo(Color.Black));
            });
        }

        [Test]
        public void MakeMove_MoveOnRightEdgeWhileRowAlreadyFilled_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[4, 0] = Color.Black;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  

            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   2 1 1 1 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(4, 7); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (4,7) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 0], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 1], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 2], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 5], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 6], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 7], Is.EqualTo(Color.None));
            });
        }

        //     0 1 2 3 4 5 6 7
        //                     
        // 0   0 0 0 0 0 0 0 0  
        // 1   0 0 0 0 0 0 0 0
        // 2   0 0 0 0 0 0 0 0
        // 3   0 0 0 1 2 0 0 0
        // 4   0 0 0 2 1 0 0 0
        // 5   0 0 0 0 0 0 0 0
        // 6   0 0 0 0 0 0 0 0
        // 7   0 0 0 0 0 0 0 0

        [Test]
        public void MakeMove_StartSituationMove22White_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };

            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(2, 2); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (2,2) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[2, 2], Is.EqualTo(Color.None));
            });
        }

        [Test]
        public void MakeMove_StartSituationMove22Black_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };

            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(2, 2); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (2,2) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[2, 2], Is.EqualTo(Color.None));
            });
        }

        [Test]
        public void MakeMove_MoveOnRightTopCorner_MoveCorrectlyExecuted()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 1  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            game.MakeMove(0, 7);
            Assert.Multiple(() =>
            {
                Assert.That(game.Board[5, 2], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[2, 5], Is.EqualTo(Color.White));
                Assert.That(game.Board[1, 6], Is.EqualTo(Color.White));
                Assert.That(game.Board[0, 7], Is.EqualTo(Color.White));
            });
        }

        [Test]
        public void MakeMove_MoveOnRightTopCorner_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[2, 5] = Color.Black;
            game.Board[1, 6] = Color.Black;
            game.Board[5, 2] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 2  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(0, 7); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (0,7) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[1, 6], Is.EqualTo(Color.Black));
                Assert.That(game.Board[2, 5], Is.EqualTo(Color.Black));
                Assert.That(game.Board[5, 2], Is.EqualTo(Color.White));
                Assert.That(game.Board[0, 7], Is.EqualTo(Color.None));
            });
        }

        [Test]
        public void MakeMove_MoveOnRightBottomCorner_MoveCorrectlyExecuted()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 2 <

            game.MakeMove(7, 7);

            Assert.Multiple(() =>
            {
                Assert.That(game.Board[2, 2], Is.EqualTo(Color.Black));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[5, 5], Is.EqualTo(Color.Black));
                Assert.That(game.Board[6, 6], Is.EqualTo(Color.Black));
                Assert.That(game.Board[7, 7], Is.EqualTo(Color.Black));
            });
        }

        [Test]
        public void MakeMove_MoveOnRightBottomCorner_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[2, 2] = Color.Black;
            game.Board[5, 5] = Color.White;
            game.Board[6, 6] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 1 <

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(7, 7); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (7,7) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[2, 2], Is.EqualTo(Color.Black));
                Assert.That(game.Board[5, 5], Is.EqualTo(Color.White));
                Assert.That(game.Board[6, 6], Is.EqualTo(Color.White));
                Assert.That(game.Board[7, 7], Is.EqualTo(Color.None));
            });
        }

        [Test]
        public void MakeMove_MoveOnLeftTopCorner_MoveCorrectlyExecuted()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   2 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0 

            game.MakeMove(0, 0);

            Assert.Multiple(() =>
            {
                Assert.That(game.Board[0, 0], Is.EqualTo(Color.Black));
                Assert.That(game.Board[1, 1], Is.EqualTo(Color.Black));
                Assert.That(game.Board[2, 2], Is.EqualTo(Color.Black));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[5, 5], Is.EqualTo(Color.Black));
            });
        }

        [Test]
        public void MakeMove_MoveOnLeftTopCorner_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[1, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[5, 5] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(0, 0); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (0,0) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[1, 1], Is.EqualTo(Color.White));
                Assert.That(game.Board[2, 2], Is.EqualTo(Color.White));
                Assert.That(game.Board[5, 5], Is.EqualTo(Color.Black));
                Assert.That(game.Board[0, 0], Is.EqualTo(Color.None));
            });
        }

        [Test]
        public void MakeMove_MoveOnLeftBottomCorner_MoveCorrectlyExecuted()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   1 0 0 0 0 0 0 0 <

            game.MakeMove(7, 0);

            Assert.Multiple(() =>
            {
                Assert.That(game.Board[7, 0], Is.EqualTo(Color.White));
                Assert.That(game.Board[6, 1], Is.EqualTo(Color.White));
                Assert.That(game.Board[5, 2], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[2, 5], Is.EqualTo(Color.White));
            });
        }

        [Test]
        public void MakeMove_MoveOnLeftBottomCorner_Exception()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.Black
            };
            game.Board[2, 5] = Color.White;
            game.Board[5, 2] = Color.Black;
            game.Board[6, 1] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   2 0 0 0 0 0 0 0 <

            InvalidGameOperationException ex = Assert.Throws<InvalidGameOperationException>(delegate { game.MakeMove(7, 0); });
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Move (7,0) is not possible!"));
                Assert.That(game.Board[3, 3], Is.EqualTo(Color.White));
                Assert.That(game.Board[4, 4], Is.EqualTo(Color.White));
                Assert.That(game.Board[3, 4], Is.EqualTo(Color.Black));
                Assert.That(game.Board[4, 3], Is.EqualTo(Color.Black));
                Assert.That(game.Board[2, 5], Is.EqualTo(Color.White));
                Assert.That(game.Board[5, 2], Is.EqualTo(Color.Black));
                Assert.That(game.Board[6, 1], Is.EqualTo(Color.Black));
                Assert.That(game.Board[7, 7], Is.EqualTo(Color.None));
                Assert.That(game.Board[7, 0], Is.EqualTo(Color.None));
            });
        }

        [Test]
        public void Pass_BlackTurnAndNoPossibleMove_ReturnTrueAndChangedTurns()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing
            };

            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 0
            // 4   1 1 1 1 1 1 0 0
            // 5   1 1 1 1 1 1 0 2
            // 6   1 1 1 1 1 1 1 0
            // 7   1 1 1 1 1 1 1 1

            game.Pass();

            Assert.That(game.PlayersTurn, Is.EqualTo(Color.White));
        }

        [Test]
        public void Pass_WhiteTurnButNoPossibleMove_ReturnTrueAndChangedTurns()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.White,
                Second = two.Token,
                SColor = Color.Black,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 0
            // 4   1 1 1 1 1 1 0 0
            // 5   1 1 1 1 1 1 0 2
            // 6   1 1 1 1 1 1 1 0
            // 7   1 1 1 1 1 1 1 1

            game.Pass();

            Assert.That(game.PlayersTurn, Is.EqualTo(Color.Black));
        }

        [Test]
        public void Finished_NoPossibleMoveAnymore_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.None;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.None;
            game.Board[4, 7] = Color.None;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.None;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.None;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 0
            // 4   1 1 1 1 1 1 0 0
            // 5   1 1 1 1 1 1 0 2
            // 6   1 1 1 1 1 1 1 0
            // 7   1 1 1 1 1 1 1 1

            var actual = game.Finished();

            Assert.That(actual, Is.True);
        }

        [Test]
        public void Finished_NoPossibleMoveBoardIsFull_ReturnTrue()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };
            game.Board[0, 0] = Color.White;
            game.Board[0, 1] = Color.White;
            game.Board[0, 2] = Color.White;
            game.Board[0, 3] = Color.White;
            game.Board[0, 4] = Color.White;
            game.Board[0, 5] = Color.White;
            game.Board[0, 6] = Color.White;
            game.Board[0, 7] = Color.White;
            game.Board[1, 0] = Color.White;
            game.Board[1, 1] = Color.White;
            game.Board[1, 2] = Color.White;
            game.Board[1, 3] = Color.White;
            game.Board[1, 4] = Color.White;
            game.Board[1, 5] = Color.White;
            game.Board[1, 6] = Color.White;
            game.Board[1, 7] = Color.White;
            game.Board[2, 0] = Color.White;
            game.Board[2, 1] = Color.White;
            game.Board[2, 2] = Color.White;
            game.Board[2, 3] = Color.White;
            game.Board[2, 4] = Color.White;
            game.Board[2, 5] = Color.White;
            game.Board[2, 6] = Color.White;
            game.Board[2, 7] = Color.White;
            game.Board[3, 0] = Color.White;
            game.Board[3, 1] = Color.White;
            game.Board[3, 2] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[3, 5] = Color.White;
            game.Board[3, 6] = Color.White;
            game.Board[3, 7] = Color.White;
            game.Board[4, 0] = Color.White;
            game.Board[4, 1] = Color.White;
            game.Board[4, 2] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[4, 4] = Color.White;
            game.Board[4, 5] = Color.White;
            game.Board[4, 6] = Color.Black;
            game.Board[4, 7] = Color.Black;
            game.Board[5, 0] = Color.White;
            game.Board[5, 1] = Color.White;
            game.Board[5, 2] = Color.White;
            game.Board[5, 3] = Color.White;
            game.Board[5, 4] = Color.White;
            game.Board[5, 5] = Color.White;
            game.Board[5, 6] = Color.Black;
            game.Board[5, 7] = Color.Black;
            game.Board[6, 0] = Color.White;
            game.Board[6, 1] = Color.White;
            game.Board[6, 2] = Color.White;
            game.Board[6, 3] = Color.White;
            game.Board[6, 4] = Color.White;
            game.Board[6, 5] = Color.White;
            game.Board[6, 6] = Color.White;
            game.Board[6, 7] = Color.Black;
            game.Board[7, 0] = Color.White;
            game.Board[7, 1] = Color.White;
            game.Board[7, 2] = Color.White;
            game.Board[7, 3] = Color.White;
            game.Board[7, 4] = Color.White;
            game.Board[7, 5] = Color.White;
            game.Board[7, 6] = Color.White;
            game.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 2
            // 4   1 1 1 1 1 1 2 2
            // 5   1 1 1 1 1 1 2 2
            // 6   1 1 1 1 1 1 1 2
            // 7   1 1 1 1 1 1 1 1

            var actual = game.Finished();

            Assert.That(actual, Is.True);
        }

        [Test]
        public void Finished_MoveIsPossible_ReturnFalse()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing,
                PlayersTurn = Color.White
            };

            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.Finished();

            Assert.That(actual, Is.False);
        }

        [Test]
        public void WinningColor_Equal_ReturnColorNone()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing
            };

            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.WinningColor();

            Assert.That(actual, Is.EqualTo(Color.None));
        }

        [Test]
        public void WinningColor_Black_ReturnColorBlack()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing
            };

            game.Board[2, 3] = Color.Black;
            game.Board[3, 3] = Color.Black;
            game.Board[4, 3] = Color.Black;
            game.Board[3, 4] = Color.Black;
            game.Board[4, 4] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 2 0 0 0 0
            // 3   0 0 0 2 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.WinningColor();

            Assert.That(actual, Is.EqualTo(Color.Black));
        }

        [Test]
        public void WinningColor_White_ReturnColorWhite()
        {
            Game game = new(one.Token, "I search an advanced player!")
            {
                Token = "one",
                FColor = Color.Black,
                Second = two.Token,
                SColor = Color.White,
                Status = Status.Playing
            };

            game.Board[2, 3] = Color.White;
            game.Board[3, 3] = Color.White;
            game.Board[4, 3] = Color.White;
            game.Board[3, 4] = Color.White;
            game.Board[4, 4] = Color.Black;

            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 1 0 0 0
            // 4   0 0 0 1 2 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            var actual = game.WinningColor();

            Assert.That(actual, Is.EqualTo(Color.White));
        }
    }
}