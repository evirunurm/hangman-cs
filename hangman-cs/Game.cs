using System.Collections;
using hangman_cs;

var g = new Game();
g.Play();
namespace hangman_cs
{
    public class Game
    {
        private readonly ArrayList _words = new ArrayList();
        private string? _word;
        private string _guessedWord = "";

        private int _maxTries = 10;
        private bool isGameStopped = false;
        private ArrayList _notFoundChars = new ArrayList();
        private char currChar;
    
        public void Play() {
            Console.WriteLine("Game starting...");
            Start();
                   
            while (!isGameStopped)
            {
                 Render();
                 ReadAnswer();
                 Update();
            }

            Stop();
        }



        private void Start()
        {
            Initialize();
            this._word = GetRandomWord();
            this._guessedWord = string.Concat((Enumerable.Repeat("_ ", this._word.Length)));
        }

        private void Initialize()
        {
            _words.Add("gato");
            _words.Add("perro");
            _words.Add("mariquita");
            _words.Add("pato");
            _words.Add("ratón");
            _words.Add("gusano");
        }

        private string GetRandomWord()
        {
            var randomGen = new Random();
            var random = (int) randomGen.NextInt64(_words.Count);
            Console.Write(random);
            
            return (string) _words[random]!;
        }

        private void Render()
        {
            Console.Clear();
            var identation = "        ";
            switch (_notFoundChars.Count)
            {
                case 0:
                    Console.Write("\n\n\n\n\n\n 	________________\n");
                    break;
                case 1:
                    Console.Write("	  \n	  |\n	  |\n	  |\n	  |\n	  |\n	__|_____________\n");
                    break;
                case 2:
                    Console.Write("	  ____________\n	  |\n	  |\n	  |\n	  |\n	  |\n	__|_____________\n");
                    break;
                case 3:
                    Console.Write("	  ____________\n	  | /\n	  |/\n	  |\n	  |\n	  |\n	__|_____________\n");
                    break;
                case 4:
                    Console.Write("	  ____________\n	  | /        |\n	  |/\n          |\n          |\n	  |\n	__|_____________\n");
                    break;
                case 5:
                    Console.Write("	  ____________\n	  | /        |\n	  |/         O\n	  |\n	  |\n	  |\n	__|_____________\n");
                    break;
                case 6:
                    Console.Write("	  ____________\n	  | /        |\n	  |/         O\n	  |          |\n	  |          |\n	  |\n	__|____________\n");
                    break;
                case 7:
                    Console.Write("	  ____________\n	  | /        |\n	  |/         O\n	  |         /|\n	  |          |\n	  |\n	__|____________\n");
                    break;
                case 8:
                    Console.Write("	  ____________\n	  | /        |\n	  |/         O\n	  |         /|\\\n	  |          |\n	  |\n	__|_____________\n");
                    break;
                case 9:
                    Console.Write("	  ____________\n	  | /        |\n	  |/         O\n	  |         /|\\\n	  |          |\n	  |         /\n	__|_____________\n");
                    break;
                case 10:
                    Console.Write("	  _____________\n	  | /        |\n	  |/         O\n	  |         /|\\\n	  |          |\n	  |         / \\\n 	__|_______ _ ___\n");
                    break;
            }
            Console.WriteLine(identation + _guessedWord);
            Console.Write(identation + "Not found: ");
            foreach (var notFoundChar in _notFoundChars)
            {
                Console.Write(notFoundChar + " ");
            }
            Console.WriteLine();
            
            
        }
        
        private void ReadAnswer()
        {
            Console.Write("Letter: ");
            var input = Console.ReadLine();
            while (input != null && input.Length != 1)
            {
                Console.WriteLine("Enter a letter");
                Console.Write("Letter: ");
                input =  Console.ReadLine();
            }

            if (input != null) currChar = Convert.ToChar(input.ToLower());
        }

        private void Update()
        {
            if (_word.Contains(currChar) && !_guessedWord.Contains(currChar))
            {
                var iterator = 0;
                foreach (var c in _word.ToCharArray())
                {
                    if (c == currChar)
                    {
                        var guessedArray = _guessedWord.ToCharArray();
                        guessedArray[iterator * 2] = _word.ToCharArray()[iterator];
                        _guessedWord = new string(guessedArray);
                        Console.Write(_guessedWord);
                    }
                    iterator++;
                    if (_guessedWord.Replace(" ", "").Equals(_word))
                    {
                        isGameStopped = true;
                    }
                }
            } else if (_guessedWord.Contains(currChar) || _notFoundChars.Contains(currChar))
            {
                Console.WriteLine("That letter is right there.");
            }
            else
            {
                _notFoundChars.Add(currChar);
                if (_notFoundChars.Count == _maxTries)
                {
                    isGameStopped = true;
                }
                Console.WriteLine("Letter not found.");
            }
        }
        private void Stop() {
            Render();
        }

    }
}